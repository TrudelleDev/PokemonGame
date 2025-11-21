using System;
using PokemonGame.Pokemons.Definition;
using UnityEngine;

namespace PokemonGame.Party
{
    [Serializable]
    public struct PartyMemberEntry
    {
        [SerializeField, Tooltip("Pokémon species for this party slot.")]
        private PokemonDefinition pokemonDefinition;

        [SerializeField, Range(1, 100), Tooltip("Level of this Pokémon.")]
        private int level;

        public readonly PokemonDefinition PokemonDefinition => pokemonDefinition;
        public readonly int Level => level;
    }
}
