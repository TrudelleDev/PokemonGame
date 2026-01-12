using PokemonGame.Characters.Core;
using PokemonGame.Characters.Direction;
using PokemonGame.Characters.Inputs;
using PokemonGame.Tile;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// Base controller for all characters (Player, NPC).
    /// Manages states, animator, facing direction, and movement.
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class CharacterStateController : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Direction the character faces when the scene starts.")]
        private FacingDirection startingDirection;

        private FacingDirection facingDirection;

        [SerializeField, Required] TileMover tileMover;

        /// <summary>
        /// Tile-based movement handler for this character.
        /// </summary>
        public TileMover TileMover => tileMover;

        /// <summary>
        /// Handles idle state logic.
        /// </summary>
        public CharacterIdleState IdleState { get; private set; }

        /// <summary>
        /// Handles refacing logic (turning without moving).
        /// </summary>
        public CharacterRefacingState RefacingState { get; private set; }

        /// <summary>
        /// Handles walking logic (tile-based movement).
        /// </summary>
        public CharacterWalkingState WalkingState { get; private set; }

        /// <summary>
        /// Handles collision logic (bumping into blocked tiles).
        /// </summary>
        public CharacterCollisionState CollisionState { get; private set; }

        /// <summary>
        /// Animator wrapper for controlling character animations.
        /// </summary>
        public CharacterAnimatorController AnimatorController { get; private set; }

        /// <summary>
        /// Provides directional input (PlayerInput, NPCInput, etc.).
        /// </summary>
        public abstract CharacterInput Input { get; }

        /// <summary>
        /// Optional audio clip to play on collision. Default is none.
        /// </summary>
        public virtual AudioClip CollisionAudioClip => null;

        /// <summary>
        /// Duration in seconds to walk one tile. Default is 0.25s.
        /// </summary>
        public virtual float WalkDuration => 0.25f;

        /// <summary>
        /// The currently active state for this character.
        /// </summary>
        public ICharacterState CurrentState { get; private set; }

        /// <summary>
        /// Current facing direction. Setting this updates animator parameters.
        /// </summary>
        public FacingDirection FacingDirection
        {
            get => facingDirection;
            set
            {
                facingDirection = value;
                AnimatorController?.UpdateDirection(facingDirection);
            }
        }

        protected virtual void Awake()
        {
            AnimatorController = new CharacterAnimatorController(GetComponent<Animator>());

            IdleState = new CharacterIdleState(this);
            RefacingState = new CharacterRefacingState(this);
            WalkingState = new CharacterWalkingState(this);
            CollisionState = new CharacterCollisionState(this);
        }

        /// <summary>
        /// Sets the initial facing direction and enters the idle state.
        /// Called by Unity on the first frame this object is active.
        /// </summary>
        protected virtual void Start()
        {
            FacingDirection = startingDirection;
            SetState(IdleState);
        }

        /// <summary>
        /// Updates the active character state each frame.
        /// If the game is paused, forces the character into idle.
        /// </summary>
        protected virtual void Update()
        {


            CurrentState?.Update();
        }

        /// <summary>
        /// Switches to a new state, calling Exit on the old and Enter on the new.
        /// </summary>
        /// <param name="newState">The state to transition into.</param>
        public void SetState(ICharacterState newState)
        {
            if (newState == null)
            {
                return;
            }

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        /// <summary>
        /// Cancels the current state and forces idle.
        /// Useful for pauses, cutscenes, or resets.
        /// </summary>
        public void CancelToIdle()
        {
            SetState(IdleState);
        }

        /// <summary>
        /// Updates facing direction and plays refacing animation if changed.
        /// </summary>
        /// <param name="newFacing">The new direction to face.</param>
        public void Reface(FacingDirection newFacing)
        {
            if (FacingDirection != newFacing)
            {
                FacingDirection = newFacing;
                AnimatorController.PlayRefacing(newFacing);
            }
        }
    }
}
