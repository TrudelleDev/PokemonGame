using PokemonGame.Menu.Definition;
using PokemonGame.Shared.Interfaces;
using UnityEngine;

namespace PokemonGame
{
    public class CancelMenuButton : MonoBehaviour, IMenuOptionDisplaySource
    {
        [SerializeField] private CancelMenuOptionDefinition data;

        public IDisplayable Displayable => data;
    }
}