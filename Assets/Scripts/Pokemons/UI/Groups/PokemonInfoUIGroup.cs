using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Groups
{
    /// <summary>
    /// Groups the main Pokémon identity UI fields, including Pokédex number, name, and unique ID.
    /// Used for displaying basic Pokémon info in the summary screen.
    /// </summary>
    [Serializable]
    public class PokemonInfoUIGroup
    {
        [SerializeField, Required]
        [Tooltip("Displays the Pokédex number of the Pokémon.")]
        private TextMeshProUGUI pokedexNumberText;

        [SerializeField, Required]
        [Tooltip("Displays the name of the Pokémon.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Displays the unique ID of the Pokémon.")]
        private TextMeshProUGUI idText;

        public TextMeshProUGUI PokedexNumberText => pokedexNumberText;
        public TextMeshProUGUI NameText => nameText;
        public TextMeshProUGUI IdText => idText;
    }
}
