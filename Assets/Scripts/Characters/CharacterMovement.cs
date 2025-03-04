using UnityEngine;

namespace PokemonGame.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        private Vector3 startPosition;
        private Vector3 endPosition;
        private float interpolationPoint;
  
        public bool IsDestinationReach { get; private set; } = true;

        public bool IsColliding { get;  private set; }

        public void Move(float speed, Vector3 inputDirection)
        {
            // Check for any valid input direction.
            if (inputDirection != Vector3.zero && IsDestinationReach)
            {
                startPosition = transform.position;
                endPosition = transform.position + inputDirection;
                IsDestinationReach = false;            
            }
          
            // Move until reach the destination.
            if (!IsDestinationReach)
            {
                interpolationPoint += (Time.deltaTime) / speed;

                // Move from point A to point B using linear interpolation.
                transform.position = Vector3.Lerp(startPosition, endPosition, interpolationPoint);

                if (transform.position == endPosition)
                {
                    interpolationPoint = 0f;
                    IsDestinationReach = true;
                }
            }
        }
    }
}
