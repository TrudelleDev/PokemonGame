using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Player;

namespace MonsterTamer.Battle.States.Intro
{
    /// <summary>
    /// Manages the opening sequence of a trainer battle.
    /// Plays the trainer's introduction dialogue, sends out the first opponent Monster,
    /// and deploys the player's first Monster before transitioning to the player's action menu.
    /// </summary>
    internal sealed class TrainerBattleIntroState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new state for handling the trainer battle introduction sequence.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling the battle flow.
        /// </param>
        internal TrainerBattleIntroState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Enters the state and begins the introduction sequence.
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
        /// No cleanup required on exit.
        /// </summary>
        public void Exit() { }

        /// <summary>
        /// Coordinates the full trainer battle introduction sequence.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            yield return PlayOpponentEntrance();
            yield return PlayPlayerEntrance();

            machine.SetState(new PlayerActionMenuState(machine));
        }

        /// <summary>
        /// Handles the opponent trainer's entrance:
        /// - Displays the trainer introduction dialogue.
        /// - Sends out the opponent's first Monster.
        /// - Plays the associated animations.
        /// </summary>
        private IEnumerator PlayOpponentEntrance()
        {
            var animation = Battle.Components.Animation;
            var monster = Battle.OpponentActiveMonster;
            var trainer = Battle.Opponent.Definition;

            string introMessage = string.Format(BattleMessages.TrainerIntro, trainer.DisplayName);
            string sendMessage = string.Format(BattleMessages.TrainerSentOut, trainer.DisplayName, monster.Definition.DisplayName);

            // Visual entrance of trainer sprite
            animation.PlayPlayerTrainerEnter();
            yield return animation.PlayOpponentTrainerEnter();

            // Dialogue sequence
            yield return Battle.DialogueBox.ShowDialogueAndWaitForInput(introMessage);
            Battle.DialogueBox.ShowDialogue(sendMessage);

            // Transition to Monster sprite and HUD
            yield return animation.PlayOpponentTrainerExit();
            yield return animation.PlayOpponentMonsterEnter();
            yield return animation.PlayOpponentHudEnter();
        }

        /// <summary>
        /// Handles the player's Monster entrance:
        /// - Displays the "Go!" message.
        /// - Plays the player's Pokémon send-out animations and HUD entrance.
        /// </summary>
        private IEnumerator PlayPlayerEntrance()
        {
            var animation = Battle.Components.Animation;
            var monster = Battle.PlayerActiveMonster;
            string sendMessage = string.Format(BattleMessages.PlayerSendMonster, monster.Definition.DisplayName);

            Battle.DialogueBox.ShowDialogue(sendMessage);

            // Withdraw Trainer sprite and deploy Player Pokémon
            yield return animation.PlayPlayerTrainerExit();
            yield return animation.PlayPlayerMonsterEnter();
            yield return animation.PlayPlayerHudEnter();
        }
    }
}
