using PokemonGame.Pokemons.Abilities;
using PokemonGame.Pokemons.Data;
using PokemonGame.Shared;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Summary
{
    /// <summary>
    /// Displays detailed Pokémon information in the summary screen, including species data, types, ID, and trainer details.
    /// Supports dynamic data binding and uses "MissingNo" as a fallback when data is unavailable or invalid.
    /// </summary>
    public class SummarySkillPanel : MonoBehaviour, IPokemonBind
    {
        [Title("Stats")]
        [SerializeField, Required] private TextMeshProUGUI healthText;
        [SerializeField, Required] private TextMeshProUGUI attackText;
        [SerializeField, Required] private TextMeshProUGUI defenseText;
        [SerializeField, Required] private TextMeshProUGUI specialAttackText;
        [SerializeField, Required] private TextMeshProUGUI specialDefenseText;
        [SerializeField, Required] private TextMeshProUGUI speedText;

        [Title("Ability")]
        [SerializeField, Required] private TextMeshProUGUI abilityNameText;
        [SerializeField, Required] private TextMeshProUGUI abilityEffectText;

        /// <summary>
        /// Binds the given Pokémon data to the UI elements.
        /// Falls back to "MissingNo" if data is missing or invalid.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to display.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Data == null || pokemon?.Ability.Data == null)
            {
                RebindToMissingNo();
                return;
            }

            SetStats(pokemon);
            SetAbility(pokemon);
        }

        private void RebindToMissingNo()
        {
            Pokemon missingNo = PokemonFactory.CreateMissingNo();
            SetStats(missingNo);
            SetAbility(missingNo);
        }

        private void SetAbility(Pokemon pokemon)
        {
            AbilityData ability = pokemon.Ability.Data;

            UIHelper.SetText(abilityNameText, ability.AbilityName);
            UIHelper.SetText(abilityEffectText, ability.Effect);
        }

        private void SetStats(Pokemon pokemon)
        {
            PokemonStats stats = pokemon.CoreStat;

            UIHelper.SetText(healthText, $"{pokemon.HealthRemaining}/{stats.HealthPoint}");
            UIHelper.SetText(attackText, stats.Attack);
            UIHelper.SetText(defenseText, stats.Defense);
            UIHelper.SetText(specialAttackText, stats.SpecialAttack);
            UIHelper.SetText(specialDefenseText, stats.SpecialDefense);
            UIHelper.SetText(speedText, stats.Speed);
        }
    }
}
