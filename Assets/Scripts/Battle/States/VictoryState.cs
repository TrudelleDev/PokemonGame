using System.Collections;
using PokemonGame.Audio;
using PokemonGame.Dialogue;
using PokemonGame.Pokemon;
using PokemonGame.Pokemon.UI;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the victory sequence: EXP gain, level-up messages, and exiting the battle.
    /// </summary>
    public sealed class VictoryState : IBattleState
    {
        private readonly BattleStateMachine machine;
        internal BattleView BattleView => machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="VictoryState"/>.
        /// </summary>
        /// <param name="machine">The battle state machine controlling this battle.</param>
        public VictoryState(BattleStateMachine machine)
        {
            this.machine = machine;
        }


        /// <summary>
        /// Called when entering this state. Starts the full victory and EXP processing sequence.
        /// </summary>
        public void Enter()
        {
            BattleView.StartCoroutine(PlayVictorySequence());
        }

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlayVictorySequence()
        {
            var player = BattleView.PlayerPokemon;
            var opponent = BattleView.OpponentPokemon;
            var audio = BattleView.Components.Audio;
            var dialogue = BattleView.DialogueBox;
            var playerHudExpBar = BattleView.BattleHUDs.Player.ExperienceBar;

            // 1. Play the victory music/jingle
            audio.PlayVictory();

            // 2. Process and display EXP gain
            yield return ProcessExperienceGain(player, opponent, dialogue, audio, playerHudExpBar);

            // 3. Final Pause and Cleanup
            yield return new WaitForSecondsRealtime(0.5f);

            // Close the battle view and return to the overworld/map
            ViewManager.Instance.CloseTopView();

            // Note: After closing the view, the machine should ideally stop or transition to a non-battle state.
        }

        private IEnumerator ProcessExperienceGain(
            PokemonInstance player,
            PokemonInstance opponent,
            DialogueBox dialogue,
            BattleAudio audio,
            ExperienceBar expBar)
        {
            int expGained = player.Experience.CalculateExpGain(opponent);

            // Announce EXP gain
            yield return dialogue.ShowDialogueAndWaitForPlayerAdvance(
                $"{player.Definition.DisplayName} gained\n{expGained} Exp. Points.",
                manualArrowControl: true
            );

            // Setup level change handler
            bool leveledUp = false;
            void OnLevelChange(int _) => leveledUp = true;
            player.Experience.OnLevelChange += OnLevelChange;

            // Animate EXP bar fill and apply EXP
            audio.PlayGainExperience();
            player.Experience.AddExperience(expGained);

            // Wait until the experience bar animation is complete
            yield return expBar.WaitForAnimationComplete();
            AudioManager.Instance.StopSFX();

            // Handle level up sequence if it occurred
            if (leveledUp)
            {
                yield return ShowLevelUpDialogue(player, dialogue, audio);
            }

            // Cleanup
            player.Experience.OnLevelChange -= OnLevelChange;
            dialogue.Clear();
        }

        private IEnumerator ShowLevelUpDialogue(PokemonInstance player, DialogueBox dialogue, BattleAudio audio)
        {
            audio.PlayLevelUp();
            yield return dialogue.ShowDialogueAndWait(
                $"{player.Definition.DisplayName} grew to Level {player.Experience.Level}!",
                manualArrowControl: true
            );
        }
    }
}