using System;
using PokemonGame.Abilities;
using PokemonGame.Abilities.Definition;
using PokemonGame.Moves;
using PokemonGame.Moves.Definition;
using PokemonGame.Moves.Enums;
using PokemonGame.Pokemons.Definition;
using PokemonGame.Pokemons.Enums;
using PokemonGame.Pokemons.Models;
using PokemonGame.Pokemons.Natures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons
{
    /// <summary>
    /// Runtime Pokémon instance built from static definitions (species, nature, ability, moves).
    /// Holds dynamic state like level, stats, gender, health, and owner info.
    /// </summary>
    [Serializable]
    public class Pokemon
    {
        [BoxGroup("Progression")]
        [SerializeField, Required, Range(1, 100)]
        [Tooltip("The level of this Pokémon.")]
        private int level = 1;

        [BoxGroup("Definitions")]
        [SerializeField, Required]
        [Tooltip("Pokemon definition for this Pokémon.")]
        private PokemonDefinition pokemonDefinition;

        [BoxGroup("Definitions")]
        [SerializeField, Required]
        [Tooltip("Nature definition that affects stat growth.")]
        private NatureDefinition natureDefinition;

        [BoxGroup("Definitions")]
        [SerializeField, Required]
        [Tooltip("Ability definition for this Pokémon.")]
        private AbilityDefinition abilityDefinition;

        [BoxGroup("Definitions")]
        [SerializeField, Required, Space]
        [Tooltip("Moves known by this Pokémon.")]
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
        public StatusCondition Condition { get; private set; } = StatusCondition.None;

        public event Action<int, int> OnHealthChange; // oldHp, newHp
        public event Action<int, int> OnExperienceChange; // oldExp, newExp
        public event Action<int> OnLevelChange;
        public event Action<StatusCondition> OnStatusChange;

        public int CurrentExp { get; private set; }
        public int BaseExpYield { get; private set; }

        public Pokemon(int level, PokemonDefinition species, NatureDefinition natureDef, AbilityDefinition abilityDef, MoveDefinition[] moveDefs)
        {
            this.level = level;
            this.pokemonDefinition = species;
            this.natureDefinition = natureDef;
            this.abilityDefinition = abilityDef;
            this.moveDefinitions = moveDefs;

            CurrentExp = GetExpForCurrentLevel();

            GenerateAbility();
            GenerateNature();
            GenerateMoves();
            Initialize();
        }

        /// <summary>
        /// Creates a deep copy of this Pokémon instance with identical stats and moves.
        /// </summary>
        public Pokemon Clone()
        {
            var clonedMoves = (MoveDefinition[])moveDefinitions.Clone();
            return new Pokemon(level, pokemonDefinition, natureDefinition, abilityDefinition, clonedMoves);
        }

        /// <summary>
        /// Generates the Pokémon's Nature based on its nature definition.
        /// </summary>
        private void GenerateNature()
        {
            Nature = new Nature(natureDefinition);
        }

        /// <summary>
        /// Generates the Pokémon's Ability based on its ability definition.
        /// </summary>
        private void GenerateAbility()
        {
            Ability = new Ability(abilityDefinition);
        }

        /// <summary>
        /// Generates the Pokémon's Moves based on its move definitions.
        /// </summary>
        private void GenerateMoves()
        {
            Moves = new Move[moveDefinitions.Length];
            for (int i = 0; i < moveDefinitions.Length; i++)
            {
                Moves[i] = moveDefinitions[i] != null ? new Move(moveDefinitions[i]) : null;
            }
        }

        /// <summary>
        /// Initializes the Pokémon's stats, gender, ID, and health at creation.
        /// </summary>
        private void Initialize()
        {
            IndividualValue = StatsCalculator.GenerateIndividualValues();
            EffortValue = new PokemonStats(0, 0, 0, 0, 0, 0);
            CoreStat = StatsCalculator.CalculateCoreStats(pokemonDefinition, IndividualValue, EffortValue, level);
            GetGender();

            ID = idGenerator.GetID();
            HealthRemaining = CoreStat.HealthPoint;

            OnHealthChange?.Invoke(HealthRemaining, HealthRemaining);
        }

        /// <summary>
        /// Determines the Pokémon's gender based on its species' gender ratio.
        /// </summary>
        private void GetGender()
        {
            float roll = UnityEngine.Random.Range(0f, 100f);
            Gender = roll < pokemonDefinition.GenderRatio.MaleRatio ? PokemonGender.Male : PokemonGender.Female;
        }

        /// <summary>
        /// Adds experience to the Pokémon and triggers level-up if threshold is reached.
        /// </summary>
        public void AddExperience(int amount)
        {
            int oldExp = CurrentExp;
            CurrentExp += amount;

            OnExperienceChange?.Invoke(oldExp, CurrentExp);

            while (ShouldLevelUp())
            {
                LevelUp();
            }
        }

        /// <summary>
        /// Calculates experience points gained from defeating another Pokémon.
        /// </summary>
        /// <param name="opponent">The opposing Pokémon defeated.</param>
        /// <returns>Amount of experience gained.</returns>
        public int CalculateExpGain(Pokemon opponent)
        {
            const float LevelMultiplier = 5f;
            const float BaseReward = 10f;

            int exp = Mathf.RoundToInt(opponent.Level * LevelMultiplier + BaseReward);
            return exp;
        }

        /// <summary>
        /// Gets the total experience required for the Pokémon's current level.
        /// </summary>
        public int GetExpForCurrentLevel()
        {
            return Mathf.FloorToInt(Mathf.Pow(Level, 3));
        }

        /// <summary>
        /// Gets the total experience required to reach the next level.
        /// </summary>
        public int GetExpForNextLevel()
        {
            return Mathf.FloorToInt(Mathf.Pow(Level + 1, 3));
        }

        /// <summary>
        /// Determines if the Pokémon has enough experience to level up.
        /// </summary>
        public bool ShouldLevelUp()
        {
            int requiredExp = GetExpForNextLevel();
            return CurrentExp >= requiredExp;
        }

        /// <summary>
        /// Increases the Pokémon's level by one and triggers level change event.
        /// </summary>
        private void LevelUp()
        {
            level++;
            OnLevelChange.Invoke(level);
        }

        /// <summary>
        /// Calculates the damage dealt by a move against a target Pokémon.
        /// </summary>
        /// <param name="move">The move used by this Pokémon.</param>
        /// <param name="target">The target Pokémon receiving damage.</param>
        /// <returns>The amount of damage inflicted.</returns>
        public int Attack(Move move, Pokemon target)
        {
            int attackStat = 0;
            int defenseStat = 0;

            if (move.Definition.Category == MoveCategory.Physical)
            {
                attackStat = CoreStat.Attack;
                defenseStat = target.CoreStat.Defense;
            }
            else if (move.Definition.Category == MoveCategory.Special)
            {
                attackStat = CoreStat.SpecialAttack;
                defenseStat = target.CoreStat.SpecialDefense;
            }

            float baseDamage = move.Definition.Power * ((float)attackStat / defenseStat);
            int finalDamage = Mathf.Max(1, Mathf.RoundToInt(baseDamage)) / 10;

            return finalDamage;
        }

        /// <summary>
        /// Reduces the Pokémon's HP by the specified amount of damage.
        /// </summary>
        /// <param name="amount">Amount of damage to apply.</param>
        /// <returns>The actual damage applied.</returns>
        public int TakeDamage(int amount)
        {
            if (amount <= 0 || HealthRemaining <= 0)
            {
                return 0;
            }

            int oldHp = HealthRemaining;
            int actualDamage = Mathf.Min(amount, HealthRemaining);
            HealthRemaining -= actualDamage;

            OnHealthChange?.Invoke(oldHp, HealthRemaining);

            if (HealthRemaining <= 0)
            {
                Debug.Log($"{Definition.DisplayName} fainted!");
            }

            return actualDamage;
        }

        /// <summary>
        /// Restores the Pokémon's HP by the specified amount, up to its maximum health.
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

        /// <summary>
        /// Attempts to cure the Pokémon's status condition if it matches the specified one.
        /// </summary>
        /// <param name="condition">The status condition to attempt to cure.</param>
        /// <returns>
        /// True if the Pokémon had the specified condition and it was cured; 
        /// false if the Pokémon did not have that condition.
        /// </returns>
        public bool TryCureStatus(StatusCondition condition)
        {
            if (Condition != condition)
            {
                return false;
            }

            Condition = StatusCondition.None;
            OnStatusChange?.Invoke(Condition);

            return true;
        }
    }
}

