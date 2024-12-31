using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Summary
{
    public class SummaryPokemonDescription : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pokemonName;
        [SerializeField] private PokemonGenderSprite genderSprite;
        [SerializeField] private PokemonSprite menuSprite;
        [SerializeField] private PokemonTypeSprite firstType;
        [SerializeField] private PokemonTypeSprite secondType;
        [Space]
        [SerializeField] private Party party;

        private void Start()
        {
            Pokemon pokemon = party.SelectedPokemon;

            pokemonName.text = pokemon.Data.PokemonName;
            genderSprite.Bind(pokemon);
            menuSprite.Bind(pokemon);
            firstType.Bind(pokemon);
            secondType.Bind(pokemon);
        }
    }
}
