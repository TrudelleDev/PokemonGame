using UnityEngine;

namespace PokemonGame.Battle
{
    public class BattleTrigger : MonoBehaviour
    {
        [SerializeField] private SceneTrigger sceneTrigger;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                sceneTrigger.Trigger(null);
            }
        }
    }
}
