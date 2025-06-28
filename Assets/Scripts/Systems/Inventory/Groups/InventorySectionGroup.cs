using System;
using UnityEngine;

namespace PokemonGame.Systems.Inventory.Groups
{
    /// <summary>
    /// Represents a grouped set of inventory sections, organized by item type.
    /// This includes regular items, key items, and Poké Balls.
    /// </summary>
    [Serializable]
    public class InventorySectionGroup
    {
        [Tooltip("Regular items (e.g., Potions, Repels).")]
        [SerializeField] private InventorySection item;

        [Tooltip("Key items (e.g., Bike, Maps).")]
        [SerializeField] private InventorySection keyItem;

        [Tooltip("Poké Balls and similar items.")]
        [SerializeField] private InventorySection ball;

        public InventorySection Item => item;
        public InventorySection KeyItem => keyItem;
        public InventorySection Ball => ball;
    }
}
