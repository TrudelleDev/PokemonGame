using UnityEngine;
using UnityEngine.UI;
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
        private Button newGameButton;

        [SerializeField, Required]
        [Tooltip("Button that continues from a saved game (placeholder).")]
        private Button continueButton;

        [SerializeField, Required]
        [Tooltip("Button that exits the application.")]
        private Button exitButton;

        private GameStarter mainMenuLoader;

        private void Awake()
        {
            mainMenuLoader = GetComponent<GameStarter>();

            newGameButton.onClick.AddListener(OnNewGame);
            continueButton.onClick.AddListener(OnContinue);
            exitButton.onClick.AddListener(OnExit);
        }

        private void OnDestroy()
        {
            newGameButton.onClick.RemoveListener(OnNewGame);
            continueButton.onClick.RemoveListener(OnContinue);
            exitButton.onClick.RemoveListener(OnExit);
        }

        private void OnNewGame()
        {
            mainMenuLoader.StartNewGame();
        }

        private void OnContinue()
        {
            // TODO: Replace with save/load system
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
