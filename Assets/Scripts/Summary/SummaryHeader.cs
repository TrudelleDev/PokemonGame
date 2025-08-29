using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Summary
{
    /// <summary>
    /// Manages the UI elements for the Pok�mon summary header.
    /// Displays the Pok�mon's name, level, gender, and sprite.
    /// Safely handles null or incomplete Pok�mon data by clearing the UI.
    /// </summary>
    public class SummaryHeader : MonoBehaviour, IBindable<Pokemon>, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Handles display of the Pok�mon's name, level, gender, and sprite.")]
        private PokemonHeaderUI pokemonHeaderUI;

        /// <summary>
        /// Binds the specified Pok�mon to the header UI elements.
        /// Clears the UI if the Pok�mon or its core data is null.
        /// </summary>
        /// <param name="pokemon">The Pok�mon instance to display.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Definition == null)
            {
                Unbind();
                return;
            }

            pokemonHeaderUI.Bind(pokemon);
        }

        /// <summary>
        /// Clears the UI elements, removing any previously displayed data.
        /// </summary>
        public void Unbind()
        {
            pokemonHeaderUI.Unbind();
        }
    }
}
