using PokemonGame.MapEntry;
using Sirenix.OdinInspector;
using UnityEngine;
using PokemonGame.Transitions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PokemonGame.SceneManagement
{
    /// <summary>
    /// Handles starting gameplay sessions from the main menu.
    /// Sets up the player’s initial spawn, validates required scenes,
    /// and transitions into the core gameplay environment.
    /// </summary>
    public class GameStarter : MonoBehaviour
    {
#if UNITY_EDITOR
        [Title("Scenes (Editor Only)")]

        [SerializeField, Required]
        [Tooltip("Gameplay core systems scene.")]
        private SceneAsset coreScene;

        [SerializeField, Required]
        [Tooltip("Initial map scene to enter on New Game.")]
        private SceneAsset initialMapScene;
#endif
        [Title("Gameplay Settings")]

        [SerializeField, Required]
        [Tooltip("Map entry where the player spawns on New Game.")]
        private MapEntryID spawnLocationId;

        [SerializeField, Required]
        [Tooltip("Transition animation used when entering gameplay from the main menu.")]
        private TransitionType startGameTransition;

        // Persisted scene names for runtime (these are serialized so they exist in builds)
        [SerializeField, HideInInspector]
        private string coreSceneName;

        [SerializeField, HideInInspector]
        private string initialMapSceneName;

#if UNITY_EDITOR
        private void OnValidate()
        {
            coreSceneName = coreScene ? coreScene.name : string.Empty;
            initialMapSceneName = initialMapScene ? initialMapScene.name : string.Empty;
        }
#endif

        /// <summary>
        /// Begins a new game session.
        /// Sets the pending spawn location and transitions into the 
        /// Player, Core, and initial Map scenes using the configured transition.
        /// </summary>
        public void StartNewGame()
        {
            string[] sceneNames = {coreSceneName, initialMapSceneName };
            SceneTransitionManager.Instance.StartTransition(sceneNames, spawnLocationId, startGameTransition);
        }

        /// <summary>
        /// Continues a previously saved game.
        /// Placeholder until save/load functionality is implemented.
        /// </summary>
        public void ContinueGame()
        {
            throw new System.NotImplementedException();
        }
    }
}
