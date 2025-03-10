using PokemonGame.Characters;
using UnityEngine;

namespace PokemonGame
{
    public class WorldObject : MonoBehaviour
    {
        public enum Direction { UP, DOWN, RIGHT, LEFT }

       // public enum InteractionType { }

        [SerializeField] private Direction direction;

        public void Trigger()
        {
            print("Test");
        }
    }
}
