using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    /// <summary>
    /// Represents a Pok�mon species configuration as a ScriptableObject.
    /// Includes core identity, types, gender ratio, base stats, visual references,
    /// and Pok�dex information for use in gameplay and UI.
    /// </summary>
    [CreateAssetMenu(fileName = "NewPokemonData", menuName = "ScriptableObjects/Pokemon Data")]
    public class PokemonData : ScriptableObject
    {
        // ------------- Identity -------------

        [BoxGroup("Identity")]
        [Tooltip("Display name of the Pok�mon (e.g., 'Pikachu').")]
        [SerializeField, Required] private string displayName;

        [BoxGroup("Identity")]
        [Tooltip("Unique identifier used in the Pok�dex.")]
        [Range(1, Pokedex.TotalPokemonCount)]
        [SerializeField] private int pokedexNumber;

        // ------------- Attributes -------------

        [Space]
        [BoxGroup("Attributes")]
        [Tooltip("Primary and optional secondary type of the Pok�mon.")]
        [SerializeField] private PokemonType types;

        [BoxGroup("Attributes")]
        [Tooltip("Male/female ratio information.")]
        [SerializeField] private PokemonGenderRatio genderRatio;

        [BoxGroup("Attributes")]
        [Tooltip("Base stats used for gameplay calculations.")]
        [SerializeField] private PokemonStats baseStats;

        // ------------- Visuals -------------

        [Space]
        [BoxGroup("Visuals")]
        [Tooltip("Front/back/battle/menu sprites for this Pok�mon.")]
        [SerializeField] private PokemonSprites sprites;

        [Space]
        [BoxGroup("Visuals")]
        [Tooltip("Animator override for animating the Pok�mon in menus.")]
        [SerializeField] private AnimatorOverrideController menuSpriteOverrider;

        // ------------- Public Accessors ------------

        public string DisplayName => displayName;
        public int PokedexNumber => pokedexNumber;
        public PokemonType Types => types;
        public PokemonGenderRatio GenderRatio => genderRatio;
        public PokemonStats BaseStats => baseStats;
        public PokemonSprites Sprites => sprites;
        public AnimatorOverrideController MenuSpriteOverrider => menuSpriteOverrider;
    }
}
