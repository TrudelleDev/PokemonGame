using PokemonClone;
using UnityEngine;

namespace PokemonGame
{
    [CreateAssetMenu(fileName = "NewTypeData", menuName = "ScriptableObjects/Type Data")]
    public class TypeData : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }

    }
}
