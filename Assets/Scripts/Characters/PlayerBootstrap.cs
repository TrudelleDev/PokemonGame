using PokemonGame.Characters.Core;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Registers and unregisters the player character with the <see cref="PlayerRegistry"/>.
    /// Ensures the player character is globally accessible for gameplay systems.
    /// </summary>
    internal sealed class PlayerBootstrap : MonoBehaviour
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
