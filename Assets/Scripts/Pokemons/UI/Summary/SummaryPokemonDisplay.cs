using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Summary
{
    public class SummaryPokemonDisplay : MonoBehaviour, IPokemonBind
    {
        [SerializeField] private TextMeshProUGUI pokemonName;
        [SerializeField] private TextMeshProUGUI pokemonLevel;
        [SerializeField] private PokemonGenderSprite pokemonGenderSprite;
        [SerializeField] private PokemonSprite pokemonSprite;

        public void Bind(Pokemon pokemon)
        {
            pokemonName.text = $"{pokemon.Data.PokemonName}";
            pokemonLevel.text = $"<size=12>Lv</size>{pokemon.Level}";
            pokemonGenderSprite.Bind(pokemon);
            pokemonSprite.Bind(pokemon);
        }
    }
}
