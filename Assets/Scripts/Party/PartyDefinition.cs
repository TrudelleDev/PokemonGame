using System.Collections.Generic;
using PokemonGame.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Defines a trainer's party with a fixed set of Pokémon.
    /// Used for NPC battles and debugging.
    /// </summary>
    [CreateAssetMenu(menuName = "Party/Party Definition", fileName = "NewPartyDefinition")]
    public class PartyDefinition : ScriptableObject
    {
        [SerializeField, Required]
        [Tooltip("Pokémon in this party, listed in order.")]
        private List<PokemonInstance> members = new();

        /// <summary>
        /// All Pokémon in this party, in fixed order.
        /// </summary>
        public IReadOnlyList<PokemonInstance> Members => members;
    }
}
