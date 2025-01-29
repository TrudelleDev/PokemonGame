using UnityEngine;

namespace PokemonGame.Characters
{
    public class PlayerController : MonoBehaviour
    {
        private const int LeftFoot = 1;
        private const int RightFoot = 2;

        private bool isDestinationReach = true;
        private int currentFoot = LeftFoot;

        private PlayerState state = PlayerState.Idle;
        private Animator animator;

        private Vector3 inputDirection = Vector3.down;
        private Vector3 facingDirection = Vector3.down;
        private Vector3 startPosition = Vector3.zero;
        private Vector3 endPosition = Vector3.zero;

        private float interpolationPoint;
        private float currentAnimationSpeed;
        private float walkingAnimationSpeed;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            // Get the length of any walking animation.
            // They all have the same animation clips length.
            // The movement speed of the player is determined by the animation length.
            walkingAnimationSpeed = Utility.GetAnimationLength(animator, "RedWalkingDownLeftFoot"); 
            currentAnimationSpeed = walkingAnimationSpeed;
        }

        private void Update()
        {
            HandleInput();
            UpdateAnimatorParameters();
            HandleState();

            if (inputDirection == Vector3.zero)
            {
                state = PlayerState.Idle;
            }
        }

        private void HandleInput()
        {
            if (inputDirection.y == 0) // Disable diagolal movement
            {
                inputDirection.x = Input.GetAxisRaw("Horizontal");
            }
            if (inputDirection.x == 0)  // Disable diagolal movement
            {
                inputDirection.y = Input.GetAxisRaw("Vertical");
            }
        }

        private void UpdateAnimatorParameters()
        {
            if (inputDirection != Vector3.zero && isDestinationReach)
            {
                animator.SetFloat("Vertical", inputDirection.y);
                animator.SetFloat("Horizontal", inputDirection.x);
            }

            animator.SetBool("IsDestinationReach", isDestinationReach);
            animator.SetInteger("CurrentFoot", currentFoot);
        }

        private void ChangeFoot()
        {
            if (currentFoot == LeftFoot)
            {
                currentFoot = RightFoot;
            }
            else if (currentFoot == RightFoot)
            {
                currentFoot = LeftFoot;
            }
        }

        // This function is called in the animation clip
        private void FinishRefacing()
        {
            state = PlayerState.Idle;
        }

        private void HandleState()
        {
            if (state == PlayerState.Idle)
            {
                if (Refacing())
                {
                    state = PlayerState.Refacing;
                }
                else
                {
                    state = PlayerState.Walking;
                }

                animator.SetBool("IsRefacing", false);
            }
            if (state == PlayerState.Walking)
            {
                Move();
            }
        
            if (state == PlayerState.Refacing)
            {
                animator.SetBool("IsRefacing", true);
            }         
        }
        
        private bool Refacing()
        {
            // Only update the facing direction when the player is moving.
            if (inputDirection == Vector3.zero)
                return false;

            // The player is not facing the same direction as the new input direction.
            if (facingDirection != inputDirection && isDestinationReach)
            {
                facingDirection = inputDirection;
                return true;
            }

            // the player is facing the same direction as the new input direction.
            else
            {
                facingDirection = inputDirection;
                return false;
            }
        }

        private void Move()
        {
            // Check if a movement input key has been pressed and the the destination is not reached.
            if (inputDirection != Vector3.zero && isDestinationReach)
            {
                // Set the starting position.
                startPosition = transform.position;
                // Set the destination position.
                endPosition = transform.position + inputDirection;

                isDestinationReach = false;
            }

            // Move the player until he reach is destination
            if (isDestinationReach == false)
            {
                interpolationPoint += (Time.deltaTime) / currentAnimationSpeed;

                // Move the player from point A to point B using linear interpolation
                transform.position = Vector3.Lerp(startPosition, endPosition, interpolationPoint);

                if (transform.position == endPosition)
                {
                    interpolationPoint = 0f;
                    isDestinationReach = true;

                    // Switch animation
                    ChangeFoot();
                }
            }
        }
    }
}
