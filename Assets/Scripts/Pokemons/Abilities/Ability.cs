using System;
using UnityEngine;

namespace PokemonGame.Pokemons.Abilities
{
    [Serializable]
    public class Ability
    {
        [SerializeField] private AbilityData data;

        public AbilityData Data => data;
    }
}
