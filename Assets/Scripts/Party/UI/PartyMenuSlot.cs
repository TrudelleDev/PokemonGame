using PokemonGame.Menu;
using PokemonGame.Pokemon;
using PokemonGame.Pokemon.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Party.UI
{
    /// <summary>
    /// Displays a Pokémon's information in a party menu slot, including name, level, health, gender, and sprite.
    /// Handles data binding/unbinding and resets visuals gracefully when no valid Pokémon is assigned.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MenuButton))]
    internal sealed class PartyMenuSlot : MonoBehaviour
    {
        private static readonly int IdleHash = Animator.StringToHash("Idle");

        [SerializeField, Required, Tooltip("Displays the Pokémon's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required, Tooltip("Displays the Pokémon's level.")]
        private TextMeshProUGUI levelText;

        [SerializeField, Required, Tooltip("Displays the Pokémon's current HP.")]
        private TextMeshProUGUI healthRemaining;

        [SerializeField, Required, Tooltip("Displays the Pokémon's total HP.")]
        private TextMeshProUGUI healthTotal;

        [SerializeField, Required, Tooltip("Displays the Pokémon's gender symbol.")]
        private PokemonGenderSymbol genderSymbol;

        [SerializeField, Required, Tooltip("Displays a visual health bar representing HP.")]
        private HealthBar healthBar;

        [SerializeField, Required, Tooltip("Animator for the Pokémon's sprite shown in the party menu.")]
        private Animator menuSpriteAnimator;

        [Title("Slot Container")]
        [SerializeField, Required, Tooltip("Root GameObject container for the visual components.")]
        private GameObject contentRoot;

        private MenuButton menuButton;

        /// <summary>
        /// The Pokémon currently bound to this slot, or null if none.]
        /// </summary>
        internal PokemonInstance BoundPokemon { get; private set; }

        /// <summary>
        /// The index of this slot within the party menu.
        /// </summary>
        internal int Index { get; private set; }

        private void Awake()
        {
            menuButton = GetComponent<MenuButton>();
        }

        /// <summary>
        /// Binds a Pokémon to the slot and displays its data.
        /// Clears the slot if the Pokémon is null or invalid, and subscribes to health changes.
        /// </summary>
        /// <param name="pokemon">The Pokémon to bind, or null to clear the slot.</param>
        internal void Bind(PokemonInstance pokemon)
        {
            if (pokemon?.Definition == null)
            {
                Unbind();
                return;
            }

            // Unsubscribe from previous Pokémon event if any
            if (BoundPokemon != null)
            {
                BoundPokemon.Health.HealthChanged -= HandlePokemonHealthChange;
            }

            BoundPokemon = pokemon;
            BoundPokemon.Health.HealthChanged += HandlePokemonHealthChange;

            SetSlotVisibility(true);
            UpdateDisplay(BoundPokemon);
        }

        /// <summary>
        /// Unbinds the current Pokémon and clears all UI data.
        /// </summary>
        internal void Unbind()
        {
            if (BoundPokemon != null)
            {
                BoundPokemon.Health.HealthChanged -= HandlePokemonHealthChange;
                BoundPokemon = null;
            }

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
        /// Sets the slot index. Should be called when initializing the party menu.
        /// </summary>
        /// <param name="index">The index to assign to this slot.</param>
        internal void SetSlotIndex(int index)
        {
            Index = index;
        }


        /// <summary>
        /// Updates the UI when the Pokémon's health changes.
        /// </summary>
        private void HandlePokemonHealthChange(int oldHealth, int newHealth)
        {
            UpdateDisplay(BoundPokemon);
        }


        private void UpdateDisplay(PokemonInstance pokemon)
        {
            nameText.text = pokemon.Definition.DisplayName;
            levelText.text = pokemon.Experience.Level.ToString();
            healthRemaining.text = pokemon.Health.CurrentHealth.ToString();
            healthTotal.text = pokemon.Health.MaxHealth.ToString();

            genderSymbol.Bind(pokemon);
            healthBar.Bind(pokemon);

            if (pokemon.Definition.MenuSpriteOverrider != null)
            {
                menuSpriteAnimator.runtimeAnimatorController = pokemon.Definition.MenuSpriteOverrider;
                menuSpriteAnimator.Play(IdleHash);
            }
        }

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
