using System;
using MonsterTamer.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Summary
{
    /// <summary>
    /// Manages the Pokémon summary UI, including the header and tab content
    /// (Info, Skills, Moves), and handles data binding for the selected Pokémon.
    /// </summary>
    [Serializable]
    internal class SummaryTabGroup
    {
        [SerializeField, Required]
        [Tooltip("Displays the Monster's name, level, gender, and sprite.")]
        private SummaryHeader header;

        [SerializeField, Required]
        [Tooltip("Displays general information about the Monster.")]
        private SummaryInfoTab infoTab;

        [SerializeField, Required]
        [Tooltip("Displays the Monster's core stats.")]
        private SummarySkillTab skillTab;

        [SerializeField, Required]
        [Tooltip("Displays the Monster's moves.")]
        private SummaryMoveTab moveTab;

        /// <summary>
        /// Binds a Monster to all summary UI sections.
        /// Clears the UI if the Monster or its definition is invalid.
        /// </summary>
        /// <param name="monster">The Monster to display in the summary UI.</param>
        public void Bind(PokemonInstance monster)
        {
            if (monster?.Definition == null)
            {
                Unbind();
                return;
            }

            header.Bind(monster);
            infoTab.Bind(monster);
            skillTab.Bind(monster);
            moveTab.Bind(monster);
        }

        /// <summary>
        /// Unbinds all summary UI sections.
        /// </summary>
        public void Unbind()
        {
            header.Unbind();
            infoTab.Unbind();
            skillTab.Unbind();
            moveTab.Unbind();
        }
    }
}
