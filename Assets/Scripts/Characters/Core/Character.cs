using MonsterTamer.Inventory;
using MonsterTamer.Party;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Characters.Core
{
    /// <summary>
    /// Represents a character in the game world.
    /// Handles identity, grid snapping, and access to state, inventory, and party.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterStateController))]
    internal sealed class Character : MonoBehaviour
    {
        private const int MinID = 10000;
        private const int MaxID = 99999;
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
        /// Instantly moves the character to a world position and snaps to the grid.
        /// </summary>
        public void Teleport(Vector3 position)
        {
            transform.position = position;
            SnapToGrid();
        }

        private void GenerateId()
        {
            if (!string.IsNullOrEmpty(ID)) return;
            ID = new IDGenerator(MinID, MaxID).GetID();
        }

        private void SnapToGrid()
        {
            Vector3 snapedPosition = transform.position;
            transform.position = new Vector3(
                Mathf.Round(snapedPosition.x / GridSnapX) * GridSnapX,
                Mathf.Floor(snapedPosition.y / GridSnapY) * GridSnapY,
                0f
            );
        }
    }
}
