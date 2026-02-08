using System.Linq;
using MonsterTamer.Move;
using MonsterTamer.Move.Enums;
using MonsterTamer.Pokemon.Models;
using MonsterTamer.Type;
using UnityEngine;

namespace MonsterTamer.Pokemon.Components
{
    /// <summary>
    /// Manages a Pokémon's calculated statistics (IVs, EVs, core stats) and
    /// temporary stat stage modifications for battle.
    /// </summary>
    public class StatsComponent
    {
        private readonly int level;
        private readonly PokemonDefinition definition;

        public PokemonStats IV { get; private set; }
        public PokemonStats EV { get; private set; }
        public PokemonStats Core { get; private set; }
        public StatStageComponent StatStage { get; private set; }

        public StatsComponent(int level, PokemonDefinition definition)
        {
            this.level = level;
            this.definition = definition;

            IV = StatsCalculator.GenerateIndividualValues();
            EV = new PokemonStats(0, 0, 0, 0, 0, 0);
            Core = StatsCalculator.CalculateCoreStats(definition, IV, EV, level);

            StatStage = new StatStageComponent(Core);
            StatStage.ResetStages();
        }

      
        /// <summary>
        /// Recalculate core stats (e.g., after level up) and updates modified stats.
        /// </summary>
        public void UpdateCoreStats()
        {
            Core = StatsCalculator.CalculateCoreStats(definition, IV, EV, level);
            StatStage.UpdateModifiedStats();
        }
    }
}
