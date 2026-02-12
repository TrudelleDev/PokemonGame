using MonsterTamer.Monster.Enums;
using MonsterTamer.Monster.Models;
using MonsterTamer.Nature;
using UnityEngine;

namespace MonsterTamer.Monster
{
    /// <summary>
    /// Provides methods to calculate Monster stats, including IV/EV-based core stats and random IV generation.
    /// </summary>
    internal static class StatsCalculator
    {
        private const float BaseMultiplier = 0.01f;
        private const float EffortValueFactor = 0.25f;
        private const int HealthBaseOffset = 10;
        private const int StatBaseOffset = 5;
        private const int IVMin = 1;
        private const int IVMaxExclusive = 32;

        /// <summary>
        /// Calculates the core stats of a monster based on its base stats, IVs, EVs, level, and nature.
        /// </summary>
        /// <param name="definition">Monster definition containing base stats.</param>
        /// <param name="ivs">Individual values (IVs) of the monster.</param>
        /// <param name="evs">Effort values (EVs) of the monster.</param>
        /// <param name="level">Current level of the monster.</param>
        /// <param name="nature">Nature affecting stat multipliers.</param>
        /// <returns>Calculated <see cref="MonsterStats"/>.</returns>
        public static MonsterStats CalculateCoreStats(MonsterDefinition definition, MonsterStats ivs, MonsterStats evs, int level, NatureDefinition nature)
        {
            int hp = Mathf.FloorToInt(
                BaseMultiplier * (2 * definition.BaseStats.HealthPoint + ivs.HealthPoint + (evs.HealthPoint * EffortValueFactor)) * level
            ) + level + HealthBaseOffset;

            int atk = CalculateStandardStat(definition.BaseStats.Attack, ivs.Attack, evs.Attack, level, nature.GetMultiplier(MonsterStat.Attack));
            int def = CalculateStandardStat(definition.BaseStats.Defense, ivs.Defense, evs.Defense, level, nature.GetMultiplier(MonsterStat.Defense));
            int spatk = CalculateStandardStat(definition.BaseStats.SpecialAttack, ivs.SpecialAttack, evs.SpecialAttack, level, nature.GetMultiplier(MonsterStat.SpecialAttack));
            int spdef = CalculateStandardStat(definition.BaseStats.SpecialDefense, ivs.SpecialDefense, evs.SpecialDefense, level, nature.GetMultiplier(MonsterStat.SpecialDefense));
            int spd = CalculateStandardStat(definition.BaseStats.Speed, ivs.Speed, evs.Speed, level, nature.GetMultiplier(MonsterStat.Speed));

            return new MonsterStats(hp, atk, def, spatk, spdef, spd);
        }

        /// <summary>
        /// Calculates a standard non-HP stat, applying IVs, EVs, level, and nature multiplier.
        /// </summary>
        private static int CalculateStandardStat(int baseStat, int iv, int ev, int level, float natureMultiplier)
        {
            float val = (BaseMultiplier * (2 * baseStat + iv + (ev * EffortValueFactor)) * level) + StatBaseOffset;
            return Mathf.FloorToInt(val * natureMultiplier);
        }

        /// <summary>
        /// Generates random IVs for a monster.
        /// Each IV is between 1 (inclusive) and 32 (exclusive).
        /// </summary>
        /// <returns>A <see cref="MonsterStats"/> instance containing random IVs.</returns>
        public static MonsterStats GenerateRandomIVs()
        {
            return new MonsterStats(
                Random.Range(IVMin, IVMaxExclusive),
                Random.Range(IVMin, IVMaxExclusive),
                Random.Range(IVMin, IVMaxExclusive),
                Random.Range(IVMin, IVMaxExclusive),
                Random.Range(IVMin, IVMaxExclusive),
                Random.Range(IVMin, IVMaxExclusive)
            );
        }
    }
}
