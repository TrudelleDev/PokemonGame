using PokemonGame.Characters.States;
using PokemonGame.Inventory;
using PokemonGame.Party;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters.Core
{
    /// <summary>
    /// Represents a character in the game world.
    /// Provides identity, grid snapping, and access to state control.
    /// </summary>
    [DisallowMultipleComponent]
    public class Character : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("The display name of this character (e.g., NPC name or player).")]
        private string displayName;

        /// <summary>
        /// Unique identifier automatically generated at runtime for this character.
        /// </summary>
        [ShowInInspector, ReadOnly]
        public string ID { get; private set; }

        /// <summary>
        /// The display name of this character (e.g., NPC name or player).
        /// </summary>
        public string DisplayName => displayName;

        /// <summary>
        /// Controls this character’s state (idle, walking, refacing, collision).
        /// </summary>
        public CharacterStateController StateController { get; private set; }

        internal InventoryManager Inventory { get; private set; }

        internal PartyManager Party { get; private set; }

        private void Awake()
        {
            StateController = GetComponent<CharacterStateController>();
            Inventory = GetComponent<InventoryManager>();
            Party = GetComponent<PartyManager>();
        }

        private void Start()
        {
            SnapToGrid();
            ID = new IDGenerator(10000, 99999).GetID();
        }

        /// <summary>
        /// Instantly moves the character to a given position and snaps to the grid.
        /// </summary>
        /// <param name="position">The target world position.</param>
        public void Teleport(Vector3 position)
        {
            transform.position = position;
            SnapToGrid();
        }

        private void SnapToGrid()
        {
            Vector3 pos = transform.position;
            float snappedX = Mathf.Round(pos.x * 2f) / 2f;
            float snappedY = Mathf.Floor(pos.y);
            transform.position = new Vector3(snappedX, snappedY, 0f);
        }
    }
}
