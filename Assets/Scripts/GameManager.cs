using PokemonGame.Characters;
using PokemonGame.Pokemons.Abilities;
using PokemonGame.Pokemons.Data;
using PokemonGame.Pokemons.Moves;
using PokemonGame.Pokemons.Natures;
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


           // PokemonDataLoader.PreloadAll();
           // AbilityDataLoader.PreloadAll();
           // NatureDataLoader.PreloadAll();
            //MoveDataLoader.PreloadAll();

            Load("ViridianForest");

            Screen.SetResolution(1920, 1080, true);
        }

        private async void Start()
        {
            await PokemonDataLoader.LoadAllAsync();

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
