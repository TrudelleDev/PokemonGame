using PokemonGame.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Defines a preset inventory for trainers, shops, or new game initialization.
    /// Items are grouped into sections (General, Key Items, Poké Balls).
    /// </summary>
    [CreateAssetMenu(menuName = "Inventory/Inventory Definition", fileName = "NewInventoryDefinition")]
    public class InventoryDefinition : ScriptableObject
    {
        [BoxGroup("Sections")]
        [SerializeField, Required]
        [Tooltip("General items (e.g., Potions, Repels).")]
        private InventorySection generalItems;

        [BoxGroup("Sections")]
        [SerializeField, Required]
        [Tooltip("Key items (e.g., Bike, Maps).")]
        private InventorySection keyItems;

        [BoxGroup("Sections")]
        [SerializeField, Required]
        [Tooltip("Poké Balls and related items.")]
        private InventorySection pokeballs;

        /// <summary>
        /// Items available in the General section.
        /// </summary>
        public InventorySection GeneralItems => generalItems;

        /// <summary>
        /// Items available in the Key Items section.
        /// </summary>
        public InventorySection KeyItems => keyItems;

        /// <summary>
        /// Items available in the Poké Ball section.
        /// </summary>
        public InventorySection Pokeballs => pokeballs;
    }
}
