using UnityEngine;

namespace PokemonGame.Pokemons.Ability
{
    [CreateAssetMenu(fileName = "NewAbilityData", menuName = "ScriptableObjects/Ability Data")]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private string abilityName;
        [Space]
        [SerializeField, TextArea(5, 10)] private string description;

        public string AbilityName => abilityName;
        public string Description => description;
    }
}