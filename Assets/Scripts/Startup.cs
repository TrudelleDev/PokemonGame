using System.Threading.Tasks;
using PokemonGame.Abilities.Definition;
using PokemonGame.Items.Definition;
using PokemonGame.Moves.Definition;
using PokemonGame.Pokemons.Definition;
using PokemonGame.Pokemons.Natures;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using PokemonGame.MapEntry.Enums;
using PokemonGame.MapEntry;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PokemonGame
{
    /// <summary>
    /// Loads core game data, then loads GameCore and Initial map scenes additively.
    /// Sets the Initial map as the active scene and unloads Boot.
    /// </summary>
    public class Startup : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Scenes (Editor Only)")]

        [Tooltip("Persistent gameplay systems scene (e.g., managers, UI roots). Drag the scene asset here.")]
        [SerializeField, Required] private SceneAsset gameCoreScene;

        [Tooltip("First map scene that the player should spawn into after boot.")]
        [SerializeField, Required] private SceneAsset initialScene;

        [Tooltip("Optional transition scene (e.g., fade/black screen) shown before loading completes.")]
        [SerializeField, Required] private SceneAsset transitionScene;

        [SerializeField, Required] private SceneAsset UIScene;
        [SerializeField, Required] private SceneAsset playerScene;
#endif
        [SerializeField, Required] private MapEntryID defaultMapEntry;

        private string gameCoreSceneName;
        private string initialSceneName;
        private string transitionSceneName;
        private string UISceneName;
        private string playerSceneName;

#if UNITY_EDITOR
        private void OnValidate()
        {
            gameCoreSceneName = gameCoreScene ? gameCoreScene.name : string.Empty;
            initialSceneName = initialScene ? initialScene.name : string.Empty;
            transitionSceneName = transitionScene ? transitionScene.name : string.Empty;
            UISceneName = UIScene ? UIScene.name : string.Empty;
            playerSceneName = playerScene ? playerScene.name : string.Empty;
        }
#endif

        private async void Start()
        {
            // Load all core definitions sequentially
            await PokemonDefinitionLoader.LoadAllAsync();
            await AbilityDefinitionLoader.LoadAllAsync();
            await NatureDefinitionLoader.LoadAllAsync();
            await MoveDefinitionLoader.LoadAllAsync();
            await ItemDefinitionLoader.LoadAllAsync();

            // Load GameCore and Initial map scenes
            await LoadAdditiveAsync(UISceneName);
            await LoadAdditiveAsync(transitionSceneName);

            await LoadAdditiveAsync(playerSceneName);

            // make sure playerScene has initialized
            await Task.Yield();

            // Set default spawn for New Game
            MapEntryRegistry.SetNextEntry(defaultMapEntry);

            await LoadAdditiveAsync(gameCoreSceneName);
            await LoadAdditiveAsync(initialSceneName);

            // Set Initial map as active scene
            Scene map = SceneManager.GetSceneByName(initialSceneName);
            SceneManager.SetActiveScene(map);

            // Unload Boot scene
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }

        private static async Task LoadAdditiveAsync(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                return;
            }

            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!sceneLoadOperation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}
