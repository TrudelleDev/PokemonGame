using System.Collections;
using PokemonGame.Characters.Directions;
using PokemonGame.Characters.Player;
using PokemonGame.Raycasting;
using PokemonGame.Tile;
using PokemonGame.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters.Trainers
{
    /// <summary>
    /// Detects the player using line-of-sight and triggers the trainer challenge sequence.
    /// Handles alert icon, movement toward player, facing, and interaction.
    /// </summary>
    [RequireComponent(typeof(TrainerInput), typeof(TrainerInteractable))]
    public sealed class TrainerVision : MonoBehaviour
    {
        private const float ExclamationDuration = 0.5f;
        private const string PlayerTag = "Player";

        [SerializeField, Required]
        [Tooltip("The state controller of this trainer, used to determine facing direction and tile movement.")]
        private CharacterStateController controller;

        [SerializeField, Required]
        [Tooltip("The sprite renderer used to display the exclamation icon when the player is spotted.")]
        private SpriteRenderer exclamationIcon;

        [SerializeField, Required, Tooltip("Raycast settings used for detecting the player.")]
        private RaycastSettings raycastSettings;

        [SerializeField, Tooltip("The maximum distance the trainer can see the player.")]
        private float viewDistance = 5f;

        private readonly Vector2 visionBoxSize = new(0.9f, 0.9f);

        private bool spotted;
        private TileMover playerTileMover;

        private TrainerInput trainerInput;
        private TrainerInteractable trainerInteractable;

        private void Awake()
        {
            trainerInput = GetComponent<TrainerInput>();
            trainerInteractable = GetComponent<TrainerInteractable>();
        }

        private void OnEnable()
        {
            if (PlayerRegistry.Player == null)
                return;

            playerTileMover = PlayerRegistry.Player.GetComponent<TileMover>();
            playerTileMover.MoveCompleted += HandleMoveCompleted;
        }

        private void OnDisable()
        {
            if (playerTileMover != null)
            {
                playerTileMover.MoveCompleted -= HandleMoveCompleted;
            }
        }

        private void HandleMoveCompleted()
        {
            if (spotted)
                return;

            if (TryDetectPlayer(out Character player))
            {
                spotted = true;
                StartCoroutine(ChallengeSequence(player));
            }
        }

        private bool TryDetectPlayer(out Character foundPlayer)
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

            if (hit && hit.collider.CompareTag(PlayerTag))
            {
                foundPlayer = hit.collider.GetComponent<Character>();
            }

            return foundPlayer != null;
        }

        /// <summary>
        /// Executes the trainer challenge flow after the player is detected.
        /// Locks player control, displays an alert icon, moves the trainer toward
        /// the player, aligns facing directions, and finally triggers interaction
        /// to start the battle or dialogue sequence.
        /// </summary>
        /// // <param name="player">The detected player character.</param>
        private IEnumerator ChallengeSequence(Character player)
        {
            var playerState = player.GetComponent<CharacterStateController>();
            playerState.Lock();

            // 1. Show alert icon
            exclamationIcon.gameObject.SetActive(true);
            yield return new WaitForSeconds(ExclamationDuration);
            exclamationIcon.gameObject.SetActive(false);

            // 2. Approach player safely
            Vector2Int playerTile = Vector2Int.RoundToInt(player.transform.position);
            Vector2Int targetTile = GetApproachTile(playerTile, controller.FacingDirection);
            InputDirection moveDir = controller.FacingDirection.ToInputDirection();

            int attempts = 0;
            while (Vector2Int.RoundToInt(transform.position) != targetTile)
            {
                if (!controller.TileMover.CanMoveInDirection(controller.FacingDirection))
                    break;

                trainerInput.ForcedDirection = moveDir;
                yield return null;
                attempts++;
            }

            trainerInput.ForcedDirection = InputDirection.None;
            yield return new WaitUntil(() => !controller.TileMover.IsMoving);

            // 3. Face player and trigger interaction
            playerState.Reface(controller.FacingDirection.Opposite());
            trainerInteractable.Interact(player);
        }

        private Vector2Int GetApproachTile(Vector2Int playerTile, FacingDirection dir)
        {
            return playerTile - dir.ToVector2Int();
        }

        private void OnDrawGizmos()
        {
            if (!controller || raycastSettings == null)
                return;

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
