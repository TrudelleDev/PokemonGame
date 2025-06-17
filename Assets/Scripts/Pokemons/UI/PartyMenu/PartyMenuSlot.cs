using PokemonGame.Shared;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.PartyMenu
{
    /// <summary>
    /// Controls a single Pokémon slot in the party menu UI.
    /// Displays name, level, HP, gender, and sprite, with support for data binding, fallback display using "MissingNo", and slot interaction.
    /// </summary>

    public class PartyMenuSlot : MonoBehaviour, IPokemonBind, IUnbind
    {
        private static readonly int IdleHash = Animator.StringToHash("Idle");

        [Title("Basic Info")]
        [SerializeField, Required] private TextMeshProUGUI nameText;
        [SerializeField, Required] private TextMeshProUGUI levelText;

        [Title("Health Display")]
        [SerializeField, Required] private TextMeshProUGUI healthText;
        [SerializeField, Required] private HealthBar healthBar;

        [Title("Gender & Sprite")]
        [SerializeField, Required] private PokemonGenderSprite genderSprite;
        [SerializeField, Required] private Animator menuSpriteAnimator;

        [Title("Slot Container")]
        [Tooltip("The root GameObject that contains all visual elements of the party slot.")]
        [SerializeField, Required] private GameObject content;

        private MenuButton menuButton;

        public Pokemon Pokemon { get; private set; } // Used as a reference

        /// <summary>
        /// Binds the given Pokémon data to the UI elements.
        /// Falls back to "MissingNo" if data is missing or invalid.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(Pokemon pokemon)
        {
            SetSlotActive(true);

            if (pokemon?.Data == null)
            {
                RebindToMissingNo();
                return;
            }
        
            SetPokemonInfo(pokemon);         
            Pokemon = pokemon;
        }

        public void RebindToMissingNo()
        {
            Pokemon missingNo = PokemonFactory.CreateMissingNo();
            SetPokemonInfo(missingNo);
            Pokemon = missingNo;
        }

        /// <summary>
        /// Clears the currently assigned Pokémon and hides the slot's content.
        /// </summary>
        public void Unbind()
        {
            Pokemon = null;
            SetSlotActive(false);
        }

        private void SetSlotActive(bool isActive)
        {
            if (menuButton == null)
                menuButton = GetComponent<MenuButton>();

            content.SetActive(isActive);
            menuButton.Interactable = isActive;
        }

        private void SetPokemonInfo(Pokemon pokemon)
        {
            UIHelper.SetText(nameText, pokemon.Data.PokemonName);
            UIHelper.SetText(healthText, $"{pokemon.HealthRemaining}/{pokemon.CoreStat.HealthPoint}");
            UIHelper.SetText(levelText, $"Lv {pokemon.Level}");

            genderSprite.Bind(pokemon);
            healthBar.Bind(pokemon);

            if (pokemon.Data.MenuSpriteOverrider != null)
            {
                menuSpriteAnimator.runtimeAnimatorController = pokemon.Data.MenuSpriteOverrider;
                menuSpriteAnimator.Play(IdleHash);
            }
        }
    }
}
