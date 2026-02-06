using System.Collections;
using PokemonGame.Battle.Models;
using PokemonGame.Battle.States.Core;

namespace PokemonGame.Battle.States.Player
{
    /// <summary>
    /// Handles the victory sequence for the player after defeating a wild opponent.
    /// Manages experience gain, level-up notifications, and closing or transitioning the battle view.
    /// </summary>
    internal sealed class PlayerWildVictoryState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Creates a new player wild victory state.
        /// </summary>
        /// <param name="machine">
        /// The battle state machine controlling state transitions.
        /// </param>
        internal PlayerWildVictoryState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Enters the state and begins the victory sequence.
        /// </summary>
        public void Enter()
        {
            Battle.StartCoroutine(PlaySequence());
        }

        /// <summary>
        /// No per-frame logic required for this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// No cleanup required on exit.
        /// </summary>
        public void Exit() { }

        /// <summary>
        /// Orchestrates the transition from winning the fight to rewarding the player
        /// and closing the view or moving to trainer victory if needed.
        /// </summary>
        private IEnumerator PlaySequence()
        {
            yield return ProcessExperienceGain();
            yield return Battle.TurnPauseYield;

            // Close the battle if no trainer is present, else transition
            if (Battle.Opponent == null)
            {
                Battle.CloseBattle();
            }
            else
            {
                machine.SetState(new PlayerTrainerVictoryState(machine));
            }
        }

        /// <summary>
        /// Calculates and applies experience to the player's active Pokémon,
        /// handles level-up logic and UI updates, and waits for the experience bar animation to complete.
        /// </summary>
        private IEnumerator ProcessExperienceGain()
        {
            var player = Battle.PlayerActiveMonster;
            var opponent = Battle.OpponentActiveMonster;
            var dialogue = Battle.DialogueBox;
            var audio = Battle.Components.Audio;
            var expBar = Battle.BattleHUDs.PlayerBattleHud.ExperienceBar;

            // Calculate and display initial reward
            int expGained = player.Experience.CalculateExpGain(opponent);
            string gainMessage = string.Format(BattleMessages.GainExperience, player.Definition.DisplayName, expGained);
            yield return dialogue.ShowDialogueAndWaitForInput(gainMessage);

            // Track level-up
            bool leveledUp = false;
            void OnLevelChange(int _) => leveledUp = true;
            player.Experience.LevelChanged += OnLevelChange;

            // Apply experience (triggers UI updates)
            player.Experience.AddExperience(expGained);

            // Wait for experience bar animation
            yield return expBar.WaitForAnimationComplete();

            if (leveledUp)
            {
                string levelUpMessage = string.Format(BattleMessages.LevelUp, player.Definition.DisplayName, player.Experience.Level);
                yield return dialogue.ShowDialogueAndWaitForInput(levelUpMessage);
            }

            // Clean up event listener
            player.Experience.LevelChanged -= OnLevelChange;
        }
    }
}
