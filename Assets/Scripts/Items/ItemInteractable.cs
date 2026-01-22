using PokemonGame.Audio;
using PokemonGame.Characters;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Dialogue;
using PokemonGame.Items.Definition;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items
{
    /// <summary>
    /// Interactable item pickup: grants an item stack to the interacting character.
    /// </summary>
    public class ItemInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Required, Tooltip("The item this pickup grants.")]
        private Item item;

        [SerializeField, Required, Tooltip("Sound played when the item is received.")]
        private AudioClip receiveItemClip;

        private bool consumed;

        /// <summary>
        /// Called when a character interacts with this item.
        /// Adds the item to the character's inventory, plays the pickup sound,
        /// shows item pickup dialogue, and destroys the game object.
        /// </summary>
        /// <param name="player">The character interacting with the item.</param>
        public void Interact(Character player)
        {
            if (consumed)
            {
                return;
            }

            consumed = true;

            ItemDefinition definition = item.Definition;

            string itemFoundLine = item.Quantity > 1
                ? $"{player.Definition.DisplayName} found {item.Quantity} × {definition.DisplayName}!"
                : $"{player.Definition.DisplayName} found a {definition.DisplayName}!";

            string putInBagLine = $"{player.Definition.DisplayName} put the {definition.DisplayName}\nin the Inventory";


            AudioManager.Instance.PlaySFX(receiveItemClip);
            OverworldDialogueBox.Instance.Dialogue.ShowDialogue(new[] { itemFoundLine, putInBagLine });

            player.Inventory.Add(item);

            Destroy(gameObject);
        }
    }
}
