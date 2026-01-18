using PokemonGame.Party.Enums;
using PokemonGame.Party.UI;
using PokemonGame.Views;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Opens the party menu during battle, allowing the player to select a Pokémon or switch out.
    /// </summary>
    internal sealed class SelectPokemonState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private PartyMenuView partyView;
        private PartyMenuPresenter partyPresenter;

        internal SelectPokemonState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            partyView = ViewManager.Instance.Show<PartyMenuView>();
            partyPresenter = partyView.GetComponent<PartyMenuPresenter>();

            partyPresenter.Setup(PartySelectionMode.Overworld);
            partyPresenter.ReturnRequested += HandleCancel;

            partyView.RefreshSlots();
        }

        public void Update() { }

        public void Exit()
        {
            if (partyPresenter != null)
            {
                partyPresenter.ReturnRequested -= HandleCancel;
                partyPresenter = null;
            }

            ViewManager.Instance.Close<PartyMenuView>();
        }

        private void HandleCancel()
        {
            machine.SetState(new PlayerActionState(machine));
        }
    }
}