using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Characters.PokemonTeam.UI
{
    public class PartyMenuSlot : MonoBehaviour, IPokemonBind, IComponentInitialize
    {
        [SerializeField] private TextMeshProUGUI pokemonName;
        [SerializeField] private TextMeshProUGUI pokemonLevel;
        [SerializeField] private TextMeshProUGUI pokemonTotalHealth;
        [SerializeField] private TextMeshProUGUI pokemonRemainingHealth;
        [SerializeField] private Image pokemonSprite;
        [SerializeField] private PokemonGenderSprite pokemonGenderSprite;
        [SerializeField] private HealthBar healthBar;

        public void Initialize()
        {
            healthBar.Initialize();
            pokemonGenderSprite.Initialize();
        }

        public void Bind(Pokemon pokemon)
        {
            pokemonName.text = pokemon.PokemonData.PokemonName;
            pokemonLevel.text = $"Lv{pokemon.Level}";
            pokemonRemainingHealth.text = $"{pokemon.RemainingHealth}/";
            pokemonTotalHealth.text = $"{pokemon.CoreStat.HealthPoint}";
            pokemonSprite.sprite = pokemon.PokemonData.Sprites.MenuSprite;

            healthBar.Bind(pokemon);
            pokemonGenderSprite.Bind(pokemon);

            SetActive(true);
        }

        public void SetActive(bool value)
        {
            // Hide/Show every childs
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(value);
            }

            // Disable/Enable the button
            if (transform.GetComponent<Button>() != null)
            {
                transform.GetComponent<Button>().interactable = value;
            }
        }
    }
}
