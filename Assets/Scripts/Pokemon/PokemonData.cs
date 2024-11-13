using PokemonGame;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonClone
{
    [CreateAssetMenu(fileName = "NewPokemonData", menuName = "ScriptableObjects/Pokemon Data")]
    public class PokemonData : ScriptableObject
    {
        [field: SerializeField] public int PokedexNumber { get; private set; }

        [field: SerializeField] public string PokemonName { get; private set; }

        [field: Header("Types")]
        [field: SerializeField] public TypeData FirstType { get; private set; }

        [field: SerializeField] public TypeData SecondType { get; private set; }

        [field: SerializeField, Space] public PokemonBaseStats BaseStats { get; private set; }


        [field: SerializeField, Space] public List<Learnset> Learnset { get; private set; }
       
    }
}
