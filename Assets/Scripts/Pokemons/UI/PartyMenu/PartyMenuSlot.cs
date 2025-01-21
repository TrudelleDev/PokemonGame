using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI.PartyMenu
{
    public class PartyMenuSlot : MonoBehaviour, IPokemonBind
    {
        [SerializeField] private TextMeshProUGUI pokemonName;
        [SerializeField] private TextMeshProUGUI pokemonRemainingHealth;
        [SerializeField] private TextMeshProUGUI pokemonTotalHealth;
        [SerializeField] private TextMeshProUGUI pokemonLevel;
        [SerializeField] private PokemonGenderSprite pokemonGenderSprite;
        //[SerializeField] private Animator pokemonMenuSpriteAnimator;
        [SerializeField] private Image pokemonMenuSprite;
        [SerializeField] private HealthBar pokemonHealthBar;

        public Pokemon Pokemon { get; private set; }

        public void Bind(Pokemon pokemon)
        {
            Pokemon = pokemon;

            SetActive(true);

            pokemonName.text = pokemon.Data.PokemonName;
            pokemonRemainingHealth.text = $"{pokemon.HealthRemaining}/";
            pokemonTotalHealth.text = $"{pokemon.CoreStat.HealthPoint}";
            pokemonLevel.text = $"Lv{pokemon.Level}";

            pokemonGenderSprite.Bind(pokemon);
            pokemonHealthBar.Bind(pokemon);
            pokemonMenuSprite.sprite = pokemon.Data.Sprites.MenuSprite;

            //pokemonMenuSpriteAnimator.runtimeAnimatorController = pokemon.Data.MenuSpriteOverrider;

        }

        public void SetActive(bool value)
        {
            // Show/Hide The content of the party menu slot
            transform.GetChild(0).gameObject.SetActive(value);

            // Show normal sprite or disabled sprite of the button
            transform.GetComponent<MenuButton>().Interactable = value;
        }
    }
}
