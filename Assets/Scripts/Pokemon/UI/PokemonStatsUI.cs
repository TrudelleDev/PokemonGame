using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemon.UI
{
    /// <summary>
    /// Displays a Pokémon's base stats including HP, Attack, Defense, Special stats, and Speed.
    /// </summary>
    public class PokemonStatsUI : MonoBehaviour, IBindable<PokemonInstance>, IUnbind
    {
        [SerializeField, Required]
        private HealthBar healthBar;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's current and max HP.")]
        private TextMeshProUGUI healthText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's Attack stat.")]
        private TextMeshProUGUI attackText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's Defense stat.")]
        private TextMeshProUGUI defenseText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's Special Attack stat.")]
        private TextMeshProUGUI specialAttackText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's Special Defense stat.")]
        private TextMeshProUGUI specialDefenseText;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's Speed stat.")]
        private TextMeshProUGUI speedText;

        /// <summary>
        /// Binds the Pokémon's core stats to the corresponding UI elements.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to bind.</param>
        public void Bind(PokemonInstance pokemon)
        {
            healthBar.Bind(pokemon);

            healthText.text = $"{pokemon.Health.CurrentHealth}/{pokemon.Health.MaxHealth}";
            attackText.text = pokemon.Stats.Core.Attack.ToString();
            defenseText.text = pokemon.Stats.Core.Defense.ToString();
            specialAttackText.text = pokemon.Stats.Core.SpecialAttack.ToString();
            specialDefenseText.text = pokemon.Stats.Core.SpecialDefense.ToString();
            speedText.text = pokemon.Stats.Core.Speed.ToString();
        }

        /// <summary>
        /// Clears all stat-related UI elements.
        /// </summary>
        public void Unbind()
        {
            healthBar.Unbind();

            healthText.text = string.Empty;
            attackText.text = string.Empty;
            defenseText.text = string.Empty;
            specialAttackText.text = string.Empty;
            specialDefenseText.text = string.Empty;
            speedText.text = string.Empty;
        }
    }
}
