using PokemonGame.Characters.Inputs;
using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Manages character state transitions and delegates behavior to the active state.
    /// Handles state logic (idle, walking, refacing, collision) based on input and tile conditions.
    /// </summary>
    public class CharacterStateController : MonoBehaviour
    {
        [Header("Dependencies")]

        [Tooltip("Handles directional input from the player.")]
        [SerializeField] private CharacterInput input;

        [Tooltip("Controls movement and collision checks on the tilemap.")]
        [SerializeField] private TileMover tileMover;

        private Animator animator;

        public CharacterInput Input => input;
        public TileMover TileMover => tileMover;
        public CharacterIdleState IdleState { get; private set; }
        public CharacterRefacingState RefacingState { get; private set; }
        public CharacterWalkingState WalkingState { get; private set; }
        public CharacterCollisionState CollisionState { get; private set; }
        public CharacterAnimatorController AnimatorController { get; private set; }
        public ICharacterState CurrentState { get; private set; }
        public Direction FacingDirection { get; set; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            AnimatorController = new CharacterAnimatorController(animator);
            FacingDirection = Direction.Down;

            // Initialize states
            IdleState = new CharacterIdleState(this);
            RefacingState = new CharacterRefacingState(this);
            WalkingState = new CharacterWalkingState(this);
            CollisionState = new CharacterCollisionState(this);
        }

        private void Start()
        {
            SetState(IdleState);
        }

        private void Update()
        {
            CurrentState?.Update();
        }

        /// <summary>
        /// Transitions to a new character state.
        /// Calls exit on the current state and enter on the new one.
        /// </summary>
        /// <param name="newState">The new state to switch to.</param>
        public void SetState(ICharacterState newState)
        {
            if (newState == null) return;

            UpdateAnimatorParameters();

            Debug.Log($"Transitioning from {CurrentState?.GetType().Name} to {newState.GetType().Name}");

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        /// <summary>
        /// Updates animator direction based on input.
        /// Called during state changes.
        /// </summary>
        public void UpdateAnimatorParameters()
        {
            if (Input.CurrentDirection != Direction.None)
            {
                AnimatorController.UpdateDirection(Input.CurrentDirection);
            }
        }
    }
}
