using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI.PartyMenu
{
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
            pokemonName.text = pokemon.Data.PokemonName;
            remainingHealth.text = $"{pokemon.HealthRemaining}/";
            totalHealth.text = $"{pokemon.CoreStat.HealthPoint}";
            level.text = $"Lv{pokemon.Level}";

            genderSprite.Bind(pokemon);
            menuSprite.Bind(pokemon);
            healthBar.Bind(pokemon);

            SetInteractable(true);
        }

        public void SetInteractable(bool value)
        {
            // Hide/Show every childs
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(value);
            }

            // If value is false its will show the disable sprite
            // If value is true its will show the normal sprite
            // The controller will also skips the party menu slot if value is false
            if (transform.GetComponent<Button>() != null)
            {
                transform.GetComponent<Button>().interactable = value;
            }
        }
    }
}
