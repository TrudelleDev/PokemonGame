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
    /// Displays actionable options for a selected inventory item.
    /// Handles item usage, party menu interactions, and resulting dialogues.
    /// </summary>
    [DisallowMultipleComponent]
    public class InventoryItemOptionsView : View
    {
        [SerializeField, Required]
        [Tooltip("Reference to the player's inventory manager.")]
        private InventoryManager inventory;

        [Title("Buttons")]

        [SerializeField, Required]
        [Tooltip("Button that applies the selected item.")]
        private MenuButton useButton;

        [SerializeField, Required]
        [Tooltip("Button that cancels and closes this view.")]
        private MenuButton cancelButton;

        /// <summary>
        /// The item currently selected in the player's inventory.
        /// </summary>
        public Item SelectedItem { get; set; }

        private PartyMenuView partyMenu;
        private VerticalMenuController controller;

        private void Awake()
        {
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

            if (partyMenu != null)
                partyMenu.OnPokemonSelected -= OnPokemonSelected;

            var dialogueBox = OverworldDialogueBox.Instance?.Dialogue;

            if (dialogueBox != null)
                dialogueBox.OnDialogueFinished -= OnDialogueFinished;
        }

        /// <summary>
        /// Disables menu input.
        /// </summary>
        public override void Freeze()
        {
            controller.enabled = false;
        }

        /// <summary>
        /// Re-enables menu input.
        /// </summary>
        public override void Unfreeze()
        {
            controller.enabled = true;
        }


        /// <summary>
        /// Called when the player selects a Pokémon from the party menu.
        /// Attempts to use the selected item on that Pokémon and shows any resulting dialogue.
        /// </summary>
        private void OnPokemonSelected(PokemonInstance pokemon)
        {
            partyMenu.OnPokemonSelected -= OnPokemonSelected;

            ItemUseResult result = SelectedItem.Definition.Use(pokemon);

            if (result.Used)
            {
                inventory.Remove(SelectedItem);
            }

            OverworldDialogueBox.Instance.Dialogue.OnDialogueFinished += OnDialogueFinished;
            OverworldDialogueBox.Instance.Dialogue.ShowDialogue(result.Messages);
        }


        private void OnUseButtonClick()
        {
            partyMenu = ViewManager.Instance.Show<PartyMenuView>();
            partyMenu.OppenedFromInventory = true;
            partyMenu.OnPokemonSelected += OnPokemonSelected;
        }

        private void OnDialogueFinished()
        {
            OverworldDialogueBox.Instance.Dialogue.OnDialogueFinished -= OnDialogueFinished;
            partyMenu.OppenedFromInventory = false;
            OnCancelButtonClick();
        }

        private void OnCancelButtonClick()
        {
            ViewManager.Instance.CloseTopView();
        }
    }
}
