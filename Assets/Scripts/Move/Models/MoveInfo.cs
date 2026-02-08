using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Move.Models
{
    /// <summary>
    /// Represents the numerical attributes of a Monster move,
    /// including base power, accuracy, and maximum uses (PP).
    /// </summary>
    [Serializable]
    internal struct MoveInfo
    {
        [SerializeField, Required, Tooltip("Base power of the move (0 if status move).")]
        private int power;

        [SerializeField, Required, Tooltip("Accuracy percentage of the move (0–100).")]
        private int accuracy;

        [SerializeField, Required, Tooltip("Maximum number of times the move can be used.")]
        private int powerPoint;

        /// <summary>
        /// Base power of the move (0 if status move).
        /// </summary>
        internal readonly int Power => power;

        /// <summary>
        /// Accuracy of the move as a percentage (0–100).
        /// </summary>
        internal readonly int Accuracy => accuracy;

        /// <summary>
        /// Maximum number of times the move can be used
        /// </summary>
        internal readonly int PowerPoint => powerPoint;
    }
}
