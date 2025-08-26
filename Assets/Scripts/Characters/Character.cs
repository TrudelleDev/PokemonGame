using PokemonGame.Characters.States;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Represents a character in the game world.
    /// Handles grid snapping, unique ID generation, and references to the state controller.
    /// </summary>
    public class Character : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("The display name of this character (e.g., NPC name or player).")]
        private string characterName;

        [ShowInInspector, Required]
        [Tooltip("The character's name, read-only at runtime.")]
        public string CharacterName => characterName;

        [ReadOnly, ShowInInspector]
        [Tooltip("Unique identifier automatically generated at runtime for this character.")]
        public string ID { get; private set; }

        private CharacterStateController stateController;

        public CharacterStateController StateController => stateController;

        private void Start()
        {
            stateController = GetComponent<CharacterStateController>();
            SnapToGrid();

            IDGenerator generator = new IDGenerator(10000, 99999);
            ID = generator.GetID();
        }

        private void SnapToGrid()
        {
            Vector3 position = transform.position;
            transform.position = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), 0);
        }
    }
}
