using System.Collections;
using PokemonGame.Characters.Core;
using PokemonGame.Characters.Direction;
using PokemonGame.Characters.Inputs;
using PokemonGame.Characters.States;
using PokemonGame.Pause;
using PokemonGame.Raycasting;
using PokemonGame.Tile;
using PokemonGame.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Characters.Trainers
{
    /// <summary>
    /// Handles a trainer's vision and detection of the player.
    /// Triggers the challenge sequence when the player enters the trainer's line of sight.
    /// </summary>
    public sealed class TrainerVision : MonoBehaviour
    {
        private const float ExclamationDuration = 0.5f;

        [Title("References")]
        [SerializeField, Required]
        [Tooltip("The trainer's state controller used to determine facing direction and movement.")]
        private CharacterStateController controller;

        [SerializeField, Required]
        [Tooltip("The exclamation icon displayed when the trainer spots the player.")]
        private SpriteRenderer exclamationIcon;

        [SerializeField, Required]
        [Tooltip("Settings for raycasting used to detect the player.")]
        private RaycastSettings raycastSettings;

        [SerializeField, Tooltip("How far the trainer can see.")]
        private float viewDistance = 5f;

        private readonly Vector2 visionBoxSize = new(0.9f, 0.9f); // Slightly smaller than 1 tile to avoid overshooting

        private bool spotted;
        private TileMover playerTileMover;

        private void OnEnable()
        {
            if (PlayerRegistry.Player == null)
            {
                return;
            }

            playerTileMover = PlayerRegistry.Player.GetComponent<TileMover>();
            playerTileMover.MoveCompleted += HandleMoveCompleted;
        }

        /// <summary>
        /// Unsubscribes from the player's movement events when disabled.
        /// </summary>
        private void OnDisable()
        {
            if (playerTileMover != null)
            {
                playerTileMover.MoveCompleted -= HandleMoveCompleted;
            }
        }

        /// <summary>
        /// Called after the player completes a move; checks if the player is in vision.
        /// </summary>
        private void HandleMoveCompleted()
        {
            if (spotted)
            {
                return;
            }

            if (CheckForPlayer(out Character foundPlayer))
            {
                spotted = true;
                StartCoroutine(ChallengeSequence(foundPlayer));
            }
        }

        /// <summary>
        /// Checks if the player is within the trainer's line of sight using a BoxCast.
        /// </summary>
        /// <param name="foundPlayer">The player character found, if any.</param>
        /// <returns>True if the player is detected, otherwise false.</returns>
        private bool CheckForPlayer(out Character foundPlayer)
        {
            foundPlayer = null;

            Vector2 direction = controller.FacingDirection.ToVector2Int();
            Vector2 origin = (Vector2)transform.position + raycastSettings.RaycastOffset;

            RaycastHit2D hit = Physics2D.BoxCast(
                origin,
                visionBoxSize,
                0f,
                direction,
                viewDistance,
                raycastSettings.InteractionMask
            );

            if (hit && hit.collider.CompareTag("Player"))
            {
                foundPlayer = hit.collider.GetComponent<Character>();
            }

            return foundPlayer != null;
        }

        /// <summary>
        /// Executes the trainer's challenge sequence: alert, approach the player, face them, and start interaction.
        /// </summary>
        /// <param name="targetPlayer">The player character that was detected.</param>
        private IEnumerator ChallengeSequence(Character targetPlayer)
        {
            PauseManager.SetPaused(true);

            var trainerInput = GetComponent<TrainerInput>();
            var interaction = GetComponent<TrainerInteractable>();
            var playerState = targetPlayer.GetComponent<CharacterStateController>();

            // 1. Alert icon
            exclamationIcon.gameObject.SetActive(true);
            yield return new WaitForSeconds(ExclamationDuration);
            exclamationIcon.gameObject.SetActive(false);

            // 2. Walk toward player
            Vector2Int playerTile = Vector2Int.RoundToInt(targetPlayer.transform.position);
            Vector2Int approachTile = playerTile - controller.FacingDirection.ToVector2Int();
            InputDirection moveDir = controller.FacingDirection.ToInputDirection();

            while (Vector2Int.RoundToInt(transform.position) != approachTile)
            {
                if (!controller.TileMover.CanMoveInDirection(controller.FacingDirection))
                {
                    break;
                }

                trainerInput.ForcedDirection = moveDir;
                yield return null;
            }

            trainerInput.ForcedDirection = InputDirection.None;
            yield return new WaitUntil(() => !controller.TileMover.IsMoving);

            // 3. Face player and delegate interaction
            playerState.Reface(controller.FacingDirection.Opposite());

            // Start battle
            interaction.Interact(targetPlayer);
        }

        /// <summary>
        /// Draws a visual representation of the trainer's vision in the editor.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (!controller || raycastSettings == null)
            {
                return;
            }

            Vector2 direction = controller.FacingDirection.ToVector2Int();
            Vector2 origin = (Vector2)transform.position + raycastSettings.RaycastOffset;

            VisionGizmoDrawer.DrawBoxCast(
                origin,
                direction,
                viewDistance,
                visionBoxSize,
                spotted ? Color.red : Color.yellow
            );
        }
    }
}
