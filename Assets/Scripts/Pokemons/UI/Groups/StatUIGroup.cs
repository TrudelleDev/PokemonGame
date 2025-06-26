using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Groups
{
    /// <summary>
    /// Groups the UI elements used to display a Pokémon's base stats.
    /// </summary>
    [Serializable]
    public class StatUIGroup
    {
        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's Health Points (HP).")]
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

        public TextMeshProUGUI HealthText => healthText;
        public TextMeshProUGUI AttackText => attackText;
        public TextMeshProUGUI DefenseText => defenseText;
        public TextMeshProUGUI SpecialAttackText => specialAttackText;
        public TextMeshProUGUI SpecialDefenseText => specialDefenseText;
        public TextMeshProUGUI SpeedText => speedText;
    }
}
