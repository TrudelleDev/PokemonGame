using PokemonGame.Pokemons;
using PokemonGame.Pokemons.UI;
using PokemonGame.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Systems.UI
{
    /// <summary>
    /// Controls the Pok�mon identity panel UI within the summary screen.
    /// Displays the Pok�mon's name, gender, menu sprite, and type icons.
    /// Supports dynamic data binding and clears the UI when data is missing or invalid.
    /// </summary>
    public class SummaryIdentityPanel : MonoBehaviour, IPokemonBind, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("UI group displaying the Pok�mon's identity info: name, gender icon, sprite, and types.")]
        private PokemonIdentityUI pokemonIdentityUI;

        /// <summary>
        /// Binds the specified Pok�mon to the identity UI elements.
        /// Clears the UI if the Pok�mon or its core data is null.
        /// </summary>
        /// <param name="pokemon">The Pok�mon instance to display.</param>
        public void Bind(Pokemon pokemon)
        {
            if (pokemon?.Data == null)
            {
                Unbind();
                return;
            }

            pokemonIdentityUI.Bind(pokemon);
        }

        /// <summary>
        /// Clears all identity UI elements.
        /// </summary>
        public void Unbind()
        {
            pokemonIdentityUI.Unbind();
        }
    }
}
