using UnityEngine;

namespace PokemonGame.Characters
{
    public abstract class Character : MonoBehaviour, IInteract
    {
        private const float CENTER_OFFSET = 0.5f;
        
        [SerializeField] protected string characterName;

        public string CharacterName => characterName;

        public int Money { get; set; }

        public string ID { get; set; }

          

        protected virtual void Start()
        {
            // Make sure the position are rounded values before offsetting the character.
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);

            // Offset the character to the middle center of the tile in x axis.    
           transform.position -= new Vector3(CENTER_OFFSET, 0, 0);

            IDGenerator generator = new IDGenerator(10000, 99999);
            ID = generator.GetID();
        }

        public abstract void Interact(Transform sender);
    }
}
