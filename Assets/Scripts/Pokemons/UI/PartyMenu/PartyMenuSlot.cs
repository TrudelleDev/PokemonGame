using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI.PartyMenu
{
    [RequireComponent(typeof(Button))]
    public class PartyMenuSlot : MonoBehaviour, IPokemonBind
    {
        [SerializeField] private TextMeshProUGUI pokemonName;
        [SerializeField] private TextMeshProUGUI remainingHealth;
        [SerializeField] private TextMeshProUGUI totalHealth;
        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private PokemonGenderSprite genderSprite;
        [SerializeField] private PokemonSprite menuSprite;
        [SerializeField] private HealthBar healthBar;

        public void Bind(Pokemon pokemon)
        {
            SetActive(true);

            pokemonName.text = pokemon.Data.PokemonName;
            remainingHealth.text = $"{pokemon.HealthRemaining}/";
            totalHealth.text = $"{pokemon.CoreStat.HealthPoint}";
            level.text = $"Lv{pokemon.Level}";

            genderSprite.Bind(pokemon);
            menuSprite.Bind(pokemon);
            healthBar.Bind(pokemon);
        }

        public void SetActive(bool value)
        {
            // Show/Hide The content of the party menu slot
            transform.GetChild(0).gameObject.SetActive(value);

            // Show normal sprite or disabled sprite of the button
            transform.GetComponent<Button>().interactable = value;
        }
    }
}
