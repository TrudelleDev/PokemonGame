using PokemonGame.Summary;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Party.UI.PartyOptions
{
    /// <summary>
    /// Presenter for the party menu options (Swap, Info, Return).
    /// Listens to <see cref="PartyMenuOptionsController"/> events and executes
    /// the corresponding actions such as starting a swap, opening the
    /// info view, or closing the options menu.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class PartyMenuOptionsPresenter : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Controller handling user input for the party menu options.")]
        private PartyMenuOptionsController controller;

        [SerializeField, Required]
        [Tooltip("Reference to the parent Party Menu Presenter, used for initiating Pokémon swaps.")]
        private PartyMenuPresenter presenter;

        [SerializeField, Required, Tooltip("Reference to the player's party manager.")]
        private PartyManager playerParty;

        private void OnEnable()
        {
            controller.SwapSelected += HandleSwapSelected;
            controller.InfoSelected += HandleInfoSelected;
            controller.ReturnSelected += HandleReturnSelected;
        }

        private void OnDisable()
        {
            controller.SwapSelected -= HandleSwapSelected;
            controller.InfoSelected -= HandleInfoSelected;
            controller.ReturnSelected -= HandleReturnSelected;
        }

        private void HandleSwapSelected()
        {
            presenter.StartSwap();
            ClosePartyMenuOption();
        }

        private void HandleInfoSelected()
        {
            ViewManager.Instance.Show<SummaryView>();
            ClosePartyMenuOption();
        }

        private void HandleReturnSelected()
        {
            ClosePartyMenuOption();
        }

        private void ClosePartyMenuOption()
        {
            ViewManager.Instance.Close<PartyMenuOptionsView>();
        }
    }
}
