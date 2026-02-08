using MonsterTamer.Pokemon.Enums;
using MonsterTamer.Pokemon.Models;
using UnityEngine;

namespace MonsterTamer.Pokemon.Components
{
    public class StatStageComponent
    {
        public const int MinStage = -6;
        public const int MaxStage = 6;

        private readonly PokemonStats stats;

        // Stat stages: indexed by PokemonStat enum (excluding HP)
        private readonly int[] statStages = new int[6];

        // Current stats after applying stages
        private PokemonStats modifiedStats;
        public PokemonStats Modified => modifiedStats;

        public StatStageComponent(PokemonStats stats)
        {
            this.stats = stats;
        }

        /// <summary>
        /// Resets all stat stages to 0 and updates modified stats.
        /// </summary>
        public void ResetStages()
        {
            for (int i = 0; i < statStages.Length; i++)
            {
                statStages[i] = 0;
            }

            UpdateModifiedStats();
        }

        /// <summary>
        /// Applies a stat stage change (e.g., +1 stage or -2 stages).
        /// </summary>
        public void ModifyStat(PokemonStat stat, int stageChange)
        {
            if (stat == PokemonStat.HealthPoint)
                return; // HP is not affected by stages

            int index = (int)stat - 1; // Assuming HP = 0
            statStages[index] = Mathf.Clamp(statStages[index] + stageChange, MinStage, MaxStage);

            UpdateModifiedStats();
        }

        /// <summary>
        /// Updates the modified stats based on core stats and current stages.
        /// </summary>
        public void UpdateModifiedStats()
        {
            int hp = stats.HealthPoint;
            int atk = ApplyStage(stats.Attack, statStages[(int)PokemonStat.Attack - 1]);
            int def = ApplyStage(stats.Defense, statStages[(int)PokemonStat.Defense - 1]);
            int spAtk = ApplyStage(stats.SpecialAttack, statStages[(int)PokemonStat.SpecialAttack - 1]);
            int spDef = ApplyStage(stats.SpecialDefense, statStages[(int)PokemonStat.SpecialDefense - 1]);
            int speed = ApplyStage(stats.Speed, statStages[(int)PokemonStat.Speed - 1]);

            modifiedStats = new PokemonStats(hp, atk, def, spAtk, spDef, speed);
        }

        /// <summary>
        /// Calculates stat value based on stage multiplier.
        /// </summary>
        private int ApplyStage(int baseStat, int stage)
        {
            float multiplier = stage >= 0
                ? (2f + stage) / 2f
                : 2f / (2f - stage);

            return Mathf.Max(1, Mathf.RoundToInt(baseStat * multiplier));
        }
    }
}
