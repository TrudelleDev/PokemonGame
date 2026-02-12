using MonsterTamer.Monster;
using MonsterTamer.Monster.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Battle.UI
{
    /// <summary>
    /// Displays the player's active Monster HUD, including name, level, HP text, health bar, experience bar, and back sprite.
    /// Updates in real-time via subscribed events from the bound <see cref="MonsterInstance"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class PlayerBattleHud : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Text field showing the player's Monster name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text field showing the player's Monster level.")]
        private TextMeshProUGUI levelText;

        [SerializeField, Required]
        [Tooltip("Text field showing the player's Monster current and maximum HP (e.g., 100/150).")]
        private TextMeshProUGUI healthText;

        [SerializeField, Required]
        [Tooltip("Health bar component displaying the player's Monster HP visually.")]
        private HealthBar healthBar;

        [SerializeField, Required]
        [Tooltip("Experience bar component displaying the player's Monster current EXP progress.")]
        private ExperienceBar experienceBar;

        [SerializeField, Required]
        [Tooltip("Image showing the player's Pokémon back-facing battle sprite.")]
        private Image backSprite;

        private MonsterInstance activeMonster;

        /// <summary>
        /// Provides access to the HealthBar component for damage animations or updates.
        /// </summary>
        internal HealthBar HealthBar => healthBar;

        /// <summary>
        /// Provides access to the ExperienceBar component for EXP gain animations.
        /// </summary>
        internal ExperienceBar ExperienceBar => experienceBar;

        /// <summary>
        /// Binds the HUD to a specific Monster instance and subscribes to events for live updates.
        /// </summary>
        /// <param name="monster">The active Monster instance.</param>
        internal void Bind(MonsterInstance monster)
        {
            // Unsubscribe from any previously bound Monster
            UnsubscribeCurrentMonster();

            if (monster?.Definition == null)
            {
                Unbind();
                return;
            }

            activeMonster = monster;

            // Initialize HUD
            nameText.text = monster.Definition.DisplayName;
            levelText.text = $"L{monster.Experience.Level}";
            UpdateHealthText();
            backSprite.sprite = monster.Definition.Sprites.BackSprite;

            // Bind sub-components
            healthBar.Bind(monster);
            experienceBar.Bind(monster);

            // Subscribe to live updates
            activeMonster.Health.HealthChanged += HandleHealthChanged;
            activeMonster.Experience.LevelChanged += HandleLevelChanged;
        }

        /// <summary>
        /// Clears the HUD and unsubscribes from all Monster events.
        /// </summary>
        internal void Unbind()
        {
            UnsubscribeCurrentMonster();

            nameText.text = string.Empty;
            levelText.text = string.Empty;
            healthText.text = "- / -";
            backSprite.sprite = null;

            healthBar.Unbind();
            experienceBar.Unbind();
        }

        private void HandleHealthChanged(int oldHp, int newHp)
        {
            UpdateHealthText();
        }

        private void HandleLevelChanged(int newLevel)
        {
            levelText.text = $"L{activeMonster.Experience.Level}";
            UpdateHealthText();
        }

        private void UpdateHealthText()
        {
            healthText.text = $"{activeMonster.Health.CurrentHealth}/{activeMonster.Health.MaxHealth}";
        }

        private void UnsubscribeCurrentMonster()
        {
            if (activeMonster != null)
            {
                activeMonster.Health.HealthChanged -= HandleHealthChanged;
                activeMonster.Experience.LevelChanged -= HandleLevelChanged;
                activeMonster = null;
            }
        }
    }
}
