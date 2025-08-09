using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items.Definition
{
    /// <summary>
    /// Defines the data for a healing item, including its healing amount.
    /// </summary>
    [CreateAssetMenu(fileName = "NewHealingItemDefinition", menuName = "Items/Healing Item Definition")]
    public class HealingItemDefinition : ItemDefinition
    {
        [Tooltip("The amount of HP restored when this item is used.")]
        [SerializeField, Required] private int healingAmount;

        /// <summary>
        /// The amount of health restored when this item is used.
        /// </summary>
        public int HealingAmount => healingAmount;
    }
}
