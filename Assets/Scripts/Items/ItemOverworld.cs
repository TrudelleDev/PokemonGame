using PokemonGame.Characters;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Items.Datas;
using PokemonGame.Systems.Dialogue;
using PokemonGame.Systems.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items
{
    /// <summary>
    /// Represents an item placed in the world that the player can pick up by interacting with it.
    /// Adds the item to the inventory and shows a pickup message upon interaction.
    /// </summary>
    public class ItemOverworld : MonoBehaviour, IInteract
    {
        [SerializeField, Required]
        [Tooltip("Reference to the item data that defines this world item.")]
        private ItemData itemData;

        [SerializeField]
        [Tooltip("Quantity of the item to give to the player.")]
        private int quantity = 1;

        /// <summary>
        /// Called when the player interacts with the world item.
        /// Adds the item to the inventory, shows a pickup message, and destroys the world object.
        /// </summary>
        /// <param name="character">The player character interacting with the item.</param>
        public void Interact(Character character)
        {
            if (itemData == null)
            {
                Log.Warning(nameof(ItemOverworld), "Missing item data!");
                return;
            }

            string[] message = new string[2];
            message[0] = $"{character.CharacterName} found a {itemData.Name}!";
            message[1] = $"{character.CharacterName} put the {itemData.Name} in the {itemData.Type} pocket.";

            InventoryManager inventory = character.GetComponent<InventoryManager>();

            if (inventory != null)
            {
                inventory.Add(itemData, quantity);
            }

            if (DialogueBox.Instance != null)
            {
                DialogueBox.Instance.ShowDialogue(message);
            }

            Destroy(gameObject);
        }
    }
}
