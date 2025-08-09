using PokemonGame.Characters;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Items.Definition;
using PokemonGame.Items.Models;
using PokemonGame.Systems.Dialogue;
using PokemonGame.Systems.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items
{
    /// <summary>
    /// World pickup: grants an item stack to the interacting character,
    /// shows a message, and then destroys itself.
    /// </summary>
    public class ItemOverworld : MonoBehaviour, IInteract
    {
        [SerializeField, Required]
        [Tooltip("The item stack this pickup grants.")]
        private ItemStack stack;

        private bool consumed;

        public void Interact(Character character)
        {
            if (consumed)
            {
                return;
            }

            consumed = true; // Prevents multiple pickups in quick succession

            if (!ItemDefinitionLoader.TryGet(stack.ItemID, out var itemDefinition))
            {
                Log.Warning(nameof(ItemOverworld), $"Missing ItemDefinition for ID: {stack.ItemID}");
                return;
            }

            if (!character.TryGetComponent<InventoryManager>(out var inventory))
            {
                Log.Warning(nameof(ItemOverworld), "No InventoryManager on interacting character.");
                return;
            }

            string foundLine;

            if (stack.Quantity > 1)
            {
                foundLine = character.CharacterName + " found " + stack.Quantity + " × " + itemDefinition.DisplayName + "!";
            }
            else
            {
                foundLine = character.CharacterName + " found a " + itemDefinition.DisplayName + "!";
            }

            // string putLine = character.CharacterName + " put the " + itemDefinition.DisplayName + " in the " + itemDefinition.Category + " pocket.";

            //DialogueBox.Instance.ShowDialogue(new[] { foundLine, putLine });
            DialogueBox.Instance.ShowDialogue(new[] { foundLine });

            inventory.Add(stack);
            Destroy(gameObject);
        }
    }
}
