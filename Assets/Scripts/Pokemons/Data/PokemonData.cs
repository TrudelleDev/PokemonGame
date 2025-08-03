using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    /// <summary>
    /// Immutable data for a Pokémon.
    /// </summary>
    [CreateAssetMenu(fileName = "NewPokemonData", menuName = "ScriptableObjects/Pokemon Data")]
    public class PokemonData : ScriptableObject
    {
        // ------------- Identity -------------

        [BoxGroup("Identity")]
        [Tooltip("Name shown in UI.")]
        [SerializeField, Required] private string displayName;

        [BoxGroup("Identity")]
        [Tooltip("Stable unique identifier for this Pokémon.")]
        [SerializeField, Required] private PokemonID pokemonID;

        // ------------- Attributes -------------

        [Space]
        [BoxGroup("Attributes")]
        [Tooltip("Primary and optional secondary type of the Pokémon.")]
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
        [Tooltip("Front/back/battle/menu sprites for this Pokémon.")]
        [SerializeField] private PokemonSprites sprites;

        [Space]
        [BoxGroup("Visuals")]
        [Tooltip("Animator override for animating the Pokémon in menus.")]
        [SerializeField] private AnimatorOverrideController menuSpriteOverrider;

        // ------------- Public Accessors ------------

        [BoxGroup("Identity")]
        [ShowInInspector, ReadOnly]
        public string PokedexNumber => ((int)pokemonID).ToString("D3");
        public string DisplayName => displayName;
        public PokemonID PokemonID => pokemonID;
        public PokemonType Types => types;
        public PokemonGenderRatio GenderRatio => genderRatio;
        public PokemonStats BaseStats => baseStats;
        public PokemonSprites Sprites => sprites;
        public AnimatorOverrideController MenuSpriteOverrider => menuSpriteOverrider;
    }
}
