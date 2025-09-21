using PokemonGame.Abilities.Definition;
using PokemonGame.Items.Definition;
using PokemonGame.Moves.Definition;
using PokemonGame.Pokemons.Definition;
using PokemonGame.Pokemons.Natures;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PokemonGame.SceneManagement
{
    /// <summary>
    /// Bootstraps the game by loading core data definitions and essential scenes
    /// (audio, transition, main menu). Sets the main menu as the active scene
    /// and unloads the temporary boot scene once initialization is complete.
    /// </summary>
    public class Bootstrap : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Scenes (Editor Only)")]

        [SerializeField, Required]
        [Tooltip("Main menu scene shown after boot.")]
        private SceneAsset mainMenuScene;

        [SerializeField, Required]
        [Tooltip("Optional transition scene (fade/black screen).")]
        private SceneAsset transitionScene;

        [SerializeField, Required]
        [Tooltip("Scene containing global audio manager and mixers.")]
        private SceneAsset audioScene;
#endif

        // Persisted scene names for runtime (these are serialized so they exist in builds)
        [SerializeField, HideInInspector]
        private string mainMenuSceneName;

        [SerializeField, HideInInspector]
        private string transitionSceneName;

        [SerializeField, HideInInspector]
        private string audioSceneName;


#if UNITY_EDITOR
        private void OnValidate()
        {
            mainMenuSceneName = mainMenuScene ? mainMenuScene.name : string.Empty;
            transitionSceneName = transitionScene ? transitionScene.name : string.Empty;
            audioSceneName = audioScene ? audioScene.name : string.Empty;
        }
#endif

        private async void Start()
        {
            // Load definitions before showing the menu
            await PokemonDefinitionLoader.LoadAllAsync();
            await AbilityDefinitionLoader.LoadAllAsync();
            await NatureDefinitionLoader.LoadAllAsync();
            await MoveDefinitionLoader.LoadAllAsync();
            await ItemDefinitionLoader.LoadAllAsync();

            // Load scenes
            await SceneLoader.LoadAdditiveAsync(audioSceneName);
            await SceneLoader.LoadAdditiveAsync(transitionSceneName);
            await SceneLoader.LoadAdditiveAsync(mainMenuSceneName);

            SceneLoader.SetActive(mainMenuSceneName);

            // Unload boot scene
            await SceneLoader.UnloadAsync(gameObject.scene.name);
        }
    }
}
