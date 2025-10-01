using System;
using PokemonGame.Abilities;
using PokemonGame.Abilities.Definition;
using PokemonGame.Moves;
using PokemonGame.Moves.Definition;
using PokemonGame.Pokemons.Definition;
using PokemonGame.Pokemons.Enums;
using PokemonGame.Pokemons.Models;
using PokemonGame.Pokemons.Natures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons
{
    /// <summary>
    /// Runtime Pok�mon instance built from static definitions (species, nature, ability, moves).
    /// Holds dynamic state like level, stats, gender, health, and owner info.
    /// </summary>
    [Serializable]
    public class Pokemon
    {
        [BoxGroup("Progression")]
        [SerializeField, Required, Range(1, 100)]
        [Tooltip("The level of this Pok�mon.")]
        private int level = 1;

        [BoxGroup("Definitions")]
        [SerializeField, Required]
        [Tooltip("Pokemon definition for this Pok�mon.")]
        private PokemonDefinition pokemonDefinition;

        [BoxGroup("Definitions")]
        [SerializeField, Required]
        [Tooltip("Nature definition that affects stat growth.")]
        private NatureDefinition natureDefinition;

        [BoxGroup("Definitions")]
        [SerializeField, Required]
        [Tooltip("Ability definition for this Pok�mon.")]
        private AbilityDefinition abilityDefinition;

        [BoxGroup("Definitions")]
        [SerializeField, Required, Space]
        [Tooltip("Moves known by this Pok�mon.")]
        private MoveDefinition[] moveDefinitions;

        private static readonly IDGenerator idGenerator = new IDGenerator(1000, 9999);

        public PokemonDefinition Definition => pokemonDefinition;
        public Nature Nature { get; private set; }
        public Ability Ability { get; private set; }
        public Move[] Moves { get; private set; }

        public int Level => level;
        public PokemonStats CoreStat { get; private set; }
        public PokemonStats IndividualValue { get; private set; }
        public PokemonStats EffortValue { get; private set; }
        public PokemonGender Gender { get; private set; }
        public int HealthRemaining { get; private set; }
        public string OwnerName { get; set; } = "RED";
        public string LocationEncounter { get; set; } = "Pallet Town";
        public string ID { get; private set; }
        public int MaxHealth => CoreStat.HealthPoint;

        public event Action<int, int> OnHealthChange; // oldHp, newHp

        public Pokemon(int level, PokemonDefinition species, NatureDefinition natureDef, AbilityDefinition abilityDef, MoveDefinition[] moveDefs)
        {
            this.level = level;
            this.pokemonDefinition = species;
            this.natureDefinition = natureDef;
            this.abilityDefinition = abilityDef;
            this.moveDefinitions = moveDefs;

            GenerateAbility();
            GenerateNature();
            GenerateMoves();
            Initialize();
        }

        public Pokemon Clone()
        {
            var clonedMoves = (MoveDefinition[])moveDefinitions.Clone();
            return new Pokemon(level, pokemonDefinition, natureDefinition, abilityDefinition, clonedMoves);
        }

        private void GenerateNature()
        {
            Nature = new Nature(natureDefinition);
        }

        private void GenerateAbility()
        {
            Ability = new Ability(abilityDefinition);
        }

        private void GenerateMoves()
        {
            Moves = new Move[moveDefinitions.Length];
            for (int i = 0; i < moveDefinitions.Length; i++)
            {
                Moves[i] = moveDefinitions[i] != null ? new Move(moveDefinitions[i]) : null;
            }
        }

        private void Initialize()
        {
            IndividualValue = StatsCalculator.GenerateIndividualValues();
            EffortValue = new PokemonStats(0, 0, 0, 0, 0, 0);
            CoreStat = StatsCalculator.CalculateCoreStats(pokemonDefinition, IndividualValue, EffortValue, level);
            GetGender();

            ID = idGenerator.GetID();
            //  HealthRemaining = CoreStat.HealthPoint;

            HealthRemaining = 1;

            // Old health and new health are the same during init
            OnHealthChange?.Invoke(HealthRemaining, HealthRemaining);
        }

        private void GetGender()
        {
            float roll = UnityEngine.Random.Range(0f, 100f);
            Gender = roll < pokemonDefinition.GenderRatio.MaleRatio ? PokemonGender.Male : PokemonGender.Female;
        }

        /// <summary>
        /// Restores HP up to the Pok�mon's max health.
        /// </summary>
        /// <param name="amount">The amount of HP to restore.</param>
        /// <returns>The actual amount of HP restored.</returns>
        public int RestoreHP(int amount)
        {
            if (amount <= 0 || HealthRemaining >= MaxHealth)
            {
                return 0;
            }

            int old = HealthRemaining;
            HealthRemaining = Mathf.Min(HealthRemaining + amount, MaxHealth);
            int healed = HealthRemaining - old;

            if (healed > 0)
            {
                OnHealthChange?.Invoke(old, HealthRemaining);
            }

            return healed;
        }
    }
}

