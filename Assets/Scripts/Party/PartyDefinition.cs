using System.Collections.Generic;
using PokemonGame.Party.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Defines a trainer's party with a fixed set of Pokémon.
    /// </summary>
    [CreateAssetMenu(menuName = "Party/Party Definition")]
    internal sealed class PartyDefinition : ScriptableObject
    {
        [SerializeField, Required, Tooltip("Pokémon in this party, listed in order.")]
        private List<PartyMemberEntry> members = new();

        /// <summary>
        /// Gets the party members in their defined order.
        /// </summary>
        internal IReadOnlyList<PartyMemberEntry> Members => members;
    }
}
