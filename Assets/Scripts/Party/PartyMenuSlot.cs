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
    /// Displays a Pok�mon's information in a party menu slot, including name, level, health, gender, and sprite.
    /// Handles data binding/unbinding and resets visuals gracefully when no valid Pok�mon is assigned.
    /// </summary>
    [RequireComponent(typeof(MenuButton))]
    public class PartyMenuSlot : MonoBehaviour
    {
        private static readonly int IdleHash = Animator.StringToHash("Idle");

        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's level.")]
        private TextMeshProUGUI levelText;

        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's current HP.")]
        private TextMeshProUGUI healthText;

        [SerializeField, Required]
        [Tooltip("Displays a visual health bar representing HP.")]
        private HealthBar healthBar;

        [SerializeField, Required]
        [Tooltip("Displays the Pok�mon's gender icon.")]
        private PokemonGenderSprite genderIcon;

        [SerializeField, Required]
        [Tooltip("Animator for the Pok�mon's sprite shown in the party menu.")]
        private Animator menuSpriteAnimator;

        [Title("Slot Container")]
        [SerializeField, Required]
        [Tooltip("Root GameObject container for the visual components.")]
        private GameObject contentRoot;

        /// <summary>
        /// The Pok�mon currently bound to this slot, or null if none.
        /// </summary>
        public Pokemon BoundPokemon { get; private set; }

        private MenuButton menuButton;

        private void Awake()
        {
            menuButton = GetComponent<MenuButton>();
        }

        /// <summary>
        /// Binds a Pok�mon to the slot and displays its data. Clears the slot if null or invalid.
        /// </summary>
        /// <param name="pokemon">The Pok�mon to bind, or null to clear the slot.</param>
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
        /// Unbinds the current Pok�mon and clears all UI data.
        /// </summary>
        public void Unbind()
        {
            BoundPokemon = null;

            nameText.text = string.Empty;
            levelText.text = string.Empty;
            healthText.text = string.Empty;

            genderIcon.Unbind();
            healthBar.Unbind();

            menuSpriteAnimator.runtimeAnimatorController = null;

            SetSlotVisibility(false);
        }

        /// <summary>
        /// Updates all UI elements with the given Pok�mon's data.
        /// </summary>
        /// <param name="pokemon">The Pok�mon whose data is displayed.</param>
        private void UpdateDisplay(Pokemon pokemon)
        {
            nameText.text = pokemon.Definition.DisplayName;
            levelText.text = $"Lv {pokemon.Level}";
            healthText.text = $"{pokemon.HealthRemaining}/{pokemon.CoreStat.HealthPoint}";

            genderIcon.Bind(pokemon);
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
