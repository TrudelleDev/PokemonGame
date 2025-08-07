using PokemonGame.Abilities.Definition;
using PokemonGame.Characters;
using PokemonGame.Moves;
using PokemonGame.Moves.Definition;
using PokemonGame.Pokemons.Definition;
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


            Load("ViridianForest");

            Screen.SetResolution(1920, 1080, true);
        }

        private async void Start()
        {
            await PokemonDefinitionLoader.LoadAllAsync();
            await AbilityDefinitiondLoader.LoadAllAsync();
            await NatureDefinitionLoader.LoadAllAsync();
            await MoveDefinitionLoader.LoadAllAsync();
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
