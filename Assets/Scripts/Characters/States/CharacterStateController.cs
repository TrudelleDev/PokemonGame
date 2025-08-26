using PokemonGame.Characters.Enums;
using PokemonGame.Characters.Inputs;
using PokemonGame.Pause;
<<<<<<< HEAD
using Sirenix.OdinInspector;
=======
>>>>>>> origin/main
using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Controls character states (idle, walking, refacing, collision).
    /// Handles transitions and delegates logic to the active state.
    /// </summary>
    public class CharacterStateController : MonoBehaviour
    {
        [Header("Animation Settings")] 
        [SerializeField, Required]
        [Tooltip("Walking animation clip. Its length determines the duration of a single tile movement.")]
        private AnimationClip walkClip;

        [Header("Dependencies")]    
        [SerializeField, Required]
        [Tooltip("Handles directional input from the player.")]
        private CharacterInput input;
        
        [SerializeField, Required]
        [Tooltip("Controls movement and collision checks on the tilemap.")]
        private TileMover tileMover;

        [Header("Initial State")]
        [Tooltip("Direction the character faces when the scene starts.")]
        [SerializeField, Required]
        private FacingDirection startingDirection;

        private Animator animator;
        private FacingDirection facingDirection;

        public CharacterInput Input => input;
        public TileMover TileMover => tileMover;
        public float WalkDuration => walkClip.length;

        public CharacterIdleState IdleState { get; private set; }
        public CharacterRefacingState RefacingState { get; private set; }
        public CharacterWalkingState WalkingState { get; private set; }
        public CharacterCollisionState CollisionState { get; private set; }

        public CharacterAnimatorController AnimatorController { get; private set; }
        public ICharacterState CurrentState { get; private set; }
  
        public FacingDirection FacingDirection
        {
            get => facingDirection;
            set
            {
                facingDirection = value;
                AnimatorController?.UpdateDirection(facingDirection);
            }
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            AnimatorController = new CharacterAnimatorController(animator);

            IdleState = new CharacterIdleState(this);
            RefacingState = new CharacterRefacingState(this);
            WalkingState = new CharacterWalkingState(this);
            CollisionState = new CharacterCollisionState(this); 
        }

        private void Start()
        {
            FacingDirection = startingDirection;
            SetState(IdleState);
        }

        private void Update()
        {
            if (PauseManager.IsPaused)
            {
                if (CurrentState != IdleState)
                {
<<<<<<< HEAD
                    SetState(IdleState);
                }
                   
                return;
            }

=======
                    SetState(IdleState); // force Idle when paused
                }
                return;
            }
               
>>>>>>> origin/main
            CurrentState?.Update();
        }

        /// <summary>
        /// Switches to a new state, calling Exit on the old and Enter on the new.
        /// </summary>
        public void SetState(ICharacterState newState)
        {
            if (newState == null)
                return;

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
