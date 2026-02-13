using MonsterTamer.Monster;
using MonsterTamer.Monster.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Battle.UI
{
    /// <summary>
    /// Displays the player's active Monster HUD with name, level, HP, experience, and back sprite.
    /// Automatically updates via events from the bound <see cref="MonsterInstance"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class PlayerBattleHud : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Text displaying the Monster's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required, Tooltip("Text displaying the Monster's level.")]
        private TextMeshProUGUI levelText;

        [SerializeField, Required, Tooltip("Text displaying current/max HP.")]
        private TextMeshProUGUI healthText;

        [SerializeField, Required, Tooltip("Visual health bar component.")]
        private HealthBar healthBar;

        [SerializeField, Required, Tooltip("Visual experience bar component.")]
        private ExperienceBar experienceBar;

        [SerializeField, Required, Tooltip("Back-facing battle sprite.")]
        private Image backSprite;

        private MonsterInstance activeMonster;

        internal HealthBar HealthBar => healthBar;
        internal ExperienceBar ExperienceBar => experienceBar;

        /// <summary>
        /// Binds the HUD to a Monster and subscribes to live updates.
        /// </summary>
        internal void Bind(MonsterInstance monster)
        {
            UnsubscribeCurrentMonster();

            if (monster?.Definition == null)
            {
                Unbind();
                return;
            }

            activeMonster = monster;

            nameText.text = monster.Definition.DisplayName;
            levelText.text = $"L{monster.Experience.Level}";
            backSprite.sprite = monster.Definition.Sprites.BackSprite;

            healthBar.Bind(monster);
            experienceBar.Bind(monster);

            activeMonster.Health.HealthChanged += OnHealthChanged;
            activeMonster.Experience.LevelChanged += OnLevelChanged;

            UpdateHealthText();
        }

        /// <summary>
        /// Clears the HUD and unsubscribes from Monster events.
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

        private void OnHealthChanged(int _, int __ ) => UpdateHealthText();

        private void OnLevelChanged(int newLevel)
        {
            levelText.text = $"L{newLevel}";
            UpdateHealthText();
        }

        private void UpdateHealthText()
        {
            healthText.text = $"{activeMonster.Health.CurrentHealth}/{activeMonster.Health.MaxHealth}";
        }

        private void UnsubscribeCurrentMonster()
        {
            if (activeMonster == null) return;

            activeMonster.Health.HealthChanged -= OnHealthChanged;
            activeMonster.Experience.LevelChanged -= OnLevelChanged;
            activeMonster = null;
        }
    }
}
