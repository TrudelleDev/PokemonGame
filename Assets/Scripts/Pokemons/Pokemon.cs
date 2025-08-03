using System;
using PokemonGame.Abilities;
using PokemonGame.Abilities.Definition;
using PokemonGame.Pokemons.Definition;
using PokemonGame.Pokemons.Enums;
using PokemonGame.Pokemons.Models;
using PokemonGame.Pokemons.Moves;
using PokemonGame.Pokemons.Natures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons
{
    [Serializable]
    public class Pokemon
    {
        [SerializeField, Required] private int level;
        [SerializeField, Required] private PokemonDefinition data;
        [SerializeField, Required] private NatureData natureData;
        [SerializeField, Required] private AbilityDefinition abilityData;
        [SerializeField, Required] private MoveData[] movesData;

        private static readonly IDGenerator idGenerator = new IDGenerator(1000, 9999);
        public int Level => level;

        public PokemonDefinition Data => data;

        public Nature Nature { get; private set; }
        public Ability Ability { get; private set; }
        public Move[] Moves { get; private set; }

        public PokemonStats CoreStat { get; private set; }
        public PokemonStats IndividualValue { get; private set; }
        public PokemonStats EffortValue { get; private set; }
        public PokemonGender Gender { get; private set; }
        public float HealthRemaining { get; private set; }
        public string OwnerName { get; set; } = "RED"; // Change this with real character name
        public string LocationEncounter { get; set; } = "Pallet Town";
        public string ID { get; private set; }

        public event Action<float> OnHealthChange;

        public Pokemon(int level, PokemonDefinition pokemonData, NatureData natureData, AbilityDefinition abilityData, MoveData[] movesData)
        {
            this.level = level;
            this.data = pokemonData;
            this.natureData = natureData;
            this.abilityData = abilityData;
            this.movesData = movesData;

            Nature = new Nature(natureData);
            Ability = new Ability(abilityData);
            Moves = new Move[movesData.Length];

            for (int i = 0; i < movesData.Length; i++)
            {
                Moves[i] = new Move(movesData[i]);
            }

            if (data != null)
                Initialize();
        }

        public Pokemon Clone()
        {
            return new Pokemon(level, data, natureData, abilityData, movesData);
        }

        private void Initialize()
        {
            IndividualValue = StatsCalculator.GenerateIndividualValues();
            EffortValue = new PokemonStats(0, 0, 0, 0, 0, 0);
            CoreStat = StatsCalculator.CalculateCoreStats(data, IndividualValue, EffortValue, level);
            GetGender();

            ID = idGenerator.GetID();

            HealthRemaining = CoreStat.HealthPoint;

            OnHealthChange?.Invoke(HealthRemaining);
        }

        private void GetGender()
        {
            float roll = UnityEngine.Random.Range(0f, 100f);
            Gender = roll < data.GenderRatio.MaleRatio ? PokemonGender.Male : PokemonGender.Female;
        }
    }
}

