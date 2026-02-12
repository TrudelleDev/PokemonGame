using MonsterTamer.Nature.Models;
using MonsterTamer.Monster.Enums; // Assuming MonsterStat enum is here
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Nature
{
    /// <summary>
    /// Defines a Monster's nature, which modifies specific stats by a fixed percentage.
    /// </summary>
    [CreateAssetMenu(menuName = "MonsterTamer/Nature/Nature Definition")]
    public class NatureDefinition : ScriptableObject
    {
        [SerializeField, Required]
        private string displayName;

        [SerializeField]
        private NatureStatsModifier modifiers;

        public string DisplayName => displayName;

        // This is the "bridge" that lets the StatsCalculator work.
        // It simply asks the struct for the float value (0.9, 1.0, or 1.1).
        public float GetMultiplier(MonsterStat stat) => modifiers.GetMultiplier(stat);
    }
}