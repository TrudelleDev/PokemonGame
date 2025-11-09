using System.Collections.Generic;
using PokemonGame.Abilities;
using PokemonGame.Abilities.Definition;
using PokemonGame.Moves;
using PokemonGame.Pokemons.Enums;
using PokemonGame.Pokemons.Models;
using PokemonGame.Pokemons.Natures;
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

        [BoxGroup("Identity")]
        [Tooltip("Stable unique identifier for this Pokémon.")]
        [SerializeField, Required]
        private PokemonId pokemonID;

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
        private NatureDefinition[] possibleNatures;

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

        /// <summary>
        /// Three-digit string version of the Pokémon ID.
        /// </summary>
        [BoxGroup("Identity")]
        [ShowInInspector, ReadOnly]
        public string PokedexNumber => ((int)pokemonID).ToString("D3");

        /// <summary>
        /// Display name shown in UI.
        /// </summary>
        public string DisplayName => displayName;

        /// <summary>
        /// Unique identifier for this Pokémon.
        /// </summary>
        public PokemonId PokemonID => pokemonID;

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
        public NatureDefinition[] PossibleNatures => possibleNatures;

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
