using System.Collections;
using PokemonGame.Battle.Models;
using PokemonGame.Battle.States.Core;
using PokemonGame.Battle.States.Player;

namespace PokemonGame.Battle.States.Intro
{
    /// <summary>
    /// Handles the introductory sequence for a wild battle encounter.
    /// Plays animations for the wild Monster and the player's active Monster,
    /// displays the introductory dialogue, and transitions to the player's first action.
    /// </summary>
    internal sealed class WildBattleIntroState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new wild battle intro state.
        /// </summary>
        /// <param name="machine">The battle state machine controlling the battle flow.</param>
        internal WildBattleIntroState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Enters the state and begins the intro sequence.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(PlaySequence());
        }

        /// <summary>
        /// No per-frame logic is required for this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// No cleanup is required on exit.
        /// </summary>
        public void Exit() { }

        /// <summary>
        /// Executes the full sequence: wild Monster appearance, player Monster deployment,
        /// and transitions to the PlayerActionMenuState.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            yield return PlayOpponentEntrance();
            yield return PlayPlayerEntrance();

            machine.SetState(new PlayerActionMenuState(machine));
        }

        /// <summary>
        /// Plays the visual and dialogue sequence for the wild Monster appearing.
        /// </summary>
        private IEnumerator PlayOpponentEntrance()
        {
            var animation = Battle.Components.Animation;
            var wildMonsterName = Battle.OpponentActiveMonster.Definition.DisplayName;
            var wildIntro = string.Format(BattleMessages.WildIntro, wildMonsterName);

            animation.PlayPlayerTrainerEnter();

            // Visual deployment of the wild Monster
            yield return animation.PlayWildMonsterEnter();
            yield return animation.PlayOpponentHudEnter();

            yield return Battle.DialogueBox.ShowDialogueAndWaitForInput(wildIntro);
        }

        /// <summary>
        /// Plays the visual and dialogue sequence for the player sending out their active Monster.
        /// </summary>
        private IEnumerator PlayPlayerEntrance()
        {
            var animation = Battle.Components.Animation;
            var playerMonsterName = Battle.PlayerActiveMonster.Definition.DisplayName;
            var sendMonster = string.Format(BattleMessages.PlayerSendMonster, playerMonsterName);

            Battle.DialogueBox.ShowDialogue(sendMonster);

            // Withdraw player trainer sprite and deploy active Monster
            yield return animation.PlayPlayerTrainerExit();
            yield return animation.PlayPlayerMonsterEnter();
            yield return animation.PlayPlayerHudEnter();
        }
    }
}
