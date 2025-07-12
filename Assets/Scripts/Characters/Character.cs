using PokemonGame.Characters.States;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters
{
    public class Character : MonoBehaviour
    {
        private const float CENTER_OFFSET = 0.5f;

        [SerializeField, Required] 
        protected string characterName;

        private CharacterStateController stateController;
        public Direction CurrentDirection => stateController.FacingDirection;

        public string CharacterName => characterName;
        public string ID { get; private set; }


        private void Start()
        {
            // Make sure the position are rounded values before offsetting the character.
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);

            // Offset the character to the middle center of the tile in x axis.    
            transform.position -= new Vector3(CENTER_OFFSET, 0, 0);

            stateController = GetComponent<CharacterStateController>();

            IDGenerator generator = new IDGenerator(10000, 99999);
            ID = generator.GetID();
        }
    }
}
