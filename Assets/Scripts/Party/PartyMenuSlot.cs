using PokemonGame.Menu;
using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI;
using PokemonGame.Pokemons.UI.Health;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Displays a Pokémon's information in a party menu slot, including name, level, health, gender, and sprite.
    /// Handles data binding/unbinding and resets visuals gracefully when no valid Pokémon is assigned.
    /// </summary>
    [RequireComponent(typeof(MenuButton))]
    public class PartyMenuSlot : MonoBehaviour
    {
        private static readonly int IdleHash = Animator.StringToHash("Idle");

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's level.")]
        private TextMeshProUGUI levelText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's current HP.")]
        private TextMeshProUGUI healthRemaining;

        [SerializeField, Required]
        private TextMeshProUGUI healthTotal;

        [SerializeField, Required]
        private PokemonGenderSymbol genderSymbol;

        [SerializeField, Required]
        [Tooltip("Displays a visual health bar representing HP.")]
        private HealthBar healthBar;

        [SerializeField, Required]
        [Tooltip("Animator for the Pokémon's sprite shown in the party menu.")]
        private Animator menuSpriteAnimator;

        [Title("Slot Container")]
        [SerializeField, Required]
        [Tooltip("Root GameObject container for the visual components.")]
        private GameObject contentRoot;

        /// <summary>
        /// The Pokémon currently bound to this slot, or null if none.
        /// </summary>
        public Pokemon BoundPokemon { get; private set; }

        private MenuButton menuButton;

        private void Awake()
        {
            menuButton = GetComponent<MenuButton>();
        }

        /// <summary>
        /// Binds a Pokémon to the slot and displays its data. Clears the slot if null or invalid.
        /// </summary>
        /// <param name="pokemon">The Pokémon to bind, or null to clear the slot.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Definition == null)
            {
                Unbind();
                return;
            }

            BoundPokemon = pokemon;
            pokemon.OnHealthChange += OnPokemonHealthChange;
            SetSlotVisibility(true);
            UpdateDisplay(pokemon);
        }

        /// <summary>
        /// Responds to HP changes by updating the display.
        /// </summary>
        private void OnPokemonHealthChange(int oldHealth, int newHealth)
        {
            UpdateDisplay(BoundPokemon);
        }

        /// <summary>
        /// Unbinds the current Pokémon and clears all UI data.
        /// </summary>
        public void Unbind()
        {
            BoundPokemon = null;

            nameText.text = string.Empty;
            levelText.text = string.Empty;
            healthRemaining.text = string.Empty;
            healthTotal.text = string.Empty;

            genderSymbol.Unbind();
            healthBar.Unbind();

            menuSpriteAnimator.runtimeAnimatorController = null;

            SetSlotVisibility(false);
        }

        /// <summary>
        /// Updates all UI elements with the given Pokémon's data.
        /// </summary>
        /// <param name="pokemon">The Pokémon whose data is displayed.</param>
        private void UpdateDisplay(Pokemon pokemon)
        {
            nameText.text = pokemon.Definition.DisplayName;
            levelText.text = pokemon.Level.ToString();
            healthRemaining.text = pokemon.HealthRemaining.ToString();
            healthTotal.text = pokemon.CoreStat.HealthPoint.ToString();

            genderSymbol.Bind(pokemon);
            healthBar.Bind(pokemon);

            if (pokemon.Definition.MenuSpriteOverrider != null)
            {
                menuSpriteAnimator.runtimeAnimatorController = pokemon.Definition.MenuSpriteOverrider;
                menuSpriteAnimator.Play(IdleHash);
            }
        }

        /// <summary>
        /// Shows or hides the slot and updates interactability.
        /// </summary>
        /// <param name="visible">True to show the slot, false to hide it.</param>
        private void SetSlotVisibility(bool visible)
        {
            contentRoot.SetActive(visible);

            if (menuButton != null)
            {
                menuButton.SetInteractable(visible);
            }
        }
    }
}
