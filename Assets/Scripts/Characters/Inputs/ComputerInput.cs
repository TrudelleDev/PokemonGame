using PokemonGame.Characters.States;
using UnityEngine;

namespace PokemonGame.Characters.Inputs
{
    public class ComputerInput : CharacterInput
    {
        [SerializeField] private float intervalBetweenAction = 2f;

        private NPCDirectionController stepCounter;
        private CharacterStateController characterMovement;

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
            characterMovement = GetComponent<CharacterStateController>();
        }

        
        protected override void Update()
        {
            /*
            if (characterMovement.TileMover.IsMoving == false)
            {
                delay += Time.deltaTime;

                if (delay > intervalBetweenAction)
                {
                    Vector3 nextDirection = directions[Random.Range(0, directions.Length)];

                    if (stepCounter.CanMoveToNextDirection(nextDirection))
                    {
                       // inputDirection = nextDirection;
                        delay = 0;
                    }
                }
            }
            else
            {
               // inputDirection = Vector3.zero;
            }

            */
        }
    }
}
