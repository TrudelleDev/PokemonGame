using System;
using PokemonGame.Menu;
using PokemonGame.Menu.Controllers;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party
{
    /// <summary>
    /// Option menu displayed when a Pokémon is selected in the party.
    /// Raises events for Switch, Summary, and Cancel actions.
    /// </summary>
    public class PartyMenuOptionView : View
    {
        [Title("Buttons")]
        [SerializeField, Required] private MenuButton summaryButton;
        [SerializeField, Required] private MenuButton switchButton;
        [SerializeField, Required] private MenuButton cancelButton;

        private VerticalMenuController controller;

        public event Action OnSwitchSelected;
        public event Action OnSummarySelected;
        public event Action OnCancelSelected;

        private void Awake()
        {
            controller = GetComponent<VerticalMenuController>();
        }

        private void OnEnable()
        {
            summaryButton.OnClick += RaiseSummary;
            switchButton.OnClick += RaiseSwitch;
            cancelButton.OnClick += RaiseCancel;

            controller.SelectFirst();
        }

        private void OnDisable()
        {
            summaryButton.OnClick -= RaiseSummary;
            switchButton.OnClick -= RaiseSwitch;
            cancelButton.OnClick -= RaiseCancel;
        }

        public override void Freeze() => controller.enabled = false;
        public override void Unfreeze() => controller.enabled = true;

        private void RaiseSummary() => OnSummarySelected?.Invoke();
        private void RaiseSwitch() => OnSwitchSelected?.Invoke();
        private void RaiseCancel() => OnCancelSelected?.Invoke();
    }
}
