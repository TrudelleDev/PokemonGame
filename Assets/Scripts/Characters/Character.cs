using PokemonGame.Inventory;
using PokemonGame.Party;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Represents a character in the game world.
    /// Provides identity, grid snapping, and access to state control.
    /// </summary>

    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterStateController))]
    internal sealed class Character : MonoBehaviour
    {
        // ID generation range
        private const int MinID = 10000;
        private const int MaxID = 99999;

        // Grid snapping values
        private const float GridSnapX = 0.5f;
        private const float GridSnapY = 1f;

        [SerializeField, Required, Tooltip("Optional trainer definition if this character is a trainer.")]
        private CharacterDefinition definition;

        public CharacterDefinition Definition => definition;
        public string ID { get; private set; }
        public CharacterStateController StateController { get; private set; }
        public InventoryManager Inventory { get; private set; }
        public PartyManager Party { get; private set; }

        private void Awake()
        {
            StateController = GetComponent<CharacterStateController>();
            Inventory = new InventoryManager(definition.InventoryDefinition);
            Party = new PartyManager(definition.PartyDefinition);

            GenerateId();
            SnapToGrid();
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

        private void GenerateId()
        {
            if (string.IsNullOrEmpty(ID))
            {
                ID = new IDGenerator(MinID, MaxID).GetID();
            }
        }

        private void SnapToGrid()
        {
            Vector3 pos = transform.position;
            float snappedX = Mathf.Round(pos.x / GridSnapX) * GridSnapX;
            float snappedY = Mathf.Floor(pos.y / GridSnapY) * GridSnapY;
            transform.position = new Vector3(snappedX, snappedY, 0f);
        }
    }
}