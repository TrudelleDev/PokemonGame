using System.Collections;
using PokemonGame.Audio;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles the victory sequence: EXP gain, level-up messages, and exiting the battle.
    /// </summary>
    internal sealed class VictoryState : IBattleState
    {
        private const float ClosePause = 1f;

        private readonly BattleStateMachine machine;
        private BattleView BattleView => machine.BattleView;

        internal VictoryState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter() 
        {
            BattleView.StartCoroutine(PlaySequence());
        } 

        public void Update() { }

        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            BattleView.Components.Audio.PlayVictory();

            yield return ProcessExperienceGain();
            yield return new WaitForSecondsRealtime(ClosePause);

            ViewManager.Instance.Close<BattleView>();
        }

        private IEnumerator ProcessExperienceGain()
        {
            var player = BattleView.PlayerActivePokemon;
            var opponent = BattleView.OpponentActivePokemon;
            var dialogue = BattleView.DialogueBox;
            var audio = BattleView.Components.Audio;
            var expBar = BattleView.BattleHUDs.Player.ExperienceBar;

            int expGained = player.Experience.CalculateExpGain(opponent);

            yield return dialogue.ShowDialogueAndWaitForInput($"{player.Definition.DisplayName} gained\n{expGained} Exp. Points.");

            // Setup level change handler
            bool leveledUp = false;
            void OnLevelChange(int _) => leveledUp = true;
            player.Experience.LevelChanged += OnLevelChange;

            // Animate
            audio.PlayGainExperience();
            player.Experience.AddExperience(expGained);

            yield return expBar.WaitForAnimationComplete();
            AudioManager.Instance.StopSFX();

            if (leveledUp)
            {
                audio.PlayLevelUp();
                yield return dialogue.ShowDialogueAndWaitForInput($"{player.Definition.DisplayName} grew to Level {player.Experience.Level}!");
            }

            player.Experience.LevelChanged -= OnLevelChange;
        }
    }
}