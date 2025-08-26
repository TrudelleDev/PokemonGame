using PokemonGame.Characters.Inputs.Enums;
using PokemonGame.Characters.Inputs.Extensions;
using PokemonGame.Characters.States;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters.Inputs
{
    /// <summary>
    /// Provides random wandering input for NPCs.
    /// </summary>
    [RequireComponent(typeof(CharacterMovementBounds))]
    [RequireComponent(typeof(CharacterStateController))]
    public class ComputerInput : CharacterInput
    {
        [SerializeField, Required]
        [Tooltip("Average time between random movement decisions.")]
        private float baseIntervalBetweenAction = 2f;

        [SerializeField, Required]
        [Tooltip("Random variation added/subtracted to the base interval.")]
        private float intervalVariation = 0.5f;

        [SerializeField, Required, Range(0f, 1f)]
        [Tooltip("Chance the NPC will only reface instead of walking.")]
        private float refaceChance = 0.25f;

        private float delay;
        private float currentInterval;

        private CharacterMovementBounds movementBounds;
        private CharacterStateController stateController;

        private static readonly InputDirection[] directions =
        {
            InputDirection.Up,
            InputDirection.Down,
            InputDirection.Left,
            InputDirection.Right
        };

        private void Awake()
        {
            movementBounds = GetComponent<CharacterMovementBounds>();
            stateController = GetComponent<CharacterStateController>();
            SetNextInterval();
        }

        protected override void Update()
        {
            // Only consider action if NPC is not already moving
            if (!stateController.TileMover.IsMoving)
            {
                delay += Time.deltaTime;

                // Time to decide next action
                if (delay >= currentInterval)
                {
                    delay = 0f;
                    SetNextInterval();

                    // Pick a random direction
                    InputDirection nextDirection = directions[Random.Range(0, directions.Length)];
                    float roll = Random.value;

                    // Chance to only reface without walking
                    if (roll < refaceChance)
                    {
                        InputDirection = nextDirection;
                        return;
                    }

                    // Try walking in that direction if it's allowed
                    if (movementBounds.TryMove(nextDirection.ToVector2Int()))
                    {
                        InputDirection = nextDirection;
                    }
                    else
                    {
                        // Path blocked, stay idle
                        InputDirection = InputDirection.None;
                    }
                }
            }
            else
            {
                // Reset input while currently moving
                InputDirection = InputDirection.None;
            }
        }

        private void SetNextInterval()
        {
            float min = Mathf.Max(0.1f, baseIntervalBetweenAction - intervalVariation);
            float max = baseIntervalBetweenAction + intervalVariation;
            currentInterval = Random.Range(min, max);
        }
    }
}
