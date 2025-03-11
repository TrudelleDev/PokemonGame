using PokemonGame.Characters.Inputs;
using UnityEngine;

namespace PokemonGame.Characters
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Inputs.CharacterInput input;
        [SerializeField] private LayerMask layerMask;
        [Space]
        [SerializeField] private AnimationClip walkingAnimationClip; // each walking animation clip need to have the same lenght.

        private CharacterMovement movement;
        private Animator animator;
        private FootController footController;

        private CharacterState state = CharacterState.Idle;
        private RaycastHit2D hit;

        private float walkingAnimationLenght;
        private bool isRefacing;

        public Vector3 FacingDirection { get; private set; }

        public bool IsMoving { get; private set; }

        private void Awake()
        {
            footController = new FootController();
            animator = GetComponent<Animator>();
            movement = GetComponent<CharacterMovement>();
        }

        private void Start()
        {
            // The speed of the player is calculated with the animation clip lenght.
            walkingAnimationLenght = walkingAnimationClip.length;
            FacingDirection = Vector3.down;
        }


        private void Update()
        {
            if (PauseControl.IsGamePaused)
                return;

            input.HandleInputs();
            UpdateAnimatorParameters();
            UpdateState();
            UpdateFacingDirection();

            if (input.InputDirection == Vector3.zero && movement.IsDestinationReach)
            {
                state = CharacterState.Idle;
                IsMoving = false;
            }
            else
            {
                IsMoving = true;
            }

            Debug.Log("Current State: " + state);
        }

        private void UpdateAnimatorParameters()
        {
            if (input.InputDirection != Vector3.zero && movement.IsDestinationReach)
            {
                animator.SetFloat("Vertical", input.InputDirection.y);
                animator.SetFloat("Horizontal", input.InputDirection.x);
            }

            animator.SetBool("IsDestinationReach", movement.IsDestinationReach);
            animator.SetBool("IsRefacing", isRefacing);
            animator.SetInteger("CurrentFoot", footController.CurrentFoot);
        }

        private void UpdateState()
        {
            switch (state)
            {
                case CharacterState.Idle:
                    IdleState();
                    break;
                case CharacterState.Refacing:
                    RefacingState();
                    break;
                case CharacterState.Walking:
                    WalkingState();
                    break;
            }
        }

        private void RefacingState()
        {
            isRefacing = true;
        }

        private void IdleState()
        {
            if (FacingDirection != input.InputDirection)
            {
                state = CharacterState.Refacing;
            }
            else if (input.InputDirection != Vector3.zero)
            {
                state = CharacterState.Walking;
            }
        }

        private void WalkingState()
        {
            if (movement.IsDestinationReach)
            {
                Vector3 rayCastOrigin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

                hit = Physics2D.Raycast(rayCastOrigin, new Vector2(input.InputDirection.x, input.InputDirection.y),1, layerMask);

                // Check if there a collision with entity that are not related to the tilemap.
                if (hit)
                    return;

                if (!movement.IsColliding)
                {
                    footController.ChangeFoot();
                }
            }

            movement.Move(walkingAnimationLenght, input.InputDirection);
        }

        public void UpdateFacingDirection()
        {
            if (input.InputDirection != Vector3.zero)
            {
                if (FacingDirection != input.InputDirection)
                {
                    FacingDirection = input.InputDirection;
                }
            }
        }

        public void LookAt(Vector3 direction)
        {
            FacingDirection = direction;
            animator.SetFloat("Vertical",direction.y);
            animator.SetFloat("Horizontal", direction.x);
        }

        // This function is called in every refacing animation clip.
        private void FinishRefacing()
        {
            isRefacing = false;
            state = CharacterState.Idle;
        }
    }
}
