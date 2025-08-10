using PokemonGame.Pokemons.Enums;
using PokemonGame.Pokemons.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Definition
{
    /// <summary>
    /// Defines a Pokémon's core data, types, stats, and visuals.
    /// </summary>
    [CreateAssetMenu(fileName = "NewPokemonDefinition", menuName = "Pokemon/Pokemon Definition")]
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
        /// Sprites for front, back, battle, and menu views.
        /// </summary>
        public PokemonSprites Sprites => sprites;

        /// <summary>
        /// Animator override for Pokémon menu sprite animations.
        /// </summary>
        public AnimatorOverrideController MenuSpriteOverrider => menuSpriteOverrider;
    }
}
