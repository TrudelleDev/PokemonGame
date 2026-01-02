using System.Collections;
using PokemonGame.Pokemon;

namespace PokemonGame.Battle.States
{
    public sealed class OpponentFaintedState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly PokemonInstance faintedPokemon;
        private BattleView Battle => machine.BattleView;

        internal OpponentFaintedState(BattleStateMachine machine, PokemonInstance faintedPokemon)
        {
            this.machine = machine;
            this.faintedPokemon = faintedPokemon;
        }

        public void Enter()
        {
            Battle.StartCoroutine(PlaySequence());
        }

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            var audio = Battle.Components.Audio;
            var anim = Battle.Components.Animation;
            var dialogue = Battle.DialogueBox;

            // Play feedback
            audio.PlayPokemonCry(faintedPokemon);
            anim.PlayOpponentDeath();

            // Hide HUD and show message
            yield return anim.PlayOpponentHudExit();
            yield return dialogue.ShowDialogueAndWaitForInput($"Wild {faintedPokemon.Definition.DisplayName}\nfainted!");

            // Transition to the results
            machine.SetState(new VictoryState(machine));
        }
    }
}
