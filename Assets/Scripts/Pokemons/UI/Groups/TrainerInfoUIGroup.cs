using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemons.UI.Groups
{
    /// <summary>
    /// Groups the UI elements related to a Pokémon's trainer information,
    /// including the trainer's name and a custom trainer memo.
    /// </summary>
    [Serializable]
    public class TrainerInfoUIGroup
    {
        [SerializeField, Required]
        [Tooltip("Displays the trainer's name.")]
        private TextMeshProUGUI trainerNameText;

        [SerializeField, Required]
        [Tooltip("Displays the trainer's memo text, such as encounter info.")]
        private TextMeshProUGUI trainerMemoText;

        public TextMeshProUGUI TrainerNameText => trainerNameText;
        public TextMeshProUGUI TrainerMemoText => trainerMemoText;
    }
}
