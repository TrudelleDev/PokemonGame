using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.UI.PartyMenu
{
    /// <summary>
    /// Displays a Pokémon's information in a party menu slot, including name, level, health, gender, and sprite.
    /// Handles data binding and unbinding, and gracefully resets visuals if no valid Pokémon is assigned.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class PartyMenuSlot : MonoBehaviour, IBindable<Pokemon>, IUnbind
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
        private TextMeshProUGUI healthText;

        [SerializeField, Required]
        [Tooltip("Displays a visual health bar representing HP.")]
        private HealthBar healthBar;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's gender icon.")]
        private PokemonGenderSprite genderIcon;

        [SerializeField, Required]
        [Tooltip("Animator for the Pokémon's sprite shown in the party menu.")]
        private Animator menuSpriteAnimator;

        [Title("Slot Container")]
        [SerializeField, Required]
        [Tooltip("Root GameObject container for the visual components.")]
        private GameObject contentRoot;

        public Pokemon BoundPokemon { get; private set; }

        private Button menuButton;

        private void Awake()
        {
            menuButton = GetComponent<Button>();
        }

        /// <summary>
        /// Binds a Pokémon to the slot and displays its data. Clears the slot if null or invalid.
        /// </summary>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Data == null)
            {
                Unbind();
                return;
            }

            BoundPokemon = pokemon;

            SetSlotVisibility(true);
            UpdateDisplay(pokemon);
        }

        /// <summary>
        /// Unbinds the current Pokémon and clears all UI data.
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

        private void UpdateDisplay(Pokemon pokemon)
        {
            nameText.text = pokemon.Data.DisplayName;
            levelText.text = $"Lv {pokemon.Level}";
            healthText.text = $"{pokemon.HealthRemaining}/{pokemon.CoreStat.HealthPoint}";

            genderIcon.Bind(pokemon);
            healthBar.Bind(pokemon);

            if (pokemon.Data.MenuSpriteOverrider != null)
            {
                menuSpriteAnimator.runtimeAnimatorController = pokemon.Data.MenuSpriteOverrider;
                menuSpriteAnimator.Play(IdleHash);
            }
        }

        private void SetSlotVisibility(bool visible)
        {
            contentRoot.SetActive(visible);

            if (menuButton != null)
                menuButton.interactable = visible;
        }
    }
}
