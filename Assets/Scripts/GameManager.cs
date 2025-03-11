using PokemonGame.Characters;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PokemonGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Character player;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            Load("Pallet Town");

            Screen.SetResolution(1920, 1080, true);
        }

        public void SetPlayerPosition(Vector3 position)
        {
            player.transform.position = position;
        }

        public void Load(string sceneName)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }

        public void Unload(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }
}
