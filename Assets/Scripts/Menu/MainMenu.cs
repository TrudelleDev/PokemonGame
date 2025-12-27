using UnityEngine;
using Sirenix.OdinInspector;
using PokemonGame.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PokemonGame.Menu
{
    /// <summary>
    /// Binds main menu UI buttons (New Game, Continue, Exit) 
    /// to actions provided by <see cref="GameStarter"/>.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(GameStarter))]
    public class MainMenu : MonoBehaviour
    {
        [Title("UI Buttons")]

        [SerializeField, Required]
        [Tooltip("Button that starts a new game.")]
        private MenuButton newGameButton;

        [SerializeField, Required]
        [Tooltip("Button that exits the application.")]
        private MenuButton exitButton;

        private GameStarter mainMenuLoader;

        private void Awake()
        {
            mainMenuLoader = GetComponent<GameStarter>();

            newGameButton.OnSubmitted += OnNewGame;
            exitButton.OnSubmitted += OnExit;
        }

        private void OnDestroy()
        {
            newGameButton.OnSubmitted -= OnNewGame;
            exitButton.OnSubmitted -= OnExit;
        }

        private void OnNewGame()
        {
            mainMenuLoader.StartNewGame();
        }

        private void OnExit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
