using MonsterTamer.Monster;
using MonsterTamer.Monster.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MonsterTamer.Summary
{
    /// <summary>
    /// Manages the UI elements for the Monster summary header.
    /// Displays the Mosnter's name and sprite.
    /// </summary>
    internal class SummaryHeader : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Displays the Monster's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required, Tooltip("Displays the Monster's front-facing sprite.")]
        private MonsterSprite sprite;

        /// <summary>
        /// Binds the given Monster definition to the name and sprite.
        /// Clears the UI if <paramref name="monster"/> is null or missing required definition.
        /// </summary>
        /// <param name="monster">The Monster instance to display, or null to clear the UI.</param>
        public void Bind(MonsterInstance monster)
        {
            if (monster?.Definition == null)
            {
                Unbind();
                return;
            }

            nameText.text = monster.Definition.DisplayName;
            sprite.Bind(monster);
        }

        /// <summary>
        /// Clears the UI elements by removing all Pokémon-related data.
        /// </summary>
        public void Unbind()
        {
            nameText.text = string.Empty;
            sprite.Unbind();
        }
    }
}
