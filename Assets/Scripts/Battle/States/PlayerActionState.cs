using PokemonGame.Views;
using Unity.VisualScripting;

namespace PokemonGame.Battle.States
{
    public class PlayerActionState : IBattleState
    {
        private readonly BattleStateMachine machine;

        private BattleView View => machine.BattleView;

        public PlayerActionState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            View.DialogueBox.ShowDialogue(new[] { "What would\nyou do?" }, true);

            View.BattleAnimation.PlayPlayerHudIdle();
            ViewManager.Instance.Show<PlayerActionView>();
        }

        public void Update()
        {

        }

        public void Exit()
        {

        }
    }
}
