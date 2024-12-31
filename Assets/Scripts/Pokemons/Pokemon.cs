using PokemonGame.Pokemons.Abilities;
using PokemonGame.Pokemons.Data;
using PokemonGame.Pokemons.Moves;
using PokemonGame.Pokemons.Natures;
using System;
using UnityEngine;

namespace PokemonGame.Pokemons
{
    [Serializable]
    public class Pokemon
    {
        [SerializeField] private int level;
        [SerializeField] private PokemonData data;
        [SerializeField] private Nature nature;
        [SerializeField] private Ability abiliity;
        [SerializeField] private Move[] moves;

        public int Level => level;

        public PokemonData Data => data;
        public Nature Nature => nature;
        public Ability Ability => abiliity;
        public Move[] Moves => moves;


        public PokemonStats CoreStat { get; private set; }
        public PokemonStats IndividualValue { get; private set; }
        public PokemonStats EffortValue { get; private set; }
        public PokemonGender Gender { get; private set; }
        public float HealthRemaining { get; private set; }
        public string OwnerName { get; set; }
        public string LocationEncounter { get; set; } = "Pallet Town";
        public string ID { get; private set; }

        public event Action<float> OnHealthChange;

        public Pokemon(int level, PokemonData data, Nature nature, Ability ability, Move[] moves)
        {
            this.level = level;
            this.data = data;
            this.nature = nature;
            this.abiliity = ability;
            this.moves = moves;

            this.moves = new Move[moves.Length];

            for (int i = 0; i < this.moves.Length; i++)
            {
                this.moves[i] = new Move(moves[i].Data);
            }

            GetIndividualValues();
            GetCoreStats();
            GetGender();

            HealthRemaining = CoreStat.HealthPoint;

            ID = IDGenerator.GetID();

            OnHealthChange?.Invoke(HealthRemaining);

        }

        private void GetGender()
        {
            float random = UnityEngine.Random.Range(1f, 100f);

            if (random < Data.GenderRatio.MaleRatio)
            {
                Gender = PokemonGender.Male;
            }
            else
            {
                Gender = PokemonGender.Female;
            }
        }

        private void GetCoreStats()
        {
            // Formula: https://pokemon.fandom.com/wiki/Statistics#:~:text=chance%20of%20hitting.-,Formula,Level)%20%2B%205)%20x%20Nature

            int healthPoint = Mathf.FloorToInt(0.01f * (2 * Data.BaseStats.HealthPoint + IndividualValue.HealthPoint + Mathf.FloorToInt(0.25f * EffortValue.HealthPoint)) * Level) + Level + 10;
            int attack = Mathf.FloorToInt(0.01f * (2 * Data.BaseStats.Attack + IndividualValue.Attack + Mathf.FloorToInt(0.25f * EffortValue.Attack)) * Level) + 5;
            int defense = Mathf.FloorToInt(0.01f * (2 * Data.BaseStats.Defense + IndividualValue.Defense + Mathf.FloorToInt(0.25f * EffortValue.Defense)) * Level) + 5;
            int specialAttack = Mathf.FloorToInt(0.01f * (2 * Data.BaseStats.SpecialAttack + IndividualValue.SpecialAttack + Mathf.FloorToInt(0.25f * EffortValue.SpecialAttack)) * Level) + 5;
            int specialDefense = Mathf.FloorToInt(0.01f * (2 * Data.BaseStats.SpecialDefense + IndividualValue.SpecialDefense + Mathf.FloorToInt(0.25f * EffortValue.SpecialDefense)) * Level) + 5;
            int speed = Mathf.FloorToInt(0.01f * (2 * Data.BaseStats.Speed + IndividualValue.Speed + Mathf.FloorToInt(0.25f * EffortValue.Speed)) * Level) + 5;

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

