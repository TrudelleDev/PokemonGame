using System;
using UnityEngine;

namespace PokemonGame
{
    public class MenuToggler : MonoBehaviour
    {
        public static event Action OnOpenMenu;

        private void Update()
        {
            if (Input.GetKeyDown(Keybind.StartKey))
            {
                OnOpenMenu?.Invoke();
            }
        }
    }
}
