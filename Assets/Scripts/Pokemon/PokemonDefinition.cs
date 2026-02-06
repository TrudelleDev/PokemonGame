using PokemonGame.Move.Models;
using PokemonGame.Nature;
using PokemonGame.Pokemon.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemon
{
    /// <summary>
    /// Defines a Pokémon's core data, types, stats, and visuals.
    /// </summary>
    [CreateAssetMenu(menuName = "PokemonGame/Pokemon/Pokemon Definition")]
    public class PokemonDefinition : ScriptableObject
    {
        // ------------- Identity -------------
        [BoxGroup("Identity")]
        [SerializeField, Required, Tooltip("Name shown in UI.")]
        private string displayName;

        [BoxGroup("Identity")]
        [SerializeField, Required, Range(0, Pokedex.TotalPokemonCount)]
        [Tooltip("National Pokédex number of this Pokémon.")]
        private int pokedexNumber;

        // ------------- Attributes -------------
        [BoxGroup("Attributes")]
        [SerializeField, Tooltip("Primary and optional secondary type of the Pokémon.")]
        private PokemonType types;

        [BoxGroup("Attributes")]
        [SerializeField, Tooltip("Male/female ratio information.")]
        private PokemonGenderRatio genderRatio;

        [BoxGroup("Attributes")]
        [SerializeField, Required, Tooltip("Base stats used to calculate the Pokémon's final stats.")]
        private PokemonStats baseStats;

        // ------------- Natures -------------
        [BoxGroup("Natures")]
        [SerializeField, Required, Tooltip("All possible natures this Pokémon can have.")]
        private NatureDatabase possibleNatures;

        // ------------- Moves -------------
        [BoxGroup("Moves")]
        [SerializeField, Tooltip("All moves this Pokémon can learn when leveling up.")]
        private LevelUpMove[] levelUpMoves;

        // ------------- Audio -------------
        [BoxGroup("Audio")]
        [SerializeField, Required, Tooltip("The AudioClip that plays when this Pokémon cries.")]
        private AudioClip cryClip;

        // ------------- Visuals -------------
        [BoxGroup("Visuals")]
        [SerializeField, Tooltip("Front/back/battle/menu sprites for this Pokémon.")]
        private PokemonSprites sprites;

        // ------------- Public Properties -------------
        public string DisplayName => displayName;
        public int PokedexNumber => pokedexNumber;
        public PokemonType Types => types;
        public PokemonGenderRatio GenderRatio => genderRatio;
        public PokemonStats BaseStats => baseStats;
        public NatureDatabase PossibleNatures => possibleNatures;
        internal LevelUpMove[] LevelUpMoves => levelUpMoves;
        public AudioClip CryClip => cryClip;
        public PokemonSprites Sprites => sprites;
    }
}
