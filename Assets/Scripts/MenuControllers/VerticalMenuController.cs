using System;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.MenuControllers
{
    public class VerticalMenuController : MenuController
    {    
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyBind.Down) && currentButtonIndex < interactables.Count - 1)
            {
                currentButtonIndex++;
            }
            if (Input.GetKeyDown(KeyBind.Up) && currentButtonIndex > 0)
            {
                currentButtonIndex--;
            }
            if (Input.GetKeyDown(KeyBind.Accept))
            {
                OnClick();
            }

           UpdateSelection();
        }
    }
}
