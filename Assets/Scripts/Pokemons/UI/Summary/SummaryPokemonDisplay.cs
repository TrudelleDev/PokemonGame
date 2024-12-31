using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Summary
{
    public class SummaryPokemonDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pokemonName;
        [SerializeField] private TextMeshProUGUI pokemonLevel;
        [SerializeField] private PokemonGenderSprite pokemonGenderSprite;
        [SerializeField] private PokemonSprite pokemonSprite;
        [Space]
        [SerializeField] private Party party;


        private void Start()
        {
            Pokemon pokemon = party.SelectedPokemon;

            pokemonName.text = $"{pokemon.Data.PokemonName}";
            pokemonLevel.text = $"<size=30>Lv</size> {pokemon.Level}";
            pokemonGenderSprite.Bind(pokemon);
            pokemonSprite.Bind(pokemon);
        }
    }
}
