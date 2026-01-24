using PokemonGame.Pokemon;
using PokemonGame.Pokemon.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Summary
{
    /// <summary>
    /// Displays detailed Monster stats and ability information in the summary screen.
    /// Supports dynamic data binding and clears the UI when the Monster definition is missing or invalid.
    /// </summary>
    public class SummarySkillTab : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Group containing base stat UI elements.")]
        private PokemonStatsUI statsUI;

        [SerializeField, Required, Tooltip("Group containing experience UI elements.")]
        private ExperienceUI experienceUI;

        /// <summary>
        /// Binds the specified Monster definition to the stat and experience UI groups.
        /// Clears the UI if the Monster defenition is missing or invalid.
        /// </summary>
        /// <param name="monster">The Monster instance to display.</param>
        public void Bind(PokemonInstance monster)
        {
            if (monster?.Definition == null)
            {
                Unbind();
                return;
            }

            statsUI.Bind(monster);
            experienceUI.Bind(monster);
        }

        /// <summary>
        /// Clears the stat and experience UI elements.
        /// </summary>
        public void Unbind()
        {
            statsUI.Unbind();
            experienceUI.Unbind();
        }
    }
}
