using PokemonGame.Pokemons.Interfaces;
using PokemonGame.Shared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Pokemons.UI
{
    /// <summary>
    /// Displays the Pokémon's primary and secondary type icons.
    /// </summary>
    public class PokemonTypeUI : MonoBehaviour, IPokemonBindable, IUnbind
    {
        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's primary type icon.")]
        private PokemonTypeIcon primaryTypeSprite;

        [SerializeField, Required]
        [Tooltip("Displays the Pokémon's secondary type icon, if any.")]
        private PokemonTypeIcon secondaryTypeSprite;

        /// <summary>
        /// Binds the Pokémon's type data to the type icons.
        /// </summary>
        /// <param name="pokemon">The Pokémon instance to bind.</param>
        public void Bind(Pokemon pokemon)
        {
            primaryTypeSprite.Bind(pokemon);

            if (pokemon.Data.Types.HasSecondType)
                secondaryTypeSprite.Bind(pokemon);
            else
                secondaryTypeSprite.Unbind();
        }

        /// <summary>
        /// Clears both primary and secondary type icons.
        /// </summary>
        public void Unbind()
        {
            primaryTypeSprite.Unbind();
            secondaryTypeSprite.Unbind();
        }
    }
}
