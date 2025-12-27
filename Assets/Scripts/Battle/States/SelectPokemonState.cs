using PokemonGame.Party.Enums;
using PokemonGame.Party.UI;
using PokemonGame.Pokemon;
using PokemonGame.Views;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Opens the party menu during battle, allowing the player
    /// to select a Pokémon to switch in or take action.
    /// </summary>
    public sealed class SelectPokemonState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private PartyMenuView partyView;
        private PartyMenuPresenter partyPresenter;

        public SelectPokemonState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            // Show the Party Menu View
            partyView = ViewManager.Instance.Show<PartyMenuView>();

            // Get the presenter from the view
            partyPresenter = partyView.GetComponent<PartyMenuPresenter>();

            // Setup presenter for battle mode
            partyPresenter.Setup(PartySelectionMode.Battle);

            // Subscribe to events
            partyPresenter.PokemonConfirmed += OnPokemonSelected;
            partyPresenter.CancelRequested += OnCancel;

            // Refresh slots to ensure correct UI
            partyView.RefreshSlots();
            partyView.ShowChoosePrompt();
        }

        private void OnCancel()
        {
            machine.SetState(new PlayerActionState(machine));
        }

        public void Exit()
        {
            if (partyPresenter != null)
            {
                partyPresenter.PokemonConfirmed -= OnPokemonSelected;
                partyPresenter.CancelRequested -= OnCancel;
                partyPresenter = null;
            }

            ViewManager.Instance.Close<PartyMenuView>();
        }

        public void Update() { }

        private void OnPokemonSelected(PokemonInstance pokemon)
        {
            machine.SetState(new SwitchPokemonState(machine, pokemon));
        }
    }
}
