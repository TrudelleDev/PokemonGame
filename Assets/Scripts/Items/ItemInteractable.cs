using PokemonGame.Audio;
using PokemonGame.Characters.Core;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Dialogue;
using PokemonGame.Inventory;
using PokemonGame.Items.Definition;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Items
{
    /// <summary>
    /// Interactable item pickup: grants an item stack to the interacting character,
    /// shows a Pokémon-style message, and then destroys itself.
    /// </summary>
    public class ItemInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Required]
        [Tooltip("The item this pickup grants.")]
        private Item item;

        [Header("Audio")]
        [SerializeField, Required]
        [Tooltip("Sound played when the item is received.")]
        private AudioClip receiveItemClip;

        private bool consumed;

        public void Interact(Character player)
        {
            if (consumed)
            {
                return;
            }

            consumed = true;

            ItemDefinition definition = item.Definition;

            string itemFoundLine = item.Quantity > 1
                ? $"{player.DisplayName} found {item.Quantity} × {definition.DisplayName}!"
                : $"{player.DisplayName} found a {definition.DisplayName}!";

            string putInBagLine = $"{player.DisplayName} put the {definition.DisplayName}\n" +
                             $"in the {definition.Category} Pocket.";

            if (receiveItemClip != null)
            {
                AudioManager.Instance.PlaySFX(receiveItemClip);
            }

            OverworldDialogueBox.Instance.Dialogue.ShowDialogue(new[] { itemFoundLine, putInBagLine });

            if (player.TryGetComponent<InventoryManager>(out var inventory))
            {
                inventory.Add(item);
            }

            Destroy(gameObject);
        }
    }
}
