using System.Collections.Generic;
using PokemonGame.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Defines the starting items for an inventory template. 
    /// Can be used for players, NPCs, trainers, or shops.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Inventory/Definition", fileName = "NewInventoryDefinition")]
    public class InventoryDefinition : ScriptableObject
    {
        [SerializeField, Required]
        [Tooltip("Starting items for this inventory template")]
        private List<Item> items;

        /// <summary>
        /// Read-only list of items defined in this inventory template.
        /// </summary>
        public IReadOnlyList<Item> Items => items;
    }
}
