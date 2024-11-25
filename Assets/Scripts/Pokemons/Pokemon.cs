using PokemonGame.Pokemons.Ability;
using PokemonGame.Pokemons.Data;
using PokemonGame.Pokemons.Nature;
using System;
using UnityEngine;

namespace PokemonGame.Pokemons
{
    [Serializable]
    public class Pokemon
    {
        [SerializeField] private int level;
        [SerializeField] private PokemonData pokemonData;
        [SerializeField] private NatureData natureData;
        [SerializeField] private AbilityData abilityData;

        public int Level => level;
        public PokemonData PokemonData => pokemonData;
        public NatureData NatureData => natureData;
        public AbilityData AbilityData => abilityData;
        public PokemonStats CoreStat { get; private set; }
        public PokemonStats IndividualValue { get; private set; }
        public PokemonStats EffortValue { get; private set; }
        public Gender Gender { get; private set; }
        public float RemainingHealth { get; private set; }

        public event Action<float> OnHealthChange;


        public Pokemon(int level, PokemonData pokemonData, NatureData natureData, AbilityData abilityData)
        {
            this.level = level;
            this.pokemonData = pokemonData;
            this.natureData = natureData;
            this.abilityData = abilityData;

            GetIndividualValues();
            GetCoreStats();
            GetGender();

            RemainingHealth = CoreStat.HealthPoint;

            OnHealthChange?.Invoke(RemainingHealth);

        }

        private void GetGender()
        {
            float random = UnityEngine.Random.Range(1f, 100f);

            Gender = random < PokemonData.GenderRatio.MaleRatio ? Gender.Male : Gender.Female;
        }

        private void GetCoreStats()
        {
            // Formula: https://pokemon.fandom.com/wiki/Statistics#:~:text=chance%20of%20hitting.-,Formula,Level)%20%2B%205)%20x%20Nature

            int healthPoint = Mathf.FloorToInt(0.01f * (2 * PokemonData.BaseStats.HealthPoint + IndividualValue.HealthPoint + Mathf.FloorToInt(0.25f * EffortValue.HealthPoint)) * Level) + Level + 10;
            int attack = Mathf.FloorToInt(0.01f * (2 * PokemonData.BaseStats.Attack + IndividualValue.Attack + Mathf.FloorToInt(0.25f * EffortValue.Attack)) * Level) + 5;
            int defense = Mathf.FloorToInt(0.01f * (2 * PokemonData.BaseStats.Defense + IndividualValue.Defense + Mathf.FloorToInt(0.25f * EffortValue.Defense)) * Level) + 5;
            int specialAttack = Mathf.FloorToInt(0.01f * (2 * PokemonData.BaseStats.SpecialAttack + IndividualValue.SpecialAttack + Mathf.FloorToInt(0.25f * EffortValue.SpecialAttack)) * Level) + 5;
            int specialDefense = Mathf.FloorToInt(0.01f * (2 * PokemonData.BaseStats.SpecialDefense + IndividualValue.SpecialDefense + Mathf.FloorToInt(0.25f * EffortValue.SpecialDefense)) * Level) + 5;
            int speed = Mathf.FloorToInt(0.01f * (2 * PokemonData.BaseStats.Speed + IndividualValue.Speed + Mathf.FloorToInt(0.25f * EffortValue.Speed)) * Level) + 5;

            CoreStat = new PokemonStats(healthPoint, attack, defense, specialAttack, specialDefense, speed);
        }

        private void GetIndividualValues()
        {
            int healtPoint = UnityEngine.Random.Range(1, 32);
            int attack = UnityEngine.Random.Range(1, 32);
            int defense = UnityEngine.Random.Range(1, 32);
            int specialAttack = UnityEngine.Random.Range(1, 32);
            int specialDefense = UnityEngine.Random.Range(1, 32);
            int speed = UnityEngine.Random.Range(1, 32);

            IndividualValue = new PokemonStats(healtPoint, attack, defense, specialAttack, specialDefense, speed);
        }
    }
}

