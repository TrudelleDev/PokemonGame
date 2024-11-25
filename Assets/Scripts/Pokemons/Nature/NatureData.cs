using UnityEngine;

namespace PokemonGame.Pokemons.Nature
{
    [CreateAssetMenu(fileName = "NewNatureData", menuName = "ScriptableObjects/Nature Data")]
    public class NatureData : ScriptableObject
    {
        [SerializeField] private string natureName;

        public string NatureName => natureName;
    }
}

