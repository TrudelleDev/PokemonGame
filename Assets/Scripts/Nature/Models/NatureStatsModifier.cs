using System;
using MonsterTamer.Monster.Enums;
using UnityEngine;

namespace MonsterTamer.Nature.Models
{
    /// <summary>
    /// Describes the stat-shifting effect of a Nature.
    /// Typically involves a 10% buff to one stat and a 10% nerf to another.
    /// </summary>
    [Serializable]
    internal struct NatureStatsModifier
    {
        private const float IncreaseMultiplier = 1.1f;
        private const float DecreaseMultiplier = 0.9f;
        private const float NeutralMultiplier = 1.0f;

        [SerializeField] private MonsterStat increase;
        [SerializeField] private MonsterStat decrease;

        public readonly MonsterStat IncreasedStat => increase;
        public readonly MonsterStat DecreasedStat => decrease;

        /// <summary>
        /// Returns the multiplier for a specific stat. 
        /// Used by the StatsCalculator during the primary derivation formula.
        /// </summary>
        public readonly float GetMultiplier(MonsterStat stat)
        {
            if (stat == MonsterStat.None) return NeutralMultiplier;
            if (stat == increase) return IncreaseMultiplier;
            if (stat == decrease) return DecreaseMultiplier;

            return NeutralMultiplier;
        }
    }
}