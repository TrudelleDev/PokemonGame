using System.Collections.Generic;
using MonsterTamer.Pokemon.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Pokemon
{
    /// <summary>
    /// Database of wild Monsters that can appear in the game.
    /// Each entry defines a possible Monster and its encounter weight for random selection.
    /// </summary>
    [CreateAssetMenu(fileName = "WildMonsterDatabase", menuName = "MonsterTamer/Monster/Wild Monster Database")]
    internal class WildMonsterDatabase : ScriptableObject
    {
        [SerializeField, Required, Tooltip("All possible wild Monsters and their encounter weights.")] 
        private List<WildPokemonEntry> entries = new();

        /// <summary>
        /// All possible wild Monsters and their encounter weights.
        /// </summary>
        internal List<WildPokemonEntry> Entries => entries;
    }
}
