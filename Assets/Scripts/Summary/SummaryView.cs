using MonsterTamer.Characters;
using MonsterTamer.Characters.Core;
using MonsterTamer.Monster;
using MonsterTamer.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Summary
{
    /// <summary>
    /// Displays detailed information about the selected Monster,
    /// including stats, skills, and moves.
    /// </summary>
    internal class SummaryView : View
    {
        [SerializeField, Required, Tooltip("Tabs for stats, skills, and move details.")]
        private SummaryTabGroup summaryTabs;

        [SerializeField, Required]
        [Tooltip("Reference to the player's party for selecting the current Monster.")]
        private Character player;

        /// <summary>
        /// Called when the view is enabled.
        /// Resets the panel controller and binds the summary tabs to the selected Monster.
        /// </summary>
        private void OnEnable()
        {
            MonsterInstance selectedPokemon = player.Party.SelectedMonster;

            if (selectedPokemon == null)
            {
                summaryTabs.Unbind();
                return;
            }

            ResetMenuController();
            summaryTabs.Bind(selectedPokemon);
            ReturnKeyPressed += HandleReturnKeyPressed;
        }

        private void OnDisable()
        {
            ReturnKeyPressed -= HandleReturnKeyPressed;
        }

        private void HandleReturnKeyPressed()
        {
            ViewManager.Instance.Close<SummaryView>();
        }
    }
}
