using UnityEngine;

namespace PokemonGame.Pokemons.Abilities
{
    [CreateAssetMenu(fileName = "NewAbilityData", menuName = "ScriptableObjects/Ability Data")]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private new string name;
        [Space]
        [SerializeField, TextArea(5, 10)] private string effect;

        public string Name => name;
        public string Effect => effect;
    }
}