using PokemonGame.Shared.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.Moves.UI
{
    /// <summary>
    /// Manages and binds a Pokémon's moves to the summary screen UI slots.
    /// Handles populating move data and enabling/disabling interaction.
    /// </summary>
    public class MoveSlotManager : MonoBehaviour, IBindable<Pokemon>, IUnbind
    {
        /// <summary>
        /// Binds all of a Pokémon's moves to the corresponding summary UI slots.
        /// </summary>
        /// <param name="pokemon">The Pokémon whose moves will be displayed.</param>
        public void Bind(Pokemon pokemon)
        {
            Unbind();

            if (pokemon?.Moves == null)
            {
                return;
            }

            BindMoves(pokemon.Moves);
        }

        /// <summary>
        /// Clears all move slots and disables interactivity.
        /// </summary>
        public void Unbind()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                MoveSlotUI move = child.GetComponent<MoveSlotUI>();
                Button button = child.GetComponent<Button>();

                if (move != null && button != null)
                {
                    move.Unbind();
                    button.interactable = false;
                }
            }
        }

        /// <summary>
        /// Populates each move slot with data from the given array of moves.
        /// Enables interactivity for each populated slot.
        /// </summary>
        /// <param name="moves">The array of moves to bind to the UI.</param>
        private void BindMoves(Move[] moves)
        {
            for (int i = 0; i < moves.Length && i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                MoveSlotUI move = child.GetComponent<MoveSlotUI>();
                Button button = child.GetComponent<Button>();

                if (move != null && button != null)
                {
                    move.Bind(moves[i]);
                    button.interactable = true;
                }
            }
        }
    }
}
