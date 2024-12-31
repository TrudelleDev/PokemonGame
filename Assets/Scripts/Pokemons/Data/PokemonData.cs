using PokemonGame.Encyclopedia;
using PokemonGame.Pokemons.Moves;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.Pokemons.Data
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "NewPokemonData", menuName = "ScriptableObjects/Pokemon Data")]
    public class PokemonData : ScriptableObject
    {
        [SerializeField, Range(1, Pokedex.TotalPokemon)] private int pokedexNumber;
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
        [SerializeField] private List<Learnset> learnsets;

        public int PokedexNumber => pokedexNumber;
        public string PokemonName => pokemonName;
        public PokemonType Types => types;
        public PokemonGenderRatio GenderRatio => genderRatio;
        public PokemonStats BaseStats => baseStats;
        public PokemonSprites Sprites => sprites;
        public List<Learnset> Learnset => learnsets;

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
