using MonsterTamer.Move.Effects;
using MonsterTamer.Move.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Move
{
    /// <summary>
    /// Defines a Monster move, including its stats, type/category, effect behavior, 
    /// and in-game description.
    /// </summary>
    [CreateAssetMenu(menuName = "MonsterTamer/Move/Move Definition")]
    internal sealed class MoveDefinition : ScriptableObject
    {
        [SerializeField, Required, Tooltip("Display name of the move.")]
        private string displayName;

        [SerializeField, Tooltip("Base stats of the move: Power, Accuracy, and PP.")]
        private MoveInfo moveInfo;

        [SerializeField, Tooltip("Classification of the move: Type and Category.")]
        private MoveClassification classification;

        [SerializeField, Tooltip("The effect this move applies (damage, status, etc.).")]
        private MoveEffect moveEffect;

        [SerializeField, Required, TextArea(5, 10)]
        [Tooltip("Description or effect text shown to the player.")]
        private string effect;

        [SerializeField, Tooltip("Sound played when the move is used.")]
        private AudioClip sound;

        /// <summary>
        /// Display name of the move.
        /// </summary>
        internal string DisplayName => displayName;

        /// <summary>
        /// Base stats of the move (Power, Accuracy, PP).
        /// </summary>
        internal MoveInfo MoveInfo => moveInfo;

        /// <summary>
        /// Type and category of the move.
        /// </summary>
        internal MoveClassification Classification => classification;

        /// <summary>
        /// The effect applied by the move (damage, status, etc.).
        /// </summary>
        internal MoveEffect MoveEffect => moveEffect;

        /// <summary>
        /// Description or effect text shown to the player.
        /// </summary>
        internal string Effect => effect;

        /// <summary>
        /// Sound played when the move is used.
        /// </summary>
        internal AudioClip Sound => sound;
    }
}
