using UnityEngine;

namespace PokemonGame.Characters.Inputs
{
    public class NPCInput : CharacterInput
    {
        [SerializeField] private float intervalBetweenAction = 2f;

        private NPCDirectionController stepCounter;
        private CharacterMovement characterMovement;

        private float delay;

        private readonly Vector3[] directions = new Vector3[4]
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right
        };

        private void Awake()
        {
            stepCounter = GetComponent<NPCDirectionController>();
            characterMovement = GetComponent<CharacterMovement>();
        }

        public override void HandleInputs()
        {
            if (characterMovement.IsDestinationReach)
            {
                delay += Time.deltaTime;

                if (delay > intervalBetweenAction)
                {
                    Vector3 nextDirection = directions[Random.Range(0, directions.Length)];

                    if (stepCounter.CanMoveToNextDirection(nextDirection))
                    {
                        inputDirection = nextDirection;
                        delay = 0;
                    }
                }
            }
            else
            {
                inputDirection = Vector3.zero;
            }
        }
    }
}
