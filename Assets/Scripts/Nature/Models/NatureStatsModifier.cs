using System;
using MonsterTamer.Pokemon.Enums;
using MonsterTamer.Pokemon.Models;
using UnityEngine;

namespace MonsterTamer.Nature.Models
{
    /// <summary>
    /// Defines how a nature modifies a Pokémon's stats.
    /// Each nature increases one stat by 10% and decreases another by 10%.
    /// </summary>
    [Serializable]
    public struct NatureStatsModifier
    {
        public const float IncreaseMultiplier = 1.1f;
        public const float DecreaseMultiplier = 0.9f;

        [SerializeField, Tooltip("The stat this nature increases.")]
        private PokemonStat increase;
        [SerializeField, Tooltip("The stat this nature decreases.")]
        private PokemonStat decrease;

        /// <summary>
        /// Applies the nature's stat modifications to the Pokémon's stats.
        /// </summary>
        /// <param name="stats">The stats to modify.</param>
        public readonly void Apply(ref PokemonStats stats)
        {
            if (increase != PokemonStat.None)
            {
                stats[increase] = Mathf.FloorToInt(stats[increase] * IncreaseMultiplier);
            }

            if (decrease != PokemonStat.None)
            {
                stats[decrease] = Mathf.FloorToInt(stats[decrease] * DecreaseMultiplier);
            }
        }
    }
}
