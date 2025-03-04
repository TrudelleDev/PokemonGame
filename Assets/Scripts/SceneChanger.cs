using PokemonGame.Characters;
using UnityEngine;

namespace PokemonGame
{
    public class SceneChanger : MonoBehaviour, ITrigger
    {
        [SerializeField] private Object loadSceneName;
        [SerializeField] private Object unloadSceneName;
        [SerializeField] private Vector3 newPlayerPosition;

        public void Trigger()
        {
            GameManager.Instance.Load(loadSceneName.name);
            GameManager.Instance.SetPlayerPosition(newPlayerPosition);
            GameManager.Instance.Unload(unloadSceneName.name);
        }
    }
}

