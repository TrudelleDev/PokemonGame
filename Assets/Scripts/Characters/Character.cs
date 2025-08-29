using PokemonGame.Characters.States;
using PokemonGame.Inventory;
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
        private string displayName;

        [ReadOnly, ShowInInspector]
        [Tooltip("Unique identifier automatically generated at runtime for this character.")]
        public string ID { get; private set; }

        public string DisplayName => displayName;


        private CharacterStateController stateController;
        private InventoryManager inventoryManager;

        public CharacterStateController StateController => stateController;
        public InventoryManager InventoryManager => inventoryManager;

        private void Awake()
        {
            stateController = GetComponent<CharacterStateController>();
            inventoryManager = GetComponent<InventoryManager>();
        }

        private void Start()
        {
            SnapToGrid();
            IDGenerator generator = new IDGenerator(10000, 99999);
            ID = generator.GetID();
        }

        /// <summary>
        /// Instantly moves the character to a given position and snaps to the grid.
        /// </summary>
        /// <param name="position">The world position to teleport to.</param>
        public void Teleport(Vector3 position)
        {
            transform.position = position;
            SnapToGrid();
        }

        private void SnapToGrid()
        {
            Vector3 pos = transform.position;

            // X  lock to 0.0 or 0.5
            float snappedX = Mathf.Round(pos.x * 2f) / 2f;

            // Y  always snap down to whole number
            float snappedY = Mathf.Floor(pos.y);

            transform.position = new Vector3(snappedX, snappedY, 0f);
        }
    }
}
