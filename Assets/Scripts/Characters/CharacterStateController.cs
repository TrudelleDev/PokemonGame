using MonsterTamer.Characters.Directions;
using MonsterTamer.Characters.Interfaces;
using MonsterTamer.Characters.States;
using MonsterTamer.Pause;
using MonsterTamer.Raycasting;
using MonsterTamer.Tile;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Characters
{
    /// <summary>
    /// Base controller for all characters (Player, NPC).
    /// Manages states, animator, facing direction, and movement.
    /// </summary>
    [DisallowMultipleComponent]
    internal class CharacterStateController : MonoBehaviour
    {
        [SerializeField, Required, Tooltip("Direction the character faces when the scene starts.")]
        private FacingDirection startingDirection;

        [SerializeField, Required, Tooltip("Handles tile-based movement for this character.")]
        private TileMover tileMover;

        [SerializeField, Tooltip("Sound played when colliding with obstacles. Leave empty to disable.")]
        private AudioClip collisionAudioClip;

        [SerializeField, Required, Tooltip("Walking animation clip. Its length determines the tile step duration.")]
        private AnimationClip walkAnimationClip;

        [SerializeField, Required, Tooltip("Raycast configuration that defines how far the character can interact.")]
        private RaycastSettings raycastSettings;

        private FacingDirection facingDirection;
        private bool isLocked;

        public AudioClip CollisionAudioClip => collisionAudioClip;
        public float WalkDuration => walkAnimationClip.length;
        public TileMover TileMover => tileMover;

        public ICharacterState CurrentState { get; private set; }
        public CharacterIdleState IdleState { get; private set; }
        public CharacterRefacingState RefacingState { get; private set; }
        public CharacterWalkingState WalkingState { get; private set; }
        public CharacterCollisionState CollisionState { get; private set; }
        public CharacterAnimatorController AnimatorController { get; private set; }
        public CharacterTriggerHandler TriggerHandler { get; private set; }
        public CharacterInteractionHandler InteractionHandler { get; private set; }
        public CharacterInput Input { get; private set; }

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

        private void Awake()
        {
            var character = GetComponent<Character>();
            var animator = GetComponent<Animator>();
            Input = GetComponent<CharacterInput>();

            TriggerHandler = new CharacterTriggerHandler(character, raycastSettings);
            InteractionHandler = new CharacterInteractionHandler(character, raycastSettings);
            AnimatorController = new CharacterAnimatorController(animator);

            IdleState = new CharacterIdleState(this);
            RefacingState = new CharacterRefacingState(this);
            WalkingState = new CharacterWalkingState(this);
            CollisionState = new CharacterCollisionState(this);
        }

        /// <summary>
        /// Sets the initial facing direction and enters the idle state.
        /// Called by Unity on the first frame this object is active.
        /// </summary>
        private void Start()
        {
            FacingDirection = startingDirection;
            SetState(IdleState);
        }

        /// <summary>
        /// Updates the active character state each frame.
        /// If the game is paused, forces the character into idle.
        /// </summary>
        private void Update()
        {
            if (isLocked || PauseManager.IsPaused )
            {
                if (CurrentState != IdleState)
                {
                    SetState(IdleState);
                }

                return;
            }

            InteractionHandler.Tick();
            CurrentState?.Update();
        }

        /// <summary>
        /// Switches to a new state, calling Exit on the old and Enter on the new.
        /// </summary>
        /// <param name="newState">The state to transition into.</param>
        public void SetState(ICharacterState newState)
        {
            if (newState == null)
                return;

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

        public void Lock()
        {
            isLocked = true;
        }

        public void Unlock()
        {
            isLocked = false;
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
