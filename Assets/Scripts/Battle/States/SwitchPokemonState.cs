using System.Collections;
using PokemonGame.Dialogue;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the entire sequence of switching the player's active Pokémon during battle.
    /// Plays recall and send animations, dialogues, and transitions to the opponent's turn.
    /// </summary>
    public sealed class SwitchPokemonState : IBattleState
    {
        private const float TurnDelay = 0.5f;

        private readonly BattleStateMachine machine;
        private BattleView BattleView => machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchPokemonState"/>.
        /// </summary>
        /// <param name="machine">The <see cref="BattleStateMachine"/> controlling the battle flow.</param>
        public SwitchPokemonState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Begins the asynchronous Pokémon switch sequence when entering this state.
        /// </summary>
        public void Enter()
        {
            BattleView.StartCoroutine(PlaySwitchSequence());
        }

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySwitchSequence()
        {
            var animation = BattleView.Components.Animation;
            var audio = BattleView.Components.Audio;
            var dialogue = BattleView.DialogueBox;

            // 1. Wait for any pending screen transitions
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            // --- Recall Current Pokémon ---

            // Show dialogue: "[Name], that's enough!"
            yield return ShowRecallDialogue(dialogue);

            yield return audio.PlayOpenPokeball();
            yield return animation.PlayPlayerWithdrawPokemon();
            yield return animation.PlayPlayerBattleHudExit();

            // 2. Perform the internal swap in the BattleView (updates the active Pokémon)
            // Note: This assumes BattleView handles selecting the new Pokémon from the party queue.
            BattleView.TemporarySwap();

            // --- Send New Pokémon ---

            // Show dialogue: "Go [New Name]!"
            yield return ShowSendDialogue(dialogue);

            // Play animations/SFX
            yield return animation.PlayPlayerThrowBallSequence();
            yield return animation.PlayPlayerSendPokemonEnter();
            yield return audio.PlayOpenPokeball();
            audio.PlayPokemonCry(BattleView.PlayerPokemon); // Cry of the *new* active Pokémon
            yield return animation.PlayPlayerHudEnter();

            // 3. Transition to the next turn state
            yield return new WaitForSecondsRealtime(TurnDelay);

            // Note: Use OpponentTurnState for consistency
            machine.SetState(new OpponentTurnState(machine));
        }

        private IEnumerator ShowRecallDialogue(DialogueBox dialogue)
        {
            string name = BattleView.PlayerPokemon.Definition.DisplayName;
            yield return dialogue.ShowDialogueAndWait($"{name}, that's enough!\nCome back!");
        }

        private IEnumerator ShowSendDialogue(DialogueBox dialogue)
        {
            // Note: BattleView.PlayerPokemon now refers to the *new* Pokémon
            string name = BattleView.PlayerPokemon.Definition.DisplayName;
            dialogue.ShowDialogue($"Go {name}!");

            // Wait one frame for dialogue to display before proceeding
            yield return null;
        }
    }
}