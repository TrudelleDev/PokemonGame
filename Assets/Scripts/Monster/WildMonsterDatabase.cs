using System.Collections.Generic;
using MonsterTamer.Monster.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Monster
{
    /// <summary>
    /// A weighted collection of Monster definitions used to determine wild encounter tables.
    /// </summary>
    [CreateAssetMenu(menuName = "MonsterTamer/Monster/Wild Monster Database")]
    internal sealed class WildMonsterDatabase : ScriptableObject
    {
        [SerializeField, Required]
        [Tooltip("The pool of potential encounters with their respective spawn weights.")]
        private List<WildMonsterEntry> entries = new();

        internal List<WildMonsterEntry> Entries => entries;
    }
}