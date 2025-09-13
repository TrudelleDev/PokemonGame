using PokemonGame.SceneManagement;
using UnityEngine;

namespace PokemonGame.Battle
{
    public class BattleTrigger : MonoBehaviour
    {
        [SerializeField] private SceneTransitionTrigger sceneTrigger;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                sceneTrigger.Trigger(null);
            }
        }
    }
}
