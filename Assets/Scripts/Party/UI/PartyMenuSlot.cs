using MonsterTamer.Monster;
using MonsterTamer.Monster.UI;
using MonsterTamer.Shared.UI.Core;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Party.UI
{
    /// <summary>
    /// Displays a Monster's information in a party menu slot, including name, level, health, and sprite.
    /// Handles data binding/unbinding and resets visuals gracefully when no valid Monster is assigned.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MenuButton))]
    internal sealed class PartyMenuSlot : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Displays the Monster's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required, Tooltip("Displays the Monster's level.")]
        private TextMeshProUGUI levelText;

        [SerializeField, Required, Tooltip("Displays the Monster's current HP.")]
        private TextMeshProUGUI healthText;

        [SerializeField, Required]
        private Image menuSprite;

        [SerializeField, Required, Tooltip("Displays a visual health bar representing HP.")]
        private HealthBar healthBar;

        [Space]
        [SerializeField, Required, Tooltip("Root GameObject container for the visual components.")]
        private GameObject contentRoot;

        private MenuButton menuButton;

        public MonsterInstance BoundMonster { get; private set; }

        /// <summary>
        /// The index of this slot within the party menu.
        /// </summary>
        public int Index { get; private set; }

        private void Awake()
        {
            menuButton = GetComponent<MenuButton>();
            menuButton.SetInteractable(false);
        }

        /// <summary>
        /// Binds a Monster to the slot and displays its data.
        /// Clears the slot if the Monster is null or invalid, and subscribes to health changes.
        /// </summary>
        /// <param name="monster">The Monster to bind, or null to clear the slot.</param>
        public void Bind(MonsterInstance monster)
        {
            if (monster?.Definition == null)
            {
                Unbind();
                return;
            }

            // Unsubscribe from previous Monster event if any
            if (BoundMonster != null)
            {
                BoundMonster.Health.HealthChanged -= HandleMonsterHealthChange;
            }

            BoundMonster = monster;
            BoundMonster.Health.HealthChanged += HandleMonsterHealthChange;

            SetSlotVisibility(true);
            UpdateDisplay(BoundMonster);
        }

        /// <summary>
        /// Unbinds the current Monster and clears all UI data.
        /// </summary>
        public void Unbind()
        {
            if (BoundMonster != null)
            {
                BoundMonster.Health.HealthChanged -= HandleMonsterHealthChange;
                BoundMonster = null;
            }

            nameText.text = string.Empty;
            levelText.text = string.Empty;
            healthText.text = string.Empty;

            menuSprite.enabled = false;

            healthBar.Unbind();

            SetSlotVisibility(false);
        }

        /// <summary>
        /// Sets the slot index. Should be called when initializing the party menu.
        /// </summary>
        /// <param name="index">The index to assign to this slot.</param>
        public void SetSlotIndex(int index)
        {
            Index = index;
        }

        /// <summary>
        /// Updates the UI when the Pokémon's health changes.
        /// </summary>
        private void HandleMonsterHealthChange(int oldHealth, int newHealth)
        {
            UpdateDisplay(BoundMonster);
        }

        private void UpdateDisplay(MonsterInstance monster)
        {
            nameText.text = monster.Definition.DisplayName;
            levelText.text = $"lv{monster.Experience.Level}";
            healthText.text = $"{monster.Health.CurrentHealth}/{monster.Health.MaxHealth}";

            menuSprite.enabled = true;
            menuSprite.sprite = monster.Definition.Sprites.MenuSprite;

            monster.Health.CurrentHealth.ToString();

            healthBar.Bind(monster);
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
