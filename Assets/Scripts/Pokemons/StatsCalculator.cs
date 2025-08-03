using PokemonGame.Pokemons.Definition;
using PokemonGame.Pokemons.Models;
using UnityEngine;

namespace PokemonGame.Pokemons
{
    public static class StatsCalculator
    {
        /// <summary>
        /// Calculate Core Stats based on Pokemon data, IVs, EVs, level, and nature.
        /// </summary>
        public static PokemonStats CalculateCoreStats(PokemonDefinition data, PokemonStats individualValues, PokemonStats effortValues, int level)
        {
            int healthPoint = Mathf.FloorToInt(0.01f * (2 * data.BaseStats.HealthPoint + individualValues.HealthPoint + Mathf.FloorToInt(0.25f * effortValues.HealthPoint)) * level) + level + 10;
            int attack = CalculateStat(data.BaseStats.Attack, individualValues.Attack, effortValues.Attack, level);
            int defense = CalculateStat(data.BaseStats.Defense, individualValues.Defense, effortValues.Defense, level);
            int specialAttack = CalculateStat(data.BaseStats.SpecialAttack, individualValues.SpecialAttack, effortValues.SpecialAttack, level);
            int specialDefense = CalculateStat(data.BaseStats.SpecialDefense, individualValues.SpecialDefense, effortValues.SpecialDefense, level);
            int speed = CalculateStat(data.BaseStats.Speed, individualValues.Speed, effortValues.Speed, level);

            return new PokemonStats(healthPoint, attack, defense, specialAttack, specialDefense, speed);
        }

        private static int CalculateStat(int baseStat, int iv, int ev, int level)
        {
            int stat = Mathf.FloorToInt(0.01f * (2 * baseStat + iv + Mathf.FloorToInt(ev * 0.25f)) * level) + 5;
            return stat;
        }

        /// <summary>
        /// Generate random Individual Values (IVs) for a Pokemon.
        /// </summary>
        public static PokemonStats GenerateIndividualValues()
        {
            int healthPoint = UnityEngine.Random.Range(1, 32);
            int attack = UnityEngine.Random.Range(1, 32);
            int defense = UnityEngine.Random.Range(1, 32);
            int specialAttack = UnityEngine.Random.Range(1, 32);
            int specialDefense = UnityEngine.Random.Range(1, 32);
            int speed = UnityEngine.Random.Range(1, 32);

            return new PokemonStats(healthPoint, attack, defense, specialAttack, specialDefense, speed);
        }
    }
}
