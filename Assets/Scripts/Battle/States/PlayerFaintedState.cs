using System.Collections;
using PokemonGame.Pokemon;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the sequence when the player's active Pokémon faints.
    /// Transitions to <see cref="LossState"/> or potentially a Pokémon swap state.
    /// </summary>
    internal sealed class PlayerFaintedState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly PokemonInstance faintedPokemon;
        private BattleView Battle => machine.BattleView;

        internal PlayerFaintedState(BattleStateMachine machine, PokemonInstance target)
        {
            this.machine = machine;
            this.faintedPokemon = target;
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

            audio.PlayPokemonCry(faintedPokemon);
            anim.PlayPlayerDeath();

            yield return anim.PlayPlayerBattleHudExit();
            yield return dialogue.ShowDialogueAndWaitForInput($"{faintedPokemon.Definition.DisplayName} fainted!");

            machine.SetState(new LossState(machine));
        }
    }
}
