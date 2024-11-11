using UnityEngine;

namespace PokemonGame
{
    [CreateAssetMenu(fileName = "NewAbilityData", menuName = "ScriptableObjects/Ability Data")]
    public class AbilityData : ScriptableObject
    {
        [field: SerializeField] public string AbilityName { get; private set; }

        [field: SerializeField, TextArea(5, 10), Space] public string Description { get; private set; }
    }
}