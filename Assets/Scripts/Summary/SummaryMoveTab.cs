using PokemonGame.Move.UI;
using PokemonGame.Pokemons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Summary
{
    /// <summary>
    /// Controls the move tab of the Pokémon summary screen.
    /// Handles binding Pokémon data to the identity panel and move slots.
    /// Manages visibility of the header during enable/disable.
    /// </summary>
    public class SummaryMoveTab : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's name, level, and gender.")]
        private SummaryHeader header;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's icon and species name.")]
        private SummaryIdentityPanel identityPanel;

        [SerializeField, Required]
        [Tooltip("Displays the list of moves and handles binding each move slot.")]
        private MoveSlotUIManager moveManager;

        private void OnEnable()
        {
            header.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            header.gameObject.SetActive(true);
        }

        /// <summary>
        /// Binds the specified Pokémon's data to the identity panel and move manager.
        /// Clears the UI if the Pokémon instance or its core data is null.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(PokemonInstance pokemon)
        {
            // always reset state first
            Unbind();

            if (pokemon?.Definition == null)
            {
                return;
            }

            identityPanel.Bind(pokemon);
            moveManager.Bind(pokemon);
        }

        /// <summary>
        /// Clears all bound data from the identity panel and move manager.
        /// </summary>
        public void Unbind()
        {
            identityPanel.Unbind();
            moveManager.Unbind();
        }
    }
}
