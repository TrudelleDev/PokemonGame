using PokemonGame.Shared;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Summary
{
    /// <summary>
    /// Controls the Pokémon identity panel UI within the summary screen.
    /// Displays the Pokémon's name, gender, menu sprite, and type icons.
    /// Provides binding for dynamic Pokémon data and falls back to a "MissingNo" placeholder if data is missing or invalid.
    /// </summary>
    public class SummaryIdentityPanel : MonoBehaviour, IPokemonBind
    {
        [SerializeField, Required] private TextMeshProUGUI nameText;
        [SerializeField, Required] private PokemonGenderSprite genderSprite;
        [SerializeField, Required] private PokemonSprite menuSprite;
        [SerializeField, Required] private PokemonTypeIcon primaryTypeSprite;
        [SerializeField, Required] private PokemonTypeIcon secondaryTypeSprite;

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

        private void RebindToMissingNo()
        {
            Pokemon missingNo = PokemonFactory.CreateMissingNo();
            SetPokemonInfo(missingNo);
        }

        private void SetPokemonInfo(Pokemon pokemon)
        {
            UIHelper.SetText(nameText, pokemon.Data.PokemonName);

            genderSprite.Bind(pokemon);
            menuSprite.Bind(pokemon);
            primaryTypeSprite.Bind(pokemon);

            if (pokemon.Data.Types.HasSecondType)
                secondaryTypeSprite.Bind(pokemon);
            else
                secondaryTypeSprite.Unbind();
        }
    }
}
