using System.Collections.Generic;
using MonsterTamer.Party.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Party
{
    /// <summary>
    /// Defines a trainer's party with a fixed set of Monster.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Party/Party Definition")]
    internal sealed class PartyDefinition : ScriptableObject
    {
        [SerializeField, Required, Tooltip("Monster in this party, listed in order.")]
        private List<PartyMemberEntry> members = new();

        /// <summary>
        /// Gets the party members in their defined order.
        /// </summary>
        public IReadOnlyList<PartyMemberEntry> Members => members;
    }
}
