using PokemonGame.Inventory;
using PokemonGame.Party;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Characters
{
    /// <summary>
    /// Defines a Pokémon trainer's static data.
    /// Used to configure trainer identity, battle appearance, party composition, and starting inventory.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Characters/Charcater Definition")]
    public sealed class CharacterDefinition : ScriptableObject
    {
        [SerializeField, Tooltip("Optional trainer's title (e.g., Bug Catcher, Ace Trainer).")]
        private string title;

        [SerializeField, Required, Tooltip("The name of this trainer as it appears in dialogues.")]
        private string displayName;

        [SerializeField, Required, Tooltip("Sprite shown for this trainer during battles.")]
        private Sprite battleSprite;

        [SerializeField, Required, Tooltip("The Pokémon party this trainer uses in battle.")]
        private PartyDefinition partyDefinition;

        [SerializeField, Required, Tooltip("The starting inventory assigned to this trainer.")]
        private InventoryDefinition inventoryDefinition;

        public string Title => title;
        public string DisplayName => displayName;
        public Sprite BattleSprite => battleSprite;
        public PartyDefinition PartyDefinition => partyDefinition;
        public InventoryDefinition InventoryDefinition => inventoryDefinition;
    }
}
