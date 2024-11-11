using UnityEngine;

namespace PokemonGame
{
    [CreateAssetMenu(fileName = "NewNatureData", menuName = "ScriptableObjects/Nature Data")]
    public class NatureData : ScriptableObject
    {
        [field: SerializeField] public string NatureName { get; private set; }

        [field: SerializeField, Space] public PokemonStat IncreaseStat { get; private set; }

        [field: SerializeField] public PokemonStat DecreaseStat { get; private set;}
    }
}

