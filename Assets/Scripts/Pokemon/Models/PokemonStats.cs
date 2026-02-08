using System;
using MonsterTamer.Pokemon.Enums;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MonsterTamer.Pokemon.Models
{
    /// <summary>
    /// Represents a Pokémon's core stats: Hit Points (HP), Attack, Defense,
    /// Special Attack, Special Defense, and Speed. 
    /// Provides read-only access to individual stats and allows modification.
    /// </summary>
    [Serializable]
    public struct PokemonStats
    {
        [SerializeField, Tooltip("Hit Points (HP) stat value.")]
        private int healthPoint;
        [SerializeField, Tooltip("Attack stat value.")]
        private int attack;
        [SerializeField, Tooltip("Defense stat value.")]
        private int defense;
        [SerializeField, Tooltip("Special Attack stat value.")]
        private int specialAttack;
        [SerializeField, Tooltip("Special Defense stat value.")]
        private int specialDefense;
        [SerializeField, Tooltip("Speed stat value.")]
        private int speed;

        [ShowInInspector, ReadOnly, Tooltip("Sum of all stat values.")]
        public readonly int Total => healthPoint + attack + defense + specialAttack + specialDefense + speed;

        public readonly int HealthPoint => healthPoint;
        public readonly int Attack => attack;
        public readonly int Defense => defense;
        public readonly int SpecialAttack => specialAttack;
        public readonly int SpecialDefense => specialDefense;
        public readonly int Speed => speed;

        public PokemonStats(int healthPoint, int attack, int defense, int specialAttack, int specialDefense, int speed)
        {
            this.healthPoint = healthPoint;
            this.attack = attack;
            this.defense = defense;
            this.specialAttack = specialAttack;
            this.specialDefense = specialDefense;
            this.speed = speed;
        }

        /// <summary>
        /// Accesses a stat by its PokemonStat enum.
        /// Allows internal modification through the setter.
        /// </summary>
        /// <param name="stat">The stat to get or set.</param>
        /// <returns>The value of the specified stat.</returns>
        public int this[PokemonStat stat]
        {
            readonly get => stat switch
            {
                PokemonStat.HealthPoint => healthPoint,
                PokemonStat.Attack => attack,
                PokemonStat.Defense => defense,
                PokemonStat.SpecialAttack => specialAttack,
                PokemonStat.SpecialDefense => specialDefense,
                PokemonStat.Speed => speed,
                _ => throw new ArgumentOutOfRangeException(nameof(stat), stat, null)
            };
            set
            {
                switch (stat)
                {
                    case PokemonStat.HealthPoint: healthPoint = value; break;
                    case PokemonStat.Attack: attack = value; break;
                    case PokemonStat.Defense: defense = value; break;
                    case PokemonStat.SpecialAttack: specialAttack = value; break;
                    case PokemonStat.SpecialDefense: specialDefense = value; break;
                    case PokemonStat.Speed: speed = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(stat), stat, null);
                }
            }
        }
    }
}
