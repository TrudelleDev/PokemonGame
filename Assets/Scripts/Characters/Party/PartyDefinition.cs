﻿using System.Collections.Generic;
using PokemonGame.Pokemons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters.Party
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
        private List<Pokemon> members = new();

        /// <summary>
        /// All Pokémon in this party, in fixed order.
        /// </summary>
        public IReadOnlyList<Pokemon> Members => members;
    }
}
