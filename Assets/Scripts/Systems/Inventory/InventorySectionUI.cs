using PokemonGame.Items;
using PokemonGame.Items.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Systems.Inventory
{
    /// <summary>
    /// Displays the contents of an InventorySection by instantiating UI elements for each item.
    /// </summary>
    public class InventorySectionUI : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("The prefab used to display each individual item in the inventory.")]
        private ItemUI itemUIPrefab;

        [SerializeField, Required]
        [Tooltip("The cancel button that will be added to the end of the item list.")]
        private MenuButton cancelButton;

        [SerializeField, Required]
        [Tooltip("The transform under which all item UI elements and the cancel button will be instantiated.")]
        private Transform contentParent;

        /// <summary>
        /// Populates the UI with the items from the given inventory section.
        /// </summary>
        /// <param name="section">The section of the inventory to display.</param>
        public void Bind(InventorySection section)
        {
            Clear();

            foreach (Item item in section.Items)
            {
                ItemUI itemUIInstance = Instantiate(itemUIPrefab, contentParent);

                itemUIInstance.Bind(item);
            }

            Instantiate(cancelButton, contentParent);
        }

        private void Clear()
        {
            for (int i = contentParent.childCount - 1; i >= 0; i--)
            {
                Destroy(contentParent.GetChild(i).gameObject);
            }
        }
    }
}
