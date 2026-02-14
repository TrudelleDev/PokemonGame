using MonsterTamer.Inventory;
using MonsterTamer.Party;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Characters.Core
{
    /// <summary>
    /// Static definition of a trainer character.
    /// 
    /// Contains identity, optional battle configuration,
    /// inventory, and dialogue data.
    /// This data is immutable at runtime.
    /// </summary>
    [CreateAssetMenu(menuName = "MonsterTamer/Characters/Character Definition")]
    internal sealed class CharacterDefinition : ScriptableObject
    {
        [BoxGroup("Identity")]
        [SerializeField, Required, Tooltip("The display name shown in dialogue and battle UI.")]
        private string displayName;

        [BoxGroup("Identity")]
        [SerializeField, Required, Tooltip("Sprite used to represent this trainer during battle.")]
        private Sprite battleSprite;

        [BoxGroup("Battle")]
        [SerializeField, Tooltip("Party used by this trainer during battle. (Optional)")]
        private PartyDefinition partyDefinition;

        [BoxGroup("Battle")]
        [SerializeField, Tooltip("Starting inventory assigned for battle usage. (Optional)")]
        private InventoryDefinition inventoryDefinition;

        [BoxGroup("Dialogues")]
        [SerializeField, TextArea, Tooltip("Default dialogue shown when talking to this character normally.")]
        private string defaultInteractionDialogue;

        [BoxGroup("Dialogues")]
        [SerializeField, TextArea, Tooltip("Dialogue shown after a story event (e.g., trainer has been defeated).")]
        private string postEventDialogue;

        [BoxGroup("Dialogues")]
        [SerializeField, TextArea, Tooltip("Short dialogue shown immediately after a battle concludes.")]
        private string postBattleClosingDialogue;

        internal string DisplayName => displayName;
        internal Sprite BattleSprite => battleSprite;
        internal PartyDefinition PartyDefinition => partyDefinition;
        internal InventoryDefinition InventoryDefinition => inventoryDefinition;
        internal string DefaultInteractionDialogue => defaultInteractionDialogue;
        internal string PostEventDialogue => postEventDialogue;
        internal string PostBattleClosingDialogue => postBattleClosingDialogue;
    }
}
