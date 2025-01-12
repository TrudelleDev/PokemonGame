using System;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.MenuControllers
{
    public class VerticalMenuController : MenuController
    {    
        protected override void Update()
        {
            if (Input.GetKeyDown(Keybind.DownKey) && currentButtonIndex < interactables.Count - 1)
            {
                currentButtonIndex++;
            }
            if (Input.GetKeyDown(Keybind.UpKey) && currentButtonIndex > 0)
            {
                currentButtonIndex--;
            }
            if (Input.GetKeyDown(Keybind.AcceptKey))
            {
                OnClick();
            }

           UpdateSelection();
        }
    }
}
