using PokemonGame.Dialogue;
using PokemonGame.Items;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Party;
using PokemonGame.Pokemons;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Inventory
{
    /// <summary>
    /// Presents options when an inventory item is selected.
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

        private PartyMenuView partyMenu;
        private DialogueBox dialogueBox;
        private VerticalMenuController controller;

        private void Awake()
        {
            dialogueBox = OverworldDialogueBox.Instance.Dialogue;
            controller = GetComponent<VerticalMenuController>();
        }

        private void OnEnable()
        {
            useButton.OnClick += OnUseButtonClick;
            cancelButton.OnClick += OnCancelButtonClick;
        }

        private void OnDisable()
        {
            useButton.OnClick -= OnUseButtonClick;
            cancelButton.OnClick -= OnCancelButtonClick;

           // partyMenu.OnPokemonSelected -= OnPokemonSelected;
           // dialogueBox.OnDialogueFinished -= OnDialogueFinished;
        }

        public override void Freeze()
        {
            controller.enabled = false;
        }

        public override void Unfreeze()
        {
            controller.enabled = true;
        }

        /// <summary>
        /// Handles the "Use" button click by opening the party menu.
        /// </summary>
        private void OnUseButtonClick()
        {
            partyMenu = ViewManager.Instance.Show<PartyMenuView>();
            partyMenu.OppenedFromInventory = true;
            partyMenu.OnPokemonSelected += OnPokemonSelected;
        }

        /// <summary>
        /// Called when a Pokémon is selected from the Party menu.
        /// Applies the item, updates the inventory, and shows dialogue.
        /// </summary>
        private void OnPokemonSelected(Pokemon pokemon)
        {
            partyMenu.OnPokemonSelected -= OnPokemonSelected;

            ItemUseResult result = SelectedItem.Definition.Use(pokemon);

            if (result.Used)
            {
                inventory.Remove(SelectedItem);
            }

            dialogueBox.OnDialogueFinished += OnDialogueFinished;
            dialogueBox.ShowDialogue(result.Messages);
        }

        /// <summary>
        /// Called when the dialogue box finishes showing messages.
        /// Closes the current view and unsubscribes events.
        /// </summary>
        private void OnDialogueFinished()
        {
            dialogueBox.OnDialogueFinished -= OnDialogueFinished;
            partyMenu.OppenedFromInventory = false;
            OnCancelButtonClick();
        }

        /// <summary>
        /// Closes this options view.
        /// </summary>
        private void OnCancelButtonClick()
        {
            ViewManager.Instance.CloseTopView();
        }
    }
}
