using System.Threading.Tasks;
using PokemonGame.Abilities.Definition;
using PokemonGame.Characters;
using PokemonGame.Items.Definition;
using PokemonGame.Moves.Definition;
using PokemonGame.Pokemons.Definition;
using PokemonGame.Pokemons.Natures;
using PokemonGame.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PokemonGame
{
    /// <summary>
    /// Bootstraps core data, manages scene loading, and exposes player utilities.
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private Character player;

        
        private  void Start()
        {

           

            ViewManager.Instance.Initialize();
        }
        

        /// <summary>
        /// Sets the player's world position.
        /// </summary>
        public void SetPlayerPosition(Vector3 position)
        {
            player.transform.position = position;
        }

        /// <summary>
        /// Loads a scene additively if it is not already loaded.
        /// </summary>
        public void Load(string sceneName)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }

        /// <summary>
        /// Unloads a scene if it is currently loaded.
        /// </summary>
        public void Unload(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }

        public async Task LoadAsync(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                return;
            }

            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!op.isDone)
            {
                await Task.Yield();
            }
        }
    }
}
