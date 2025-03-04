using System;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame
{
    public class PauseControl : MonoBehaviour
    {
        public static event Action<bool> PauseGame;
        private bool previousState;
        private bool currentState = false;

        private void Update()
        {
            if (ViewManager.Instance.IsHistoryEmpty())
            {
                OnPauseGame(false);
            }
            else
            {
                OnPauseGame(true);
            }
        }

        private void OnPauseGame(bool value)
        {
            if(currentState != previousState)
            {
                PauseGame?.Invoke(value);
                previousState = currentState;
            }
          
        }
    }
}
