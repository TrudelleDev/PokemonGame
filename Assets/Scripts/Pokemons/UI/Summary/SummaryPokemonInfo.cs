using System;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Summary
{
    public class SummaryPokemonInfo : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private TextMeshProUGUI pokedexNumber;
        [SerializeField] private TextMeshProUGUI pokemonName;
        [SerializeField] private TextMeshProUGUI pokemonID;
        [Header("Trainer")]
        [SerializeField] private TextMeshProUGUI trainerName;
        [SerializeField] private TextMeshProUGUI trainerMemo;
        [Header("Type")]
        [SerializeField] private PokemonTypeSprite firstType;
        [SerializeField] private PokemonTypeSprite secondType;
        [Space]
        [SerializeField] private Party party;

        private void Start()
        {
            Pokemon pokemon = party.SelectedPokemon;

            pokedexNumber.text = $"{pokemon.Data.PokedexNumber:000}";
            pokemonName.text = $"{pokemon.Data.PokemonName}";
            trainerName.text = $"{pokemon.OwnerName}";
            pokemonID.text = $"{pokemon.ID}";

            trainerMemo.text =
              $"{pokemon.Nature.Data.NatureName} nature.{Environment.NewLine}" +
              $"Met at {pokemon.LocationEncounter} at level {pokemon.Level}.";

            firstType.Bind(pokemon);
            secondType.Bind(pokemon);

        }
    }
}
