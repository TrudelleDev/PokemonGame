using System;
using PokemonGame.Pokemons.UI.Groups;
using PokemonGame.Shared;
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
        [SerializeField] private PokemonInfoUIGroup pokemonInfo;
        [SerializeField] private PokemonTypeUIGroup pokemonType;
        [SerializeField] private TrainerInfoUIGroup trainerInfo;

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
            UIHelper.SetText(pokemonInfo.PokedexNumberText, pokemon.Data.PokedexNumber, "D3");
            UIHelper.SetText(pokemonInfo.NameText, pokemon.Data.PokemonName);
            UIHelper.SetText(pokemonInfo.IdText, pokemon.ID);

            UIHelper.SetText(trainerInfo.TrainerNameText, pokemon.OwnerName);
            UIHelper.SetText(trainerInfo.TrainerMemoText, BuildTrainerMemo(pokemon));

            pokemonType.PrimaryTypeSprite.Bind(pokemon);

            if (pokemon.Data.Types.HasSecondType)
                pokemonType.SecondaryTypeSprite.Bind(pokemon);
            else
                pokemonType.SecondaryTypeSprite.Unbind();
        }

        private string BuildTrainerMemo(Pokemon pokemon)
        {
            return $"{pokemon.Nature.Data.NatureName} nature.{Environment.NewLine}" +
                   $"Met at {pokemon.LocationEncounter} at level {pokemon.Level}.";
        }
    }
}
