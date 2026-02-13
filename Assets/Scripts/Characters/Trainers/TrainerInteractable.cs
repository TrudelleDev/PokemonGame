using MonsterTamer.Audio;
using MonsterTamer.Battle;
using MonsterTamer.Characters.Directions;
using MonsterTamer.Characters.Interfaces;
using MonsterTamer.Dialogue;
using MonsterTamer.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace MonsterTamer.Characters.Trainers
{
    /// <summary>
    /// Handles trainer interaction logic, including pre- and post-battle dialogue
    /// and initializing trainer battles.
    /// </summary>
    internal sealed class TrainerInteractable : MonoBehaviour, IInteractable
    {
        private bool hasBattled;
        private Character player;
        private Character trainer;
        private CharacterStateController trainerStateController;
        private CharacterStateController playerStateController;

        private void Awake()
        {
            trainer = GetComponent<Character>();
            trainerStateController = GetComponent<CharacterStateController>();
        }

        /// <summary>
        /// Called when the player interacts with this trainer.
        /// Locks the player and triggers dialogue/battle as needed.
        /// </summary>
        /// <param name="player">The player character interacting with the trainer.</param>
        public void Interact(Character player)
        {
            this.player = player;
            playerStateController = player.GetComponent<CharacterStateController>();
            trainerStateController.Reface(playerStateController.FacingDirection.Opposite());

            if (hasBattled)
            {
                OverworldDialogueBox.Instance.Dialogue.ShowDialogue(trainer.Definition.PostEventDialogue);
                return;
            }
       
            playerStateController.Lock(); // Lock the player for the interaction

            // Pre-battle dialogue
            OverworldDialogueBox.Instance.Dialogue.DialogueFinished += OnPreBattleDialogueFinished;
            OverworldDialogueBox.Instance.Dialogue.ShowDialogue(trainer.Definition.DefaultInteractionDialogue);
        }

        private void OnPreBattleDialogueFinished()
        {
            OverworldDialogueBox.Instance.Dialogue.DialogueFinished -= OnPreBattleDialogueFinished;

            BattleView battle = ViewManager.Instance.Show<BattleView>();
            battle.InitializeTrainerBattle(player, trainer);
            battle.OnBattleViewClose += OnBattleFinished;
            hasBattled = true;
        }

        private void OnBattleFinished()
        {
            if (playerStateController != null)
            {
                playerStateController.Unlock();
            }
        }
    }
}
