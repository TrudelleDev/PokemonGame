using MonsterTamer.Monster;
using MonsterTamer.Move.UI;
using MonsterTamer.Shared.UI.Navigation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Summary
{
    /// <summary>
    /// Controls the move tab of the Pokémon summary screen.
    /// Handles binding Pokémon data to the identity panel and move slots.
    /// Manages visibility of the header during enable/disable.
    /// </summary>
    internal class SummaryMoveTab : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Displays the list of moves and handles binding each move slot.")]
        private MoveSlotUIManager moveManager;

        [SerializeField, Required]
        private VerticalMenuController controller;

        private void OnDisable()
        {
            StartCoroutine(controller.ResetController());
        }

        /// <summary>
        /// Binds the specified Pokémon's data to the identity panel and move manager.
        /// Clears the UI if the Pokémon instance or its core data is null.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(MonsterInstance pokemon)
        {
            // always reset state first
            Unbind();

            if (pokemon?.Definition == null)
            {
                return;
            }

            // identityPanel.Bind(pokemon);
            moveManager.Bind(pokemon);
        }

        /// <summary>
        /// Clears all bound data from the identity panel and move manager.
        /// </summary>
        public void Unbind()
        {
            // identityPanel.Unbind();
            moveManager.Unbind();
        }
    }
}
