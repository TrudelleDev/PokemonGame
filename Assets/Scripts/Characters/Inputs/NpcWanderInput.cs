using PokemonGame.Characters.Core;
using PokemonGame.Characters.Direction;
using PokemonGame.Characters.States;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters.Inputs
{
    /// <summary>
    /// Provides random wandering input for NPCs.
    /// </summary>
    internal sealed class NpcWanderInput : CharacterInput
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

        private NpcMovementBounds movementBounds;
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
            movementBounds = GetComponent<NpcMovementBounds>();
            stateController = GetComponent<CharacterStateController>();
            SetNextInterval();
        }

        /// <summary>
        /// Decides input for wandering NPCs each frame.
        /// </summary>
        protected override void ReadInput()
        {
            // Only decide input when not already moving
            if (!stateController.TileMover.IsMoving)
            {
                delay += Time.deltaTime;

                if (delay >= currentInterval)
                {
                    delay = 0f;
                    SetNextInterval();

                    // Pick a random direction
                    InputDirection nextDirection = directions[Random.Range(0, directions.Length)];
                    bool shouldReface = Random.value < refaceChance;

                    if (shouldReface)
                    {
                        CurrentDirection = nextDirection; // turn without moving
                    }
                    else if (movementBounds.CanMove(nextDirection.ToVector2Int()))
                    {
                        CurrentDirection = nextDirection; // walk in direction
                    }
                    else
                    {
                        CurrentDirection = InputDirection.None; // path blocked go to idle
                    }

                    return;
                }
            }

            // Default: idle when moving or waiting for next decision
            CurrentDirection = InputDirection.None;
        }

        private void SetNextInterval()
        {
            float min = Mathf.Max(0.1f, baseIntervalBetweenAction - intervalVariation);
            float max = baseIntervalBetweenAction + intervalVariation;
            currentInterval = Random.Range(min, max);
        }
    }
}
