using System;
using System.Linq;
using PokemonGame.Dialogue;
using PokemonGame.Items.Definition;
using PokemonGame.Party.Enums;
using PokemonGame.Party.UI;
using PokemonGame.Pokemon;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Inventory.UI.InventoryOptions
{
    /// <summary>
    /// Handles the flow of using an inventory item via <see cref="InventoryOptionsController"/>.
    /// Opens the party menu when necessary and reacts to Pokémon selection.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class InventoryOptionsPresenter : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Controller managing the InventoryOptionsView buttons.")]
        private InventoryOptionsController inventoryOptionsController;

        [SerializeField, Required, Tooltip("Inventory manager containing the player's items.")]
        private InventoryManager playerInventory;

        [SerializeField, Required, Tooltip("Presenter for the party menu used for selecting Pokémon targets.")]
        private PartyMenuPresenter partyMenuPresenter;

        [SerializeField, Required, Tooltip("View displaying inventory item options.")]
        private InventoryOptionsView inventoryOptionsView;

        private ItemDefinition currentItem;

        /// <summary>
        /// Raised when an item has been used successfully.
        /// </summary>
        internal event Action<bool> ItemUsed;

        private void OnEnable()
        {
            inventoryOptionsController.UseRequested += HandleUse;
            inventoryOptionsController.CancelRequested += HandleCancel;
        }

        private void OnDisable()
        {
            inventoryOptionsController.UseRequested -= HandleUse;
            inventoryOptionsController.CancelRequested -= HandleCancel;
        }

        /// <summary>
        /// Sets the item that this presenter will handle.
        /// Must be called before using the item.
        /// </summary>
        /// <param name="item">The item definition selected by the player.</param>
        internal void Initialize(ItemDefinition item)
        {
            currentItem = item;
            inventoryOptionsView.SetDescription($"{currentItem.DisplayName}\nis selected.");
        }

        private void HandleUse()
        {
            if (currentItem == null) return;

            ViewManager.Instance.Close<InventoryOptionsView>();
            partyMenuPresenter.Setup(PartySelectionMode.UseItem);

            void TempHandler(PokemonInstance pokemon)
            {
                var itemInstance = playerInventory.Items.FirstOrDefault(i => i.Definition == currentItem);
                if (itemInstance == null) return;

                var result = currentItem.Use(pokemon);
                if (result.Used)
                {
                    playerInventory.Remove(itemInstance);
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
            ItemUsed?.Invoke(true);
        }

        private void HandleCancel()
        {
            ViewManager.Instance.CloseTopView();
        }
    }
}
