using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.Abilities.UI.Groups
{
    /// <summary>
    /// Groups the UI elements used to display a Pokémon's ability name and effect description.
    /// </summary>
    [Serializable]
    public class AbilityUIGroup
    {
        [SerializeField, Required]
        [Tooltip("Displays the ability's name.")]
        private TextMeshProUGUI nameText;

        [SerializeField, Required]
        [Tooltip("Displays the ability's effect description.")]
        private TextMeshProUGUI effectText;

        public TextMeshProUGUI NameText => nameText;
        public TextMeshProUGUI EffectText => effectText;
    }
}
