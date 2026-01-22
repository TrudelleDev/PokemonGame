using PokemonGame.Items.Models;
using PokemonGame.Pokemon;
using PokemonGame.Pokemon.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.Definition
{
    /// <summary>
    /// Defines an item that cures a specific status condition (e.g., Poison, Burn, Paralysis).
    /// </summary>
    [CreateAssetMenu(menuName = "MonsterTamer/Items/Status Condition Item Definition")]
    public class StatusItemDefinition : ItemDefinition
    {
        private const string RestoredTemplate = "{0} is no longer {1}.";

        [SerializeField, Required, Tooltip("The status condition this item cures.")]
        private StatusCondition statusCondition;

        /// <summary>
        /// Uses this item on the target Monster to cure a status condition.
        /// </summary>
        /// <param name="target">The Monster to cure.</param>
        /// <returns>
        /// An <see cref="ItemUseResult"/> indicating whether the item was consumed 
        /// and the message(s) to display.
        /// </returns>
        public override ItemUseResult Use(PokemonInstance target)
        {
            if (target == null)
            {
                return new ItemUseResult(false, new[] { FailMessage });
            }

            bool statusCured = target.Health.TryCureStatus(statusCondition);

            if (statusCured)
            {
                return new ItemUseResult(true, new[]
                {
                    string.Format(RestoredTemplate, target.Definition.DisplayName, statusCondition)
                });
            }

            return new ItemUseResult(false, new[] { NoEffectMessage });
        }
    }
}
