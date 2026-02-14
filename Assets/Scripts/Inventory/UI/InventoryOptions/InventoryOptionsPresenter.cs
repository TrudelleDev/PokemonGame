using System;
using System.Linq;
using MonsterTamer.Characters;
using MonsterTamer.Characters.Core;
using MonsterTamer.Dialogue;
using MonsterTamer.Items.Definition;
using MonsterTamer.Items.Models;
using MonsterTamer.Monster;
using MonsterTamer.Party.Enums;
using MonsterTamer.Party.UI;
using MonsterTamer.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Inventory.UI.InventoryOptions
{
    /// <summary>
    /// Handles the flow of using an inventory item via <see cref="InventoryOptionsController"/>.
    /// Opens the party menu when necessary and reacts to Monster selection.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class InventoryOptionsPresenter : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Controller managing the InventoryOptionsView buttons.")]
        private InventoryOptionsController inventoryOptionsController;

        [SerializeField, Required, Tooltip("Inventory manager containing the player's items.")]
        private Character player;

        [SerializeField, Required, Tooltip("Presenter for the party menu used for selecting Monster targets.")]
        private PartyMenuPresenter partyMenuPresenter;

        [SerializeField, Required, Tooltip("View displaying inventory item options.")]
        private InventoryOptionsView inventoryOptionsView;

        private ItemDefinition currentItem;
        private bool lastItemUseSucceeded;

        /// <summary>
        /// Raised when an item has been used successfully.
        /// </summary>
        public event Action<bool> ItemUsed;

        private void OnEnable()
        {
            inventoryOptionsController.UseRequested += HandleUseRequested;
            inventoryOptionsController.ReturnRequested += HandleReturnRequested;
        }

        private void OnDisable()
        {
            inventoryOptionsController.UseRequested -= HandleUseRequested;
            inventoryOptionsController.ReturnRequested -= HandleReturnRequested;
        }

        /// <summary>
        /// Sets the item that this presenter will handle.
        /// Must be called before using the item.
        /// </summary>
        /// <param name="item">The item definition selected by the player.</param>
        public void Initialize(ItemDefinition item)
        {
            currentItem = item;
        }

        private void HandleUseRequested()
        {
            if (currentItem == null)
            {
                return;
            }

            ViewManager.Instance.Close<InventoryOptionsView>();
            partyMenuPresenter.Setup(PartySelectionMode.UseItem);

            void TempHandler(MonsterInstance pokemon)
            {
                var itemInstance = player.Inventory.Items.FirstOrDefault(i => i.Definition == currentItem);

                if (itemInstance == null)
                {
                    return;
                }

                ItemUseResult result = currentItem.Use(pokemon);
                lastItemUseSucceeded = result.Used;

                if (result.Used)
                {
                    player.Inventory.Remove(itemInstance);
                }

                OverworldDialogueBox.Instance.Dialogue.ShowDialogue(result.Messages);
                OverworldDialogueBox.Instance.Dialogue.DialogueFinished += ClosePartyMenu;

                // Unsubscribe immediately to prevent multiple calls
                partyMenuPresenter.ItemTargetSelected -= TempHandler;
            }

            partyMenuPresenter.ItemTargetSelected += TempHandler;
            ViewManager.Instance.Show<PartyMenuView>();
        }

        private void ClosePartyMenu()
        {
            ViewManager.Instance.Close<PartyMenuView>();
            OverworldDialogueBox.Instance.Dialogue.DialogueFinished -= ClosePartyMenu;
            ItemUsed?.Invoke(lastItemUseSucceeded);
        }

        private void HandleReturnRequested()
        {
            ViewManager.Instance.CloseTopView();
        }
    }
}
