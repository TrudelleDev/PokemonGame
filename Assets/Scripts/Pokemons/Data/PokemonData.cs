using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "NewPokemonData", menuName = "ScriptableObjects/Pokemon Data")]
    public class PokemonData : ScriptableObject
    {
        [SerializeField, Range(0, 200)] private int pokedexNumber;
        [SerializeField] private string pokemonName;
        [Space]
        [SerializeField] private PokemonType types;
        [Space]
        [SerializeField] private PokemonGenderRatio genderRatio;
        [Space]
        [SerializeField] private PokemonStats baseStats;
        [Space]
        [SerializeField] private PokemonSprites sprites;
        [Space]
        [SerializeField] private AnimatorOverrideController menuSpriteOverrider;

        public int PokedexNumber => pokedexNumber;
        public string PokemonName => pokemonName;
        public PokemonType Types => types;
        public PokemonGenderRatio GenderRatio => genderRatio;
        public PokemonStats BaseStats => baseStats;
        public PokemonSprites Sprites => sprites;
        public AnimatorOverrideController MenuSpriteOverrider => menuSpriteOverrider;

        private void OnValidate()
        {
            // Creating a new gender ratio to recalculate the female ratio
            genderRatio = new PokemonGenderRatio(genderRatio.MaleRatio);

            // Creating a new pokemon stat to recalculate the total
            baseStats = new PokemonStats(
                baseStats.HealthPoint,
                baseStats.Attack,
                baseStats.Defense,
                baseStats.SpecialAttack,
                baseStats.SpecialDefense,
                baseStats.Speed);
        }
    }
}
