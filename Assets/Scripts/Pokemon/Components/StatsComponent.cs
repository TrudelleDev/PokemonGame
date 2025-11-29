using PokemonGame.Move;
using PokemonGame.Move.Enums;
using PokemonGame.Pokemon.Models;
using UnityEngine;

namespace PokemonGame.Pokemon.Components
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

        public int CalculateDamage(PokemonInstance user, PokemonInstance target, MoveInstance move)
        {
            bool isPhysical = move.Definition.Classification.Category == MoveCategory.Physical;

            int attackStat = isPhysical ? user.Stats.StatStage.Modified.Attack : user.Stats.StatStage.Modified.SpecialAttack;
            int defenseStat = isPhysical ? target.Stats.StatStage.Modified.Defense : target.Stats.StatStage.Modified.SpecialDefense;

            float baseDamage = (((2f * user.Experience.Level + 10f) / 250f)
                                * ((float)attackStat / defenseStat) * move.Definition.MoveInfo.Power + 2f);

            float finalDamage = baseDamage * UnityEngine.Random.Range(0.85f, 1f);

            return Mathf.Max(1, Mathf.RoundToInt(finalDamage));
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
