using PokemonGame.Move;
using PokemonGame.Move.Enums;
using PokemonGame.Pokemon.Models;
using UnityEngine;

namespace PokemonGame.Pokemon.Components
{
    /// <summary>
    /// Manages all calculated statistics for a Pokémon, including IVs, EVs, 
    /// and derived combat stats.
    /// </summary>
    public class StatsComponent
    {
        private readonly int level;
        private readonly PokemonDefinition definition;

        public PokemonStats IV { get; private set; }
        public PokemonStats EV { get; private set; }
        public PokemonStats Core { get; private set; }

        public StatsComponent(int level, PokemonDefinition definition)
        {
            this.level = level;
            this.definition = definition;

            IV = StatsCalculator.GenerateIndividualValues();
            EV = new PokemonStats(0, 0, 0, 0, 0, 0);
            Core = StatsCalculator.CalculateCoreStats(definition, IV, EV, level);
        }

        public void UpdateStats()
        {
            Core = StatsCalculator.CalculateCoreStats(definition, IV, EV, level);
        }

        /// <summary>
        /// Calculates the raw damage a move would deal before applying health reduction.
        /// </summary>
        /// <param name="move">The move being used.</param>
        /// <param name="target">The target Pokémon instance.</param>
        /// <returns>The pre–health-application damage value.</returns>
        public int Attack(MoveInstance move, PokemonInstance target)
        {
            bool isPhysical = move.Definition.Classification.Category == MoveCategory.Physical;

            int atk = isPhysical ? Core.Attack : Core.SpecialAttack;
            int def = isPhysical ? target.Stats.Core.Defense : target.Stats.Core.SpecialDefense;

            float baseDamage = move.Definition.MoveInfo.Power * ((float)atk / def);
            return Mathf.Max(1, Mathf.RoundToInt(baseDamage)) / 10;
        }
    }
}
