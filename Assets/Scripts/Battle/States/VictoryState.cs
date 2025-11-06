using System;
using System.Collections;
using PokemonGame.Pokemons;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Represents the final battle state triggered when the player wins a battle.
    /// Handles victory dialogue, animations, and transitions back to the overworld or results screen.
    /// </summary>
    public class VictoryState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        private bool levelUpOccured;

        private Pokemon playerPokemon;
        private Pokemon opponentPokemon;

        public VictoryState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            Battle.StartCoroutine(PlayVictorySequence());
        }

        private IEnumerator PlayVictorySequence()
        {
             playerPokemon = Battle.PlayerPokemon;
             opponentPokemon = Battle.OpponentPokemon;

          
            int expGained = playerPokemon.CalculateExpGain(opponentPokemon);

            Battle.DialogueBox.ShowDialogue($"{playerPokemon.Definition.DisplayName} gained{Environment.NewLine}{expGained} Exp. Points.", manualArrowControl: true);

            yield return WaitForDialogueCompleted();

            Battle.DialogueBox.Clear();
            playerPokemon.OnLevelChange += OnPlayerLevelUp;
            playerPokemon.AddExperience(expGained);

            yield return WaitForExperienceAnimationComplete();

            // If a level-up happened, show that message
            if (levelUpOccured)
            {
                yield return ShowLevelUpDialogue();
                Battle.DialogueBox.Clear();
            }

         
            yield return new WaitForSecondsRealtime(0.5f);

            // Cleanup and exit
            playerPokemon.OnLevelChange -= OnPlayerLevelUp;
            ViewManager.Instance.CloseTopView();
        }

        private void OnPlayerLevelUp(int newLevel)
        {
            levelUpOccured = true;
        }

        private IEnumerator ShowLevelUpDialogue()
        {
            Battle.DialogueBox.ShowDialogue(
                $"{playerPokemon.Definition.DisplayName} grew to Level {playerPokemon.Level}!", manualArrowControl: true
            );
            yield return WaitForDialogueCompleted();
        }

        private IEnumerator WaitForExperienceAnimationComplete()
        {
            bool done = false;

            void OnComplete()
            {
                done = true;
                Battle.PlayerBattleHud.ExperienceBar.OnExpAnimationFinished -= OnComplete;
            }

            Battle.PlayerBattleHud.ExperienceBar.OnExpAnimationFinished += OnComplete;
            yield return new WaitUntil(() => done);
        }
        private IEnumerator WaitForDialogueCompleted()
        {
            bool done = false;

            void OnComplete()
            {
                done = true;
                Battle.DialogueBox.OnDialogueFinished -= OnComplete;
            }

            Battle.DialogueBox.OnDialogueFinished += OnComplete;
            yield return new WaitUntil(() => done);
        }

        public void Update() { }

        public void Exit() { }
    }
}
