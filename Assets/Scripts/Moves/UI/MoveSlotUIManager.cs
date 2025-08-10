using System.Collections.Generic;
using PokemonGame.Pokemons;
using PokemonGame.Shared.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Moves.UI
{
    /// <summary>
    /// Manages and binds a Pokémon's moves to the summary screen UI slots.
    /// Handles populating move data and enabling/disabling interaction.
    /// </summary>
    public class MoveSlotUIManager : MonoBehaviour, IBindable<Pokemon>, IUnbind
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
                MoveSlotUI moveSlot = child.GetComponent<MoveSlotUI>();
                Button button = child.GetComponent<Button>();

                if (moveSlot != null && button != null)
                {
                    moveSlot.Unbind();
                    button.interactable = false;
                }
            }
        }

        private void BindMoves(IReadOnlyList<Move> moves)
        {
            for (int i = 0; i < moves.Count && i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                MoveSlotUI moveSlot = child.GetComponent<MoveSlotUI>();
                Button button = child.GetComponent<Button>();

                if (moveSlot != null && button != null)
                {
                    moveSlot.Bind(moves[i]);
                    button.interactable = true;
                }
            }
        }
    }
}
