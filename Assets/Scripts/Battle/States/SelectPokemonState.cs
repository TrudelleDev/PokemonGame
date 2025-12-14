using PokemonGame.Party;
using PokemonGame.Views;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Opens the party menu during battle, allowing the player to select a Pokémon
    /// for potential switching.
    /// </summary>
    public sealed class SelectPokemonState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private PartyMenuView partyView;
        private PartyMenuOptionView optionsView;

        public SelectPokemonState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            partyView = ViewManager.Instance.Show<PartyMenuView>();
            optionsView = ViewManager.Instance.Get<PartyMenuOptionView>();

            // Subscribe switch handler
            if (optionsView != null)
                optionsView.OnSwitchSelected += OnSwitchSelected;

            // Subscribe cancel / close handler
            if (partyView != null)
                partyView.OnCloseButtonPress += OnCancel;
        }

        public void Update() { }

        public void Exit()
        {
            if (optionsView != null)
            {
                optionsView.OnSwitchSelected -= OnSwitchSelected;
                optionsView = null;
            }

            if (partyView != null)
            {
                partyView.OnCloseButtonPress -= OnCancel;
                partyView = null;
            }

            // Close views
            ViewManager.Instance.Close<PartyMenuOptionView>();
            ViewManager.Instance.Close<PartyMenuView>();
        }

        private void OnCancel()
        {
            machine.SetState(new PlayerActionState(machine));
        }

        private void OnSwitchSelected()
        {
            machine.SetState(new SwitchPokemonState(machine));
        }
    }
}
