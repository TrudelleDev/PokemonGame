using MonsterTamer.Pokemon;
using MonsterTamer.Pokemon.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Battle.UI
{
    /// <summary>
    /// Displays the opponent Monster's battle HUD, including name, level, HP, and front sprite.
    /// Provides methods to bind and unbind Monster data.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class OpponentBattleHud : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Text field showing the opponent Monster's display name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Text field showing the opponent Monster's level.")]
        private TextMeshProUGUI levelText;

        [SerializeField, Required]
        [Tooltip("Health bar component displaying the opponent Monster's current HP.")]
        private HealthBar healthBar;

        [SerializeField, Required]
        [Tooltip("Image component showing the opponent Monster's front-facing battle sprite.")]
        private Image frontSprite;

        /// <summary>
        /// Provides external access to the health bar for updates.
        /// </summary>
        internal HealthBar HealthBar => healthBar;

        /// <summary>
        /// Binds the opponent HUD to the given Monster instance.
        /// Updates name, level, health bar, and front sprite.
        /// </summary>
        /// <param name="monster">The opponent Monster to display.</param>
        internal void Bind(PokemonInstance monster)
        {
            if (monster?.Definition == null)
            {
                Unbind();
                return;
            }

            nameText.text = monster.Definition.DisplayName;
            levelText.text = $"L{monster.Experience.Level}";
            healthBar.Bind(monster);
            frontSprite.sprite = monster.Definition.Sprites.FrontSprite;
        }

        /// <summary>
        /// Clears all HUD elements and unbinds any Monster reference.
        /// </summary>
        internal void Unbind()
        {
            nameText.text = string.Empty;
            levelText.text = string.Empty;
            frontSprite.sprite = null;
            healthBar.Unbind();
        }
    }
}
