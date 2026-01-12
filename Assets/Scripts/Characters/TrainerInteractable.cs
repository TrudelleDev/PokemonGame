using PokemonGame.Battle;
using PokemonGame.Characters.Core;
using PokemonGame.Characters.Interfaces;
using PokemonGame.Dialogue;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Triggers a trainer battle and manages pre- and post-battle dialogue.
    /// </summary>
    internal sealed class TrainerInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Required]
        [Tooltip("Dialogue shown when the trainer is first interacted with, before the battle.")]
        private DialogueDefinition preBattleDialogue;

        [SerializeField, Required]
        [Tooltip("Dialogue shown on interactions after the trainer has been defeated.")]
        private DialogueDefinition postBattleDialogue;

        private bool hasBattled;
        private bool interactionLocked;

        private Character player;

        /// <summary>
        /// Handles interaction with the trainer and triggers a battle when appropriate.
        /// </summary>
        /// <param name="player">The player character interacting with this trainer.</param>
        public void Interact(Character player)
        {
            this.player = player;

            if (hasBattled)
            {
                OverworldDialogueBox.Instance.Dialogue.ShowDialogue(postBattleDialogue.Lines);
                return;
            }

            OverworldDialogueBox.Instance.Dialogue.DialogueFinished += OnDialogueFinished;
            OverworldDialogueBox.Instance.Dialogue.ShowDialogue(preBattleDialogue.Lines);
        }

        private void OnDialogueFinished()
        {
            OverworldDialogueBox.Instance.Dialogue.DialogueFinished -= OnDialogueFinished;

            hasBattled = true;

            BattleView battle = ViewManager.Instance.Show<BattleView>();
            Character opponent =  GetComponent<Character>();
            battle.InitializeTrainerBattle(player, opponent);

        }
    }
}
