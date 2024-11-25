using PokemonGame.Attributes;
using System;
using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    [Serializable]
    public struct PokemonStats
    {
        [SerializeField] private int healthPoint;
        [SerializeField] private int attack;
        [SerializeField] private int defense;
        [SerializeField] private int specialAttack;
        [SerializeField] private int specialDefense;
        [SerializeField] private int speed;
        [Space]
        [SerializeField, ReadOnly] private int total;

        public readonly int HealthPoint => healthPoint;
        public readonly int Attack => attack;
        public readonly int Defense => defense;
        public readonly int SpecialAttack => specialAttack;
        public readonly int SpecialDefense => specialDefense;
        public readonly int Speed => speed;
        public readonly int Total => total;

        public PokemonStats(int healthPoint, int attack, int defense, int specialAttack, int specialDefense, int speed)
        {
            this.healthPoint = healthPoint;
            this.attack = attack;
            this.defense = defense;
            this.specialAttack = specialAttack;
            this.specialDefense = specialDefense;
            this.speed = speed;
            this.total = healthPoint + attack + defense + specialAttack + specialDefense + speed;
        }
    }
}