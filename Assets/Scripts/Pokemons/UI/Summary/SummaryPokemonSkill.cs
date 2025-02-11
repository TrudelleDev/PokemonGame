using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Summary
{
    public class SummaryPokemonSkill : MonoBehaviour, IPokemonBind
    {
        [Header("Stats")]
        [SerializeField] private TextMeshProUGUI health;       
        [SerializeField] private TextMeshProUGUI attack;
        [SerializeField] private TextMeshProUGUI defense;
        [SerializeField] private TextMeshProUGUI specialAttack;
        [SerializeField] private TextMeshProUGUI specialDefense;
        [SerializeField] private TextMeshProUGUI speed;
        [Header("Ability")]
        [SerializeField] private TextMeshProUGUI abilityName;
        [SerializeField] private TextMeshProUGUI abilityEffect;

        public void Bind(Pokemon pokemon)
        {
            health.text = $"{pokemon.HealthRemaining}/{pokemon.CoreStat.HealthPoint}";
            attack.text = $"{pokemon.CoreStat.Attack}";
            defense.text = $"{pokemon.CoreStat.Defense}";
            specialAttack.text = $"{pokemon.CoreStat.SpecialAttack}";
            specialDefense.text = $"{pokemon.CoreStat.SpecialDefense}";
            speed.text = $"{pokemon.CoreStat.Speed}";
            abilityName.text = $"{pokemon.Ability.Data.Name}";
            abilityEffect.text = $"{pokemon.Ability.Data.Effect}";
        }
    }
}
