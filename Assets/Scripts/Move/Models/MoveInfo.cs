using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Move.Models
{
    /// <summary>
    /// Contains the numerical stats of a Pokémon move, including its base power,
    /// accuracy, and maximum number of uses (PP).
    /// </summary>
    [Serializable]
    public struct MoveInfo
    {
        [SerializeField, Required, Tooltip("Base power of the move (0 if status move).")]
        private int power;

        [SerializeField, Required, Tooltip("Accuracy percentage of the move (0–100).")]
        private int accuracy;

        [SerializeField, Required, Tooltip("Maximum number of times the move can be used.")]
        private int powerPoint;

        public readonly int Power => power;
        public readonly int Accuracy => accuracy;
        public readonly int PowerPoint => powerPoint;
    }
}
