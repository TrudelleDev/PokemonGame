using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI
{
    /// <summary>
    /// Displays general Pokémon information including Pokédex number, name, types, trainer, and held item.
    /// </summary>
    public class PokemonOverviewUI : MonoBehaviour, IBindable<Pokemon>, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's Pokédex number.")]
        private TextMeshProUGUI pokedexNumberText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's primary type icon.")]
        private PokemonTypeIcon primaryTypeSprite;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's secondary type icon.")]
        private PokemonTypeIcon secondaryTypeSprite;

        [SerializeField, Required]
        [Tooltip("Displays the name of the Pokémon's original trainer.")]
        private TextMeshProUGUI originalTrainerText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's unique ID.")]
        private TextMeshProUGUI idText;

        [SerializeField, Required]
        [Tooltip("Displays the held item's name, if any.")]
        private TextMeshProUGUI itemNameText;

        /// <summary>
        /// Binds the Pokémon data to the UI fields.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(Pokemon pokemon)
        {
            pokedexNumberText.text = pokemon.Data.PokedexNumber;
            nameText.text = pokemon.Data.DisplayName;
            idText.text = pokemon.ID;
            originalTrainerText.text = pokemon.OwnerName;

            primaryTypeSprite.Bind(pokemon);
            secondaryTypeSprite.Bind(pokemon);

            itemNameText.text = "None"; // or: pokemon.HeldItem?.DisplayName ?? "None";
        }

        /// <summary>
        /// Clears all UI fields.
        /// </summary>
        public void Unbind()
        {
            pokedexNumberText.text = string.Empty;
            nameText.text = string.Empty;
            idText.text = string.Empty;
            originalTrainerText.text = string.Empty;
            itemNameText.text = string.Empty;
            primaryTypeSprite.Unbind();
            secondaryTypeSprite.Unbind();
        }
    }
}
