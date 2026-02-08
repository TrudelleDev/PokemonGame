using PokemonGame.Audio;
using PokemonGame.Battle;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Dialogue;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters.Trainers
{
    /// <summary>
    /// Handles trainer interaction logic, including pre- and post-battle dialogue
    /// and initializing trainer battles.
    /// </summary>
    internal sealed class TrainerInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Required, Tooltip("Dialogue shown before the trainer battle.")]
        private DialogueDefinition preBattleDialogue;

        [SerializeField, Required, Tooltip("Dialogue shown after the trainer has been defeated.")]
        private DialogueDefinition postBattleDialogue;

        [SerializeField, Required]
        private AudioClip battleBGMClip;


        private bool hasBattled;
        private Character player;
        private Character trainer;
        private CharacterStateController playerStateController;

        private void Awake()
        {
            trainer = GetComponent<Character>();
        }

        /// <summary>
        /// Called when the player interacts with this trainer.
        /// Locks the player and triggers dialogue/battle as needed.
        /// </summary>
        /// <param name="player">The player character interacting with the trainer.</param>
        public void Interact(Character player)
        {
            
            if (hasBattled)
            {
                OverworldDialogueBox.Instance.Dialogue.ShowDialogue(postBattleDialogue.Lines);
                return;
            }

            this.player = player;
            playerStateController = player.GetComponent<CharacterStateController>();
            playerStateController.Lock(); // Lock the player for the interaction

            // Pre-battle dialogue
            OverworldDialogueBox.Instance.Dialogue.DialogueFinished += OnPreBattleDialogueFinished;
            OverworldDialogueBox.Instance.Dialogue.ShowDialogue(preBattleDialogue.Lines);
        }

        private void OnPreBattleDialogueFinished()
        {
            OverworldDialogueBox.Instance.Dialogue.DialogueFinished -= OnPreBattleDialogueFinished;

            AudioManager.Instance.PlayBGM(battleBGMClip);
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
