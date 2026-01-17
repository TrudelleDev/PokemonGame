using UnityEngine;

namespace PokemonGame.Characters.Player
{
    /// <summary>
    /// Registers and unregisters the player character with the <see cref="PlayerRegistry"/>.
    /// Ensures the player character is globally accessible for gameplay systems.
    /// </summary>
    public sealed class PlayerBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            PlayerRegistry.Register(GetComponent<Character>());
        }

        private void OnDestroy()
        {
            PlayerRegistry.Unregister(GetComponent<Character>());
        }
    }
}
