using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    /// <summary>
    /// Represents the base stats of a Pokémon, including HP, Attack, Defense,
    /// Special Attack, Special Defense, Speed, and a calculated total.
    /// </summary>
    [Serializable]
    public struct PokemonStats
    {
        [SerializeField, Tooltip("Base HP (Hit Points) stat.")]
        private int healthPoint;

        [SerializeField, Tooltip("Base Attack stat.")]
        private int attack;

        [SerializeField, Tooltip("Base Defense stat.")]
        private int defense;

        [SerializeField, Tooltip("Base Special Attack stat.")]
        private int specialAttack;

        [SerializeField, Tooltip("Base Special Defense stat.")]
        private int specialDefense;

        [SerializeField, Tooltip("Base Speed stat.")]
        private int speed;

        [ShowInInspector, ReadOnly]
        [Tooltip("The sum of all base stats.")]
        public readonly int Total => healthPoint + attack + defense + specialAttack + specialDefense + speed;

        public readonly int HealthPoint => healthPoint;
        public readonly int Attack => attack;
        public readonly int Defense => defense;
        public readonly int SpecialAttack => specialAttack;
        public readonly int SpecialDefense => specialDefense;
        public readonly int Speed => speed;

        /// <summary>
        /// Initializes a new instance of <see cref="PokemonStats"/> with specific stat values.
        /// </summary>
        /// <param name="healthPoint">The base HP value.</param>
        /// <param name="attack">The base Attack value.</param>
        /// <param name="defense">The base Defense value.</param>
        /// <param name="specialAttack">The base Special Attack value.</param>
        /// <param name="specialDefense">The base Special Defense value.</param>
        /// <param name="speed">The base Speed value.</param>
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
