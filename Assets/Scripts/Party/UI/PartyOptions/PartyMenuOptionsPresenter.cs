using PokemonGame.Summary;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party.UI.PartyOptions
{
    /// <summary>
    /// Presenter for the party menu options (Switch, Summary, Cancel).
    /// Listens to <see cref="PartyMenuOptionsController"/> events and executes
    /// the corresponding actions such as starting a swap, opening the
    /// summary view, or closing the options menu.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class PartyMenuOptionsPresenter : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Controller handling user input for the party menu options.")]
        private PartyMenuOptionsController controller;

        [SerializeField, Required, Tooltip("Reference to the parent Party Menu Presenter, used for initiating Pokémon swaps.")]
        private PartyMenuPresenter presenter;

        [SerializeField, Required, Tooltip("Reference to the player's party manager.")]
        private PartyManager playerParty;

        private void OnEnable()
        {
            controller.SwitchSelected += HandleSwitchSelected;
            controller.SummarySelected += HandleSummarySelected;
            controller.CancelSelected += HandleCancelSelected;
        }

        private void OnDisable()
        {
            controller.SwitchSelected -= HandleSwitchSelected;
            controller.SummarySelected -= HandleSummarySelected;
            controller.CancelSelected -= HandleCancelSelected;
        }

        private void HandleSwitchSelected()
        {
            presenter.StartSwap();
            ClosePartyMenuOption();
        }

        private void HandleSummarySelected()
        {
            ViewManager.Instance.Show<SummaryView>();
            ClosePartyMenuOption();
        }

        private void HandleCancelSelected()
        {
            ClosePartyMenuOption();
        }

        private void ClosePartyMenuOption()
        {
            ViewManager.Instance.Close<PartyMenuOptionsView>();
        }
    }
}
