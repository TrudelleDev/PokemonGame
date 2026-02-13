using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Opponent;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles XP distribution and level-up sequences immediately after a monster faints.
    /// </summary>
    internal sealed class PlayerGainExperienceState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        internal PlayerGainExperienceState(BattleStateMachine machine) => this.machine = machine;

        public void Enter() => Battle.StartCoroutine(PlaySequence());
        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            var player = Battle.PlayerActiveMonster;
            var opponent = Battle.OpponentActiveMonster; // The one that just fainted
            var dialogue = Battle.DialogueBox;
            var expBar = Battle.BattleHUDs.PlayerBattleHud.ExperienceBar;

            // 1. Calculate Gain
            int expGained = player.Experience.CalculateExpGain(opponent);
            var expGainMessage = BattleMessages.GainExperience(player.Definition.DisplayName, expGained);
            yield return dialogue.ShowDialogueAndWaitForInput(expGainMessage);

            // 2. Track Level Ups
            int levelsGained = 0;
            void OnLevelChange(int _) => levelsGained++;
            player.Experience.LevelChanged += OnLevelChange;

            // 3. Animate Bar
            player.Experience.AddExperience(expGained);
            yield return expBar.WaitForAnimationComplete();

            // 4. Handle Level Up Dialogue
            if (levelsGained > 0)
            {
                var levelUpMessage = BattleMessages.LevelUp(player.Definition.DisplayName, player.Experience.Level);
                yield return dialogue.ShowDialogueAndWaitForInput(levelUpMessage);
            }

            player.Experience.LevelChanged -= OnLevelChange;
            yield return Battle.TurnPauseYield;

            // 5. Determine next state
            DetermineNextState();
        }

        private void DetermineNextState()
        {
            // Check if the opponent is a Trainer AND has more monsters to send out
            if (Battle.Opponent != null && Battle.Opponent.Party.HasUsablePokemon())
            {
                // Opponent sends out next monster
                machine.SetState(new OpponentSendOutState(machine));
            }
            else
            {
                // Battle ends: Trainer or Wild
                machine.SetState(Battle.Opponent != null
                    ? new PlayerTrainerVictoryState(machine)
                    : new PlayerWildVictoryState(machine));
            }
        }
    }
}
