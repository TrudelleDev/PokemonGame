using UnityEditor;
using UnityEngine;

namespace PokemonGame.Pokemons.Abilities
{
    [CreateAssetMenu(fileName = "NewAbilityData", menuName = "ScriptableObjects/Ability Data")]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private string abilityName;
        [SerializeField, TextArea(5, 10)] private string effect;

        public string AbilityName => abilityName;
        public string Effect => effect;
    }
}