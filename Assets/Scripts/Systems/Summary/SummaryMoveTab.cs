using PokemonGame.Pokemons;
using PokemonGame.Pokemons.Moves.UI;
using PokemonGame.Shared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Systems.Summary
{
    /// <summary>
    /// Controls the move tab of the Pok�mon summary screen.
    /// Handles binding Pok�mon data to the identity panel and move slots.
    /// Manages visibility of the header during enable/disable.
    /// </summary>
    public class SummaryMoveTab : MonoBehaviour, IPokemonBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's name, level, and gender.")]
        private SummaryHeader header;

        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's icon and species name.")]
        private SummaryIdentityPanel identityPanel;

        [SerializeField, Required]
        [Tooltip("Displays the list of moves and handles binding each move slot.")]
        private MoveSlotManager moveManager;

        private void OnEnable()
        {
            header.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            header.gameObject.SetActive(true);
        }

        /// <summary>
        /// Binds the specified Pok�mon's data to the identity panel and move manager.
        /// Clears the UI if the Pok�mon instance or its core data is null.
        /// </summary>
        /// <param name="pokemon">The Pok�mon instance to display.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Data == null)
            {
                Unbind();
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
