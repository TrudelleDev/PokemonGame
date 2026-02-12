using MonsterTamer.Monster;
using MonsterTamer.Monster.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Summary
{
    /// <summary>
    /// Controls the detailed Monster info panel in the summary screen.
    /// Displays monster overview and trainer memo.
    /// </summary>
    internal class SummaryInfoTab : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Displays the Monster's overview information")]
        private MonsterOverviewPanel monsterOverviewUI;

        [SerializeField, Required]
        [Tooltip("Displays the trainer's memo (e.g., caught location, encounter date).")]
        private TrainerMemoUI trainerMemoUI;

        /// <summary>
        /// Binds the specified Monster to the overview and trainer memo UI elements.
        /// Clears the UI if the Monster or its core data is null.
        /// </summary>
        /// <param name="monster">The Monster instance to display.</param>
        public void Bind(MonsterInstance monster)
        {
            if (monster?.Definition == null)
            {
                Unbind();
                return;
            }

            monsterOverviewUI.Bind(monster);
            trainerMemoUI.Bind(monster);
        }

        /// <summary>
        /// Clears the overview and trainer memo UI elements.
        /// </summary>
        public void Unbind()
        {
            monsterOverviewUI.Unbind();
            trainerMemoUI.Unbind();
        }
    }
}
