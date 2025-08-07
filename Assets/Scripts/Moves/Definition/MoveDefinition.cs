using PokemonGame.Moves.Enums;
using PokemonGame.Pokemons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Moves.Definition
{
    /// <summary>
    /// Defines a move definition used to describe a Pokémon move's stats, type, and behavior.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMoveDefinition", menuName = "ScriptableObjects/Move Definition")]
    public class MoveDefinition : ScriptableObject
    {
        // ---- Identity ----

        [BoxGroup("Identity")]
        [Tooltip("Stable unique identifier for this move.")]
        [SerializeField, Required]
        private MoveID moveID;

        // ---- Basic Info ----

        [BoxGroup("Basic Info")]
        [Tooltip("Display name of the move.")]
        [SerializeField, Required]
        private string moveName;

        [BoxGroup("Basic Info")]
        [Tooltip("Base power of the move (0 if status move).")]
        [SerializeField, Required]
        private int power;

        [BoxGroup("Basic Info")]
        [Tooltip("Accuracy percentage of the move (0–100).")]
        [SerializeField, Required]
        private int accuracy;

        [BoxGroup("Basic Info")]
        [Tooltip("Maximum number of times the move can be used.")]
        [SerializeField, Required]
        private int powerPoint;

        // ---- Effect ----

        [BoxGroup("Effect")]
        [Tooltip("Description or effect text shown to the player.")]
        [SerializeField, Required, TextArea(5, 10)]
        private string effect;

        // ---- Classification ----

        [BoxGroup("Classification")]
        [Tooltip("Type of the move (e.g., Fire, Water).")]
        [SerializeField, Required]
        private TypeDefinition type;

        [BoxGroup("Classification")]
        [Tooltip("Move category (Physical, Special, or Status).")]
        [SerializeField, Required]
        private MoveCategory category;

        // ---- Properties ----

        /// <summary>
        /// Stable unique identifier for this move.
        /// </summary>
        public MoveID ID => moveID;

        /// <summary>
        /// The display name of the move.
        /// </summary>
        public string MoveName => moveName;

        /// <summary>
        /// The base power of the move. Set to 0 for status moves.
        /// </summary>
        public int Power => power;

        /// <summary>
        /// The accuracy of the move, expressed as a percentage from 0 to 100.
        /// </summary>
        public int Accuracy => accuracy;

        /// <summary>
        /// The number of times the move can be used (PP).
        /// </summary>
        public int PowerPoint => powerPoint;

        /// <summary>
        /// The textual description or effect of the move shown to the player.
        /// </summary>
        public string Effect => effect;

        /// <summary>
        /// The elemental type of the move (e.g., Fire, Water).
        /// </summary>
        public TypeDefinition Type => type;

        /// <summary>
        /// The category of the move: Physical, Special, or Status.
        /// </summary>
        public MoveCategory Category => category;
    }
}
