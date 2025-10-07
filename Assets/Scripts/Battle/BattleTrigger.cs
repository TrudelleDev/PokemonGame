using PokemonGame.SceneManagement;
using UnityEditor;
using UnityEngine;

namespace PokemonGame.Battle
{
    public class BattleTrigger : MonoBehaviour
    {
        [SerializeField] private SceneAsset battleScene;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                string[] scenes = { battleScene.name };
                SceneTransitionManager.Instance.StartTransition(scenes, MapEntry.MapEntryID.None, Transitions.TransitionType.MaskedFade);
            }
        }
    }
}
