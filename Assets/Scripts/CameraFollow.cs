using PokemonGame.Characters;
using UnityEngine;

namespace PokemonGame
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Player player;

        private void LateUpdate()
        {
             transform.position = player.transform.position + new Vector3(0, 1, -5);
        }
    }
}
