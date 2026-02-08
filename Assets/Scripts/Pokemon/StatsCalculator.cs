using MonsterTamer.Pokemon.Models;
using UnityEngine;

namespace MonsterTamer.Pokemon
{
    public static class StatsCalculator
    {
        // ---- Constants ------------------------------------------------------

        private const float BaseMultiplier = 0.01f;
        private const float EffortValueFactor = 0.25f;

        private const int HealthBaseOffset = 10;
        private const int StatBaseOffset = 5;

        private const int IVMin = 1;
        private const int IVMaxExclusive = 32;  // Unity Random.Range max is exclusive

        // ---- Public API ------------------------------------------------------

        /// <summary>
        /// Calculate fully derived Pokémon stats based on base stats, IVs, EVs, level, and nature.
        /// </summary>
        public static PokemonStats CalculateCoreStats(
            PokemonDefinition data,
            PokemonStats individualValues,
            PokemonStats effortValues,
            int level)
        {
            int healthPoint =
                Mathf.FloorToInt(
                    BaseMultiplier *
                    (2 * data.BaseStats.HealthPoint +
                     individualValues.HealthPoint +
                     Mathf.FloorToInt(EffortValueFactor * effortValues.HealthPoint)
                    ) * level
                ) + level + HealthBaseOffset;

            int attack = CalculateStat(data.BaseStats.Attack, individualValues.Attack, effortValues.Attack, level);
            int defense = CalculateStat(data.BaseStats.Defense, individualValues.Defense, effortValues.Defense, level);
            int specialAttack = CalculateStat(data.BaseStats.SpecialAttack, individualValues.SpecialAttack, effortValues.SpecialAttack, level);
            int specialDefense = CalculateStat(data.BaseStats.SpecialDefense, individualValues.SpecialDefense, effortValues.SpecialDefense, level);
            int speed = CalculateStat(data.BaseStats.Speed, individualValues.Speed, effortValues.Speed, level);

            return new PokemonStats(healthPoint, attack, defense, specialAttack, specialDefense, speed);
        }

        private static int CalculateStat(int baseStat, int iv, int ev, int level)
        {
            return Mathf.FloorToInt(
                       BaseMultiplier *
                       (2 * baseStat + iv + Mathf.FloorToInt(ev * EffortValueFactor))
                       * level
                   ) + StatBaseOffset;
        }

        /// <summary>
        /// Generate random IV values for all stats.
        /// </summary>
        public static PokemonStats GenerateIndividualValues()
        {
            return new PokemonStats(
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
