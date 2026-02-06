using PokemonGame.Items.Models;
using PokemonGame.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.Definition
{
    /// <summary>
    /// Healing item definition. Restores a fixed amount of HP
    /// to a Monster when used.
    /// </summary>
    [CreateAssetMenu(menuName = "MonsterTamer/Items/Healing Item Definition")]
    internal class HealingItemDefinition : ItemDefinition
    {
        private const string RestoredTemplate = "{0}'s HP was restored\nby {1} points.";

        [SerializeField, Required]
        [Tooltip("Amount of HP restored when this item is used.")]
        private int healingAmount;

        /// <summary>
        /// Uses this healing item on the target Monster.
        /// </summary>
        /// <param name="target">Monster to heal.</param>
        /// <returns>
        /// Result indicating whether the item was consumed and
        /// the message(s) to display.
        /// </returns>
        public override ItemUseResult Use(PokemonInstance target)
        {
            if (target == null)
            {
                return new ItemUseResult(false, new[] { FailMessage });
            }

            int restored = target.Health.Heal(healingAmount);

            if (restored > 0)
            {
                return new ItemUseResult(true, new[]
                {
                     string.Format(RestoredTemplate, target.Definition.DisplayName, restored)
                });
            }

            return new ItemUseResult(false, new[] { NoEffectMessage });
        }
    }
}
