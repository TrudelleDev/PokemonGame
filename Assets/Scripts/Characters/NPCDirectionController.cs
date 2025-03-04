using UnityEngine;

namespace PokemonGame.Characters
{
    public class NPCDirectionController : MonoBehaviour
    {
        [SerializeField] private int xAxisMaxTile;
        [SerializeField] private int yAxisMaxTile;

        private int xAxisTile;
        private int yAxisTile;
        
        private bool CanMoveUp()
        {
            if (yAxisTile < yAxisMaxTile)
            {
                yAxisTile++;
                return true;
            }

            return false;
        }

        private bool CanMoveDown()
        {
            if (yAxisTile > -yAxisMaxTile)
            {
                yAxisTile--;
                return true;
            }

            return false;
        }

        private bool CanMoveLeft()
        {
            if (xAxisTile > -xAxisMaxTile)
            {
                xAxisTile--;
                return true;
            }

            return false;
        }

        private bool CanMoveRight()
        {
            if (xAxisTile < xAxisMaxTile)
            {
                xAxisTile++;
                return true;
            }

            return false;
        }


        public bool CanMoveToNextDirection(Vector3 nextDirection)
        {
            if (nextDirection == Vector3.up)
            {
                return CanMoveUp();
            }
            if (nextDirection == Vector3.down)
            {
                return CanMoveDown();
            }
            if (nextDirection == Vector3.left)
            {
                return CanMoveLeft();
            }
            if (nextDirection == Vector3.right)
            {
                return CanMoveRight();
            }

            return false;
        }
    }
}
