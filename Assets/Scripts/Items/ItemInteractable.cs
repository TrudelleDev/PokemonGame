using MonsterTamer.Audio;
using MonsterTamer.Characters;
using MonsterTamer.Characters.Interfaces;
using MonsterTamer.Dialogue;
using MonsterTamer.Items.Definition;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Items
{
    /// <summary>
    /// Interactable item pickup: grants an item stack to the interacting character.
    /// Handles item addition, pickup sound, and 2-line dialogue display.
    /// </summary>
    internal class ItemInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Required, Tooltip("The item this pickup grants.")]
        private Item item;

        [SerializeField, Required, Tooltip("Sound played when the item is received.")]
        private AudioClip receiveItemClip;

        private bool consumed;

        /// <summary>
        /// Called when a character interacts with this item.
        /// Adds the item to the inventory, plays sound, and shows dialogue.
        /// </summary>
        public void Interact(Character player)
        {
            if (consumed) return;
            consumed = true;

            ItemDefinition definition = item.Definition;

            // Build two lines of dialogue with real newline (\n)
            string itemFoundLine = item.Quantity > 1
                ? $"You found {item.Quantity} × {definition.DisplayName}!"
                : $"You picked up a {definition.DisplayName}!";

            string putInBagLine = $"It's added to your inventory.";

            string fullDialogue = $"{itemFoundLine}\n{putInBagLine}";

            // Play pickup sound
            if (receiveItemClip != null)
                AudioManager.Instance.PlaySFX(receiveItemClip);

            // Show dialogue (DialogueBox will handle paging and typewriter)
            OverworldDialogueBox.Instance.Dialogue.ShowDialogue(fullDialogue);

            // Add item to player's inventory
            player.Inventory.Add(item);

            // Destroy pickup object
            Destroy(gameObject);
        }
    }
}
