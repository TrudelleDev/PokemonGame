using PokemonGame.Shared;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Summary
{
    /// <summary>
    /// Controls the Pokémon summary header UI, displaying the Pokémon's name, level, gender, and sprite.
    /// Automatically falls back to a "MissingNo" placeholder when given invalid or missing data.
    /// </summary>
    public class SummaryHeader : MonoBehaviour, IPokemonBind
    {
        [SerializeField, Required] private TextMeshProUGUI nameText;
        [SerializeField, Required] private TextMeshProUGUI levelText;
        [SerializeField, Required] private PokemonGenderSprite genderSprite;
        [SerializeField, Required] private PokemonSprite frontSprite;

        /// <summary>
        /// Binds the given Pokémon data to the UI elements.
        /// Falls back to "MissingNo" if data is missing or invalid.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Data == null)
            {
                RebindToMissingNo();
                return;
            }

            SetPokemonInfo(pokemon);
        }

        public void RebindToMissingNo()
        {
            Pokemon missingNo = PokemonFactory.CreateMissingNo();
            SetPokemonInfo(missingNo);
        }

        private void SetPokemonInfo(Pokemon pokemon)
        {
            UIHelper.SetText(nameText, pokemon.Data.PokemonName);
            UIHelper.SetText(levelText, $"<size=12>Lv</size>{pokemon.Level}");

            genderSprite.Bind(pokemon);
            frontSprite.Bind(pokemon);
        }
    }
}
