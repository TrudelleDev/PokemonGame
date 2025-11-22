using System;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PokemonGame.Pokemon.UI
{
    /// <summary>
    /// Displays a Pokémon's trainer-related memo, including nature and encounter location.
    /// </summary>
    public class TrainerMemoUI : MonoBehaviour, IBindable<PokemonInstance>, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Text field displaying the trainer's memo, such as encounter details.")]
        private TextMeshProUGUI memoText;

        /// <summary>
        /// Binds trainer-related data from the Pokémon instance to UI elements.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance providing trainer info.</param>
        public void Bind(PokemonInstance pokemon)
        {
            if (pokemon?.Nature?.Definition == null)
            {
                Unbind();
                return;
            }

            string natureName = pokemon.Nature.Definition.DisplayName ?? "Unknown";
            string location = string.IsNullOrWhiteSpace(pokemon.Meta.LocationEncounter) ? "an unknown location" : pokemon.Meta.LocationEncounter;

            memoText.text = $"{natureName} nature.{Environment.NewLine}Met at {location} at level {pokemon.Experience.Level}.";
        }

        /// <summary>
        /// Clears the trainer UI elements.
        /// </summary>
        public void Unbind()
        {
            memoText.text = string.Empty;
        }
    }
}
