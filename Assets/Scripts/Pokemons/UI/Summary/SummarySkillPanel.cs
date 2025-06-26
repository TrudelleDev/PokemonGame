using PokemonGame.Pokemons.Abilities;
using PokemonGame.Pokemons.Abilities.UI.Groups;
using PokemonGame.Pokemons.Data;
using PokemonGame.Pokemons.UI.Groups;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Summary
{
    /// <summary>
    /// Displays detailed Pokémon information in the summary screen, including species data, types, ID, and trainer details.
    /// Supports dynamic data binding and uses "MissingNo" as a fallback when data is unavailable or invalid.
    /// </summary>
    public class SummarySkillPanel : MonoBehaviour, IPokemonBind
    {
        [SerializeField] private StatUIGroup stats;
        [SerializeField] private AbilityUIGroup ability;


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
            AbilityData abilityData = pokemon.Ability.Data;

            ability.NameText.text = abilityData.AbilityName;
            ability.EffectText.text = abilityData.Effect;
        }

        private void SetStats(Pokemon pokemon)
        {
            PokemonStats coreStat = pokemon.CoreStat;

            stats.HealthText.text = $"{pokemon.HealthRemaining}/{coreStat.HealthPoint}";
            stats.AttackText.text = coreStat.Attack.ToString();
            stats.DefenseText.text = coreStat.Defense.ToString();
            stats.SpecialAttackText.text = coreStat.SpecialAttack.ToString();
            stats.SpecialDefenseText.text = coreStat.SpecialDefense.ToString();
            stats.SpeedText.text = coreStat.Speed.ToString();
        }
    }
}
