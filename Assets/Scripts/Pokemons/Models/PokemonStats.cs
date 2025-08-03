using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Models
{
    /// <summary>
    /// Contains a Pokémon's set of stats: HP, Attack, Defense, Special Attack, Special Defense, and Speed.
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

        /// <summary>
        /// The sum of all stat values.
        /// </summary>
        [ShowInInspector, ReadOnly]
        [Tooltip("Sum of all stat values.")]
        public readonly int Total => healthPoint + attack + defense + specialAttack + specialDefense + speed;

        /// <summary>
        /// The Hit Points (HP) stat value.
        /// </summary>
        public readonly int HealthPoint => healthPoint;

        /// <summary>
        /// The Attack stat value.
        /// </summary>
        public readonly int Attack => attack;

        /// <summary>
        /// The Defense stat value.
        /// </summary>
        public readonly int Defense => defense;

        /// <summary>
        /// The Special Attack stat value.
        /// </summary>
        public readonly int SpecialAttack => specialAttack;

        /// <summary>
        /// The Special Defense stat value.
        /// </summary>
        public readonly int SpecialDefense => specialDefense;

        /// <summary>
        /// The Speed stat value.
        /// </summary>
        public readonly int Speed => speed;

        /// <summary>
        /// Initializes a new instance of PokemonStats with specified stat values.
        /// </summary>
        /// <param name="healthPoint">HP stat value.</param>
        /// <param name="attack">Attack stat value.</param>
        /// <param name="defense">Defense stat value.</param>
        /// <param name="specialAttack">Special Attack stat value.</param>
        /// <param name="specialDefense">Special Defense stat value.</param>
        /// <param name="speed">Speed stat value.</param>
        public PokemonStats(int healthPoint, int attack, int defense, int specialAttack, int specialDefense, int speed)
        {
            this.healthPoint = healthPoint;
            this.attack = attack;
            this.defense = defense;
            this.specialAttack = specialAttack;
            this.specialDefense = specialDefense;
            this.speed = speed;
        }
    }
}
