using MonsterTamer.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MonsterTamer.Pokemon.UI
{
    /// <summary>
    /// Displays general Pokémon information including Pokédex number, name, types, trainer, and held item.
    /// </summary>
    internal class PokemonOverviewUI : MonoBehaviour, IBindable<PokemonInstance>, IUnbind
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
        private TextMeshProUGUI leveltext;

        [SerializeField, Required]
        private TextMeshProUGUI natureText;

        /// <summary>
        /// Binds the Pokémon data to the UI fields.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(PokemonInstance pokemon)
        {
            pokedexNumberText.text = pokemon.Definition.PokedexNumber.ToString("D3");
            nameText.text = pokemon.Definition.DisplayName;
            natureText.text = pokemon.Nature.Definition.DisplayName;
            originalTrainerText.text = pokemon.Meta.OwnerName;

            primaryTypeSprite.Bind(pokemon);
            secondaryTypeSprite.Bind(pokemon);

            leveltext.text = pokemon.Experience.Level.ToString();
        }

        /// <summary>
        /// Clears all UI fields.
        /// </summary>
        public void Unbind()
        {
            pokedexNumberText.text = string.Empty;
            nameText.text = string.Empty;
            natureText.text = string.Empty;
            originalTrainerText.text = string.Empty;
            leveltext.text = string.Empty;
            primaryTypeSprite.Unbind();
            secondaryTypeSprite.Unbind();
        }
    }
}
