using UnityEngine;

namespace PokemonGame.Pokemons
{
    [CreateAssetMenu(fileName = "NewTypeData", menuName = "ScriptableObjects/Type Data")]
    public class TypeData : ScriptableObject
    {
        [SerializeField] private Sprite sprite;

        public Sprite Sprite => sprite;
    }
}
