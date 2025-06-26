using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Groups
{
    /// <summary>
    /// Groups the UI components used to display a Pokémon's primary and secondary types.
    /// </summary>
    [Serializable]
    public class PokemonTypeUIGroup
    {
        [SerializeField, Required]
        [Tooltip("Displays the primary type of the Pokémon.")]
        private PokemonTypeIcon primaryTypeSprite;

        [SerializeField, Required]
        [Tooltip("Displays the secondary type of the Pokémon, if applicable.")]
        private PokemonTypeIcon secondaryTypeSprite;

        public PokemonTypeIcon PrimaryTypeSprite => primaryTypeSprite;
        public PokemonTypeIcon SecondaryTypeSprite => secondaryTypeSprite;
    }
}
