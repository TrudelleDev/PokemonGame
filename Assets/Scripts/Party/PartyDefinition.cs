using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Defines a trainer's party with a fixed set of Pokémon.
    /// </summary>
    [CreateAssetMenu(menuName = "Party/Party Definition")]
    public class PartyDefinition : ScriptableObject
    {
        [SerializeField, Required, Tooltip("Pokémon in this party, listed in order.")]
        private List<PartyMemberEntry> members = new();

        public IReadOnlyList<PartyMemberEntry> Members => members;
    }
}
