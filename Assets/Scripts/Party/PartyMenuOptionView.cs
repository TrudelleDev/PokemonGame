using System;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Summary;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Displays the option menu when selecting a Pokémon in the party.
    /// Provides actions such as opening the summary screen or canceling.
    /// </summary>
    public class PartyMenuOptionView : View
    {
        [Title("Buttons")]
        [SerializeField, Required]
        [Tooltip("Button to open the SummaryView.")]
        private MenuButton summaryButton;

        [SerializeField, Required]
        private MenuButton switchButton;

        [SerializeField, Required]
        [Tooltip("Button to close the option menu and return to the party menu.")]
        private MenuButton cancelButton;

        private VerticalMenuController controller;
        public event Action OnClose;

        private void Awake()
        {
            summaryButton.OnClick += OnSummaryClick;
            switchButton.OnClick += OnSwitchClick;
            cancelButton.OnClick += OnCancelClick;

            controller =  GetComponent<VerticalMenuController>();
        }

        private void OnDestroy()
        {
            summaryButton.OnClick -= OnSummaryClick;
            switchButton.OnClick -= OnSwitchClick;
            cancelButton.OnClick -= OnCancelClick;
        }

        private void OnEnable()
        {
            controller.SelectFirst();
        }

        public override void Freeze()
        {
            controller.enabled = false;
        }

        public override void Unfreeze()
        {
            controller.enabled = true;
        }

        private void OnSummaryClick()
        {
            ViewManager.Instance.Show<SummaryView>();
        }

        private void OnSwitchClick()
        {
            PartyMenuView partyView = ViewManager.Instance.Get<PartyMenuView>();

            if (partyView == null) 
                return;

            partyView.StartSwapMode();
            ViewManager.Instance.CloseTopView();
        }

        private void OnCancelClick()
        {
            ViewManager.Instance.CloseTopView();
        }
    }
}
