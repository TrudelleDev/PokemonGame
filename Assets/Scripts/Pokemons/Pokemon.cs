using System;
using PokemonGame.Abilities;
using PokemonGame.Abilities.Definition;
using PokemonGame.Abilities.Enums;
using PokemonGame.Moves;
using PokemonGame.Moves.Definition;
using PokemonGame.Moves.Enums;
using PokemonGame.Natures.Enums;
using PokemonGame.Pokemons.Definition;
using PokemonGame.Pokemons.Enums;
using PokemonGame.Pokemons.Models;
using PokemonGame.Pokemons.Natures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons
{
    [Serializable]
    public class Pokemon
    {
        [SerializeField, Required] private int level;
        [SerializeField, Required] private PokemonId pokemonId;
        [SerializeField, Required] private NatureId natureId;
        [SerializeField, Required] private AbilityId abilityId;
        [SerializeField, Required] private MoveId[] moveIds;

        private static readonly IDGenerator idGenerator = new IDGenerator(1000, 9999);

        public PokemonDefinition Definition { get; private set; }
        public Nature Nature { get; private set; }
        public Ability Ability { get; private set; }
        public Move[] Moves { get; private set; }

        public int Level => level;
        public PokemonStats CoreStat { get; private set; }
        public PokemonStats IndividualValue { get; private set; }
        public PokemonStats EffortValue { get; private set; }
        public PokemonGender Gender { get; private set; }
        public float HealthRemaining { get; private set; }
        public string OwnerName { get; set; } = "RED";
        public string LocationEncounter { get; set; } = "Pallet Town";
        public string ID { get; private set; }

        public event Action<float> OnHealthChange;

        public Pokemon(int level, PokemonId pokemonId, NatureId natureId, AbilityId abilityId, MoveId[] moveIds)
        {
            this.level = level;
            this.pokemonId = pokemonId;
            this.natureId = natureId;
            this.abilityId = abilityId;
            this.moveIds = moveIds;

            GenerateAbility();
            GenerateNature();
            GetPokemonDefinition();
            GenerateMoves();

            Initialize();
        }

        public Pokemon Clone()
        {
            var moves = (MoveId[])moveIds.Clone();
            return new Pokemon(level, pokemonId, natureId, abilityId, moves);
        }

        private void GenerateNature()
        {
            if (!NatureDefinitionLoader.TryGet(natureId, out var natureDefinition))
            {
                Debug.LogError($"Missing Nature definition for {natureId}");
                return;
            }

            Nature = new Nature(natureDefinition);
        }

        private void GenerateAbility()
        {
            if (!AbilityDefinitionLoader.TryGet(abilityId, out var abilityDefinition))
            {
                Debug.LogError($"Missing Ability definition for {abilityId}");
                return;
            }

            Ability = new Ability(abilityDefinition);
        }

        private void GetPokemonDefinition()
        {
            if (!PokemonDefinitionLoader.TryGet(pokemonId, out var pokemonDefinition))
            {
                Debug.LogError($"Missing Pokemon definition for {pokemonId}");
                return;
            }

            Definition = pokemonDefinition;
        }

        private void GenerateMoves()
        {
            Moves = new Move[moveIds.Length];

            for (int i = 0; i < moveIds.Length; i++)
            {
                if (MoveDefinitionLoader.TryGet(moveIds[i], out var moveDef))
                {
                    Moves[i] = new Move(moveDef);
                }
                else
                {
                    Debug.LogWarning($"Missing Move definition for {moveIds[i]}");
                    Moves[i] = null; // or a default "Empty Move" instance
                }
            }
        }
        private void Initialize()
        {
            IndividualValue = StatsCalculator.GenerateIndividualValues();
            EffortValue = new PokemonStats(0, 0, 0, 0, 0, 0);
            CoreStat = StatsCalculator.CalculateCoreStats(Definition, IndividualValue, EffortValue, level);
            GetGender();

            ID = idGenerator.GetID();
            HealthRemaining = CoreStat.HealthPoint;
            OnHealthChange?.Invoke(HealthRemaining);
        }

        private void GetGender()
        {
            float roll = UnityEngine.Random.Range(0f, 100f);
            Gender = roll < Definition.GenderRatio.MaleRatio ? PokemonGender.Male : PokemonGender.Female;
        }
    }
}
