using MonsterTamer.Audio;
using MonsterTamer.Characters.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using MonsterTamer.Transitions;
using MonsterTamer.MapEntry;
using MonsterTamer.Characters;
using MonsterTamer.Characters.Directions;
using MonsterTamer.Characters.Core;





#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MonsterTamer.SceneManagement
{
    /// <summary>
    /// Trigger for transitioning the player to a target scene. 
    /// Applies fades, relocates the player to a defined spawn point,
    /// and can play an optional sound effect.
    /// </summary>
    internal class SceneTransitionTrigger : MonoBehaviour, ITriggerable
    {
#if UNITY_EDITOR
        [Title("Scenes (Editor Only)")]
        [SerializeField, Required]
        [Tooltip("Scene to load on trigger.")]
        private SceneAsset targetScene;
#endif
        [Title("Requirements")]
        [SerializeField, Required]
        [Tooltip("Player must face this direction to activate the trigger.")]
        private FacingDirection requiredFacing;

        [Title("Spawn Location")]
        [SerializeField, Required]
        [Tooltip("Spawn location ID in the target scene.")]
        private MapEntryID spawnLocationId;

        [Title("Transition")]
        [SerializeField, Required]
        [Tooltip("Transition effect for scene change.")]
        private TransitionType sceneTransition;

        [Title("Audio")]
        [SerializeField, Required]
        [Tooltip("Sound effect played during transition (optional).")]
        private AudioClip transitionSfx;

        // Persisted scene names for runtime (these are serialized so they exist in builds)
        [SerializeField, HideInInspector]
        private string targetSceneName;

#if UNITY_EDITOR
        private void OnValidate()
        {
            targetSceneName = targetScene ? targetScene.name : string.Empty;
        }
#endif

        /// <summary>
        /// Activates the transition for the specified character.
        /// Plays optional audio, then initiates a scene transition
        /// to the target scene at the configured spawn location.
        /// </summary>
        /// <param name="character">The character that activated the trigger (typically the player).</param>
        public void Trigger(Character character)
        {
            SceneTransitionManager transitionController = SceneTransitionManager.Instance;

            if (transitionController == null || transitionController.IsTransitioning)
            {
                return;
            }

            // If not facing correctly, reface & delay transition
            if (character.StateController.FacingDirection != requiredFacing)
            {
                character.StateController.Reface(requiredFacing);
                return;
            }

            if (transitionSfx != null)
            {
                AudioManager.Instance.PlaySFX(transitionSfx);
            }

            transitionController.StartTransition(new[] { targetSceneName }, spawnLocationId, sceneTransition);
        }
    }
}
