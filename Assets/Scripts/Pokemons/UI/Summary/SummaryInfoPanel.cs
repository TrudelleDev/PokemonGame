using System;
using PokemonGame.Shared;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Summary
{
    /// <summary>
    /// Controls the detailed Pokémon info panel in the summary screen.
    /// Displays Pokédex number, name, unique ID, types, and trainer details.
    /// Supports dynamic data binding and falls back to a "MissingNo" placeholder if data is missing or invalid.
    /// </summary>
    public class SummaryInfoPanel : MonoBehaviour, IPokemonBind
    {
        [Title("Pokemon")]
        [SerializeField, Required] private TextMeshProUGUI pokedexNumberText;
        [SerializeField, Required] private TextMeshProUGUI nameText;
        [SerializeField, Required] private TextMeshProUGUI idText;
        //[SerializeField, Required] private TextMeshProUGUI heldItemName;

        [Title("Type")]
        [SerializeField, Required] private PokemonTypeIcon primaryTypeSprite;
        [SerializeField, Required] private PokemonTypeIcon secondaryTypeSprite;

        [Title("Trainer")]
        [SerializeField, Required] private TextMeshProUGUI trainerNameText;
        [SerializeField, Required] private TextMeshProUGUI trainerMemoText;

        /// <summary>
        /// Binds the given Pokémon data to the UI elements.
        /// Falls back to "MissingNo" if data is missing or invalid.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Nature?.Data == null)
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
            UIHelper.SetText(pokedexNumberText, pokemon.Data.PokedexNumber, "D3");
            UIHelper.SetText(nameText, pokemon.Data.PokemonName);
            UIHelper.SetText(idText, pokemon.ID);

            UIHelper.SetText(trainerNameText, pokemon.OwnerName);
            UIHelper.SetText(trainerMemoText, BuildTrainerMemo(pokemon));

            primaryTypeSprite.Bind(pokemon);

            if (pokemon.Data.Types.HasSecondType)
                secondaryTypeSprite.Bind(pokemon);
            else
                secondaryTypeSprite.Unbind();
        }

        private string BuildTrainerMemo(Pokemon pokemon)
        {
            return $"{pokemon.Nature.Data.NatureName.ToUpper()} nature.{Environment.NewLine}" +
                   $"Met at {pokemon.LocationEncounter.ToUpper()} at level {pokemon.Level}.";
        }
    }
}
