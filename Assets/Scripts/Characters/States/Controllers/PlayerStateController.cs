using PokemonGame.Characters.Inputs;
using PokemonGame.Pause;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// State controller for the player character.
    /// Uses <see cref="PlayerInput"/> and overrides walk duration
    /// and collision sound behavior.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerStateController : CharacterStateController
    {
        [SerializeField, Required]
        [Tooltip("Walking animation clip. Its length determines the tile step duration.")]
        private AnimationClip walkAnimationClip;

        [SerializeField]
        [Tooltip("Sound played when colliding with obstacles. Leave empty to disable.")]
        private AudioClip collisionAudioClip;

        private PlayerInput playerInput;

        protected override void Awake()
        {
            base.Awake();
            playerInput = GetComponent<PlayerInput>();
        }

        /// <summary>
        /// Provides the player's input source (WASD/arrow keys, etc.).
        /// </summary>
        public override CharacterInput Input => playerInput;

        /// <summary>
        /// Walk duration is derived from the assigned walk animation clip length.
        /// </summary>
        public override float WalkDuration => walkAnimationClip.length;

        /// <summary>
        /// Audio clip played when colliding with obstacles.
        /// Returns null if no clip is assigned.
        /// </summary>
        public override AudioClip CollisionAudioClip => collisionAudioClip;

        protected override void Update()
        {
            if (PauseManager.IsPaused)
            {
                if (CurrentState != IdleState)
                {
                    SetState(IdleState);
                }

                return;
            }

            base.Update();
        }
    }
}
