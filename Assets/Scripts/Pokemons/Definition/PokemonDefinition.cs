using PokemonGame.Ability;
using PokemonGame.Move;
using PokemonGame.Move.Models;
using PokemonGame.Nature;
using PokemonGame.Pokemons.Enums;
using PokemonGame.Pokemons.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Definition
{
    /// <summary>
    /// Defines a Pokémon's core data, types, stats, and visuals.
    /// </summary>
    [CreateAssetMenu(fileName = "NewPokemonDefinition", menuName = "PokemonGame/Pokemon/Definition")]
    public class PokemonDefinition : ScriptableObject
    {
        // ------------- Identity -------------

        [BoxGroup("Identity")]
        [Tooltip("Name shown in UI.")]
        [SerializeField, Required]
        private string displayName;

        [SerializeField, Required]
        private int pokedexNumber;

        // ------------- Attributes -------------

        [Space]
        [BoxGroup("Attributes")]
        [Tooltip("Primary and optional secondary type of the Pokémon.")]
        [SerializeField, Required]
        private PokemonType types;

        [BoxGroup("Attributes")]
        [Tooltip("Male/female ratio information.")]
        [SerializeField, Required]
        private PokemonGenderRatio genderRatio;

        [BoxGroup("Attributes")]
        [Tooltip("Base stats used to calculate the Pokémon's final stats.")]
        [SerializeField, Required]
        private PokemonStats baseStats;

        // -------------- Abilities --------------

        [Space]
        [BoxGroup("Abilities")]
        [SerializeField, Required]
        [Tooltip("All abilities this Pokémon can possibly have.")]
        private AbilityDefinition[] possibleAbilities;

        // -------------- Nature --------------

        [Space]
        [BoxGroup("Natures")]
        [SerializeField, Required]
        [Tooltip("All possible natures this Pokémon can have.")]
        private NatureDatabase possibleNatures;

        // -------------- Moves --------------

        [Space]
        [BoxGroup("Moves")]
        [SerializeField, Required]
        [Tooltip("All moves this Pokémon can learn when leveling up.")]
        private LevelUpMove[] levelUpMoves;

        // -------------- Audio --------------

        [Space]
        [BoxGroup("Audio")]
        [SerializeField, Required]
        [Tooltip("The AudioClip that plays when this Pokémon cries.")]
        private AudioClip cryClip;

        // ------------- Visuals -------------

        [Space]
        [BoxGroup("Visuals")]
        [Tooltip("Front/back/battle/menu sprites for this Pokémon.")]
        [SerializeField]
        private PokemonSprites sprites;

        [Space]
        [BoxGroup("Visuals")]
        [Tooltip("Animator override for animating the Pokémon in menus.")]
        [SerializeField, Required]
        private AnimatorOverrideController menuSpriteOverrider;

        // ------------- Public Accessors ------------


        public int PokedexNumber => pokedexNumber;

        /// <summary>
        /// Display name shown in UI.
        /// </summary>
        public string DisplayName => displayName;


        /// <summary>
        /// Primary and optional secondary types.
        /// </summary>
        public PokemonType Types => types;

        /// <summary>
        /// Male/female gender ratio information.
        /// </summary>
        public PokemonGenderRatio GenderRatio => genderRatio;

        /// <summary>
        /// Base stats used to calculate the Pokémon's final stats.
        /// </summary>
        public PokemonStats BaseStats => baseStats;

        /// <summary>
        /// Gets all abilities this Pokémon can possibly have.
        /// </summary>
        public AbilityDefinition[] PossibleAbilities => possibleAbilities;

        /// <summary>
        /// Gets all possible natures this Pokémon can have.
        /// </summary>
        public NatureDatabase PossibleNatures => possibleNatures;

        /// <summary>
        /// Gets all moves this Pokémon can learn when leveling up.
        /// </summary>
        public LevelUpMove[] LevelUpMoves => levelUpMoves;

        /// <summary>
        /// Gets the AudioClip that plays when this Pokémon cries.
        /// </summary>
        public AudioClip CryClip => cryClip;

        /// <summary>
        /// Sprites for front, back, battle, and menu views.
        /// </summary>
        public PokemonSprites Sprites => sprites;

        /// <summary>
        /// Animator override for Pokémon menu sprite animations.
        /// </summary>
        public AnimatorOverrideController MenuSpriteOverrider => menuSpriteOverrider;
    }
}
