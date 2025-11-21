using PokemonGame.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.Definition
{
    /// <summary>
    /// Healing item definition. Restores a fixed amount of HP
    /// to a Pokémon when used.
    /// </summary>
    [CreateAssetMenu(fileName = "NewHealingItemDefinition", menuName = "Items/Healing Item Definition")]
    public class HealingItemDefinition : ItemDefinition
    {
        private const string FailMessage = "But it failed...";
        private const string NoEffectMessage = "It won't have any effect.";
        private const string RestoredTemplate = "{0}'s HP was restored\nby {1} points.";

        [SerializeField, Required]
        [Tooltip("Amount of HP restored when this item is used.")]
        private int healingAmount;

        /// <summary>
        /// Uses this healing item on the target Pokémon.
        /// </summary>
        /// <param name="target">Pokémon to heal.</param>
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

            int restored = target.RestoreHP(healingAmount);

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
