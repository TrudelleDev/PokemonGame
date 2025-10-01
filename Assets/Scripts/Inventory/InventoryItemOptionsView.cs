using PokemonGame.Dialogue;
using PokemonGame.Items;
using PokemonGame.Menu;
using PokemonGame.Party;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// View that presents options when an inventory item is selected.
    /// Provides "Use" (opens <see cref="PartyMenuView"/>) and "Cancel" (closes the view).
    /// </summary>
    [DisallowMultipleComponent]
    public class InventoryItemOptionsView : View
    {
        [SerializeField, Required]
        [Tooltip("Reference to the player’s inventory manager.")]
        private InventoryManager inventory;

        [Title("Buttons")]
        [SerializeField, Required]
        [Tooltip("Button that confirms using the selected item.")]
        private MenuButton useButton;

        [SerializeField, Required]
        [Tooltip("Button that cancels and returns to the previous menu.")]
        private MenuButton cancelButton;

        /// <summary>
        /// The item currently selected in the inventory.
        /// </summary>
        public Item SelectedItem { get; set; }

        private void OnEnable()
        {
            useButton.OnClick += HandleUseClicked;
            cancelButton.OnClick += HandleCancelClicked;
        }

        private void OnDisable()
        {
            useButton.OnClick -= HandleUseClicked;
            cancelButton.OnClick -= HandleCancelClicked;
        }

        /// <summary>
        /// Handles the "Use" button click by opening the party menu and applying the item.
        /// </summary>
        private void HandleUseClicked()
        {
            PartyMenuView partyMenu = ViewManager.Instance.Show<PartyMenuView>();

            partyMenu.OnPokemonSelected += pokemon =>
            {
                ItemUseResult result = SelectedItem.Definition.Use(pokemon);

                if (result.Used)
                {
                    inventory.Remove(SelectedItem);
                }

                DialogueBoxView dialogueBox = ViewManager.Instance.Show<DialogueBoxView>();

                dialogueBox.OnDialogueFinished += () =>
                {
                    ViewManager.Instance.CloseCurrentView();
                };

                dialogueBox.ShowDialogue(result.Messages);
            };
        }

        /// <summary>
        /// Handles the "Cancel" button click by closing this view.
        /// </summary>
        private void HandleCancelClicked()
        {
            ViewManager.Instance.CloseCurrentView();
        }
    }
}
