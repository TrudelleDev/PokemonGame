using System.Collections;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Monster;

namespace MonsterTamer.Battle.States.Player
{
    /// <summary>
    /// Handles the visual sequence for a player monster fainting and branches 
    /// to either party selection or a total blackout.
    /// </summary>
    internal sealed class PlayerFaintedState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private readonly MonsterInstance faintedMonster;
        private BattleView Battle => machine.BattleView;

        internal PlayerFaintedState(BattleStateMachine machine, MonsterInstance faintedMonster)
            => (this.machine, this.faintedMonster) = (machine, faintedMonster);

        public void Enter() => Battle.StartCoroutine(PlaySequence());
        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            var animation = Battle.Components.Animation;
            var dialogue = Battle.DialogueBox;

            // 1. Visuals
            animation.PlayPlayerMonsterDeath();
            animation.PlayPlayerHudExit();

            // 2. Dialogue
            var faintMessage = BattleMessages.FaintedMessage(faintedMonster.Definition.DisplayName);
            yield return dialogue.ShowDialogueAndWaitForInput(faintMessage);

            // 3. Branch Logic
            if (Battle.Player.Party.HasUsablePokemon())
                machine.SetState(new PlayerPartySelectState(machine, isForced: true));      
            else           
                machine.SetState(new PlayerBlackoutState(machine));        
        }
    }
}
