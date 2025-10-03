using System.Collections.Generic;
using PokemonGame.Menu;
using PokemonGame.Pokemons;
using UnityEngine;

namespace PokemonGame.Moves.UI
{
    /// <summary>
    /// Manages and binds a Pok�mon's moves to the summary screen UI slots.
    /// Handles populating move data and enabling/disabling interaction.
    /// </summary>
    public class MoveSlotUIManager : MonoBehaviour
    {
        /// <summary>
        /// Binds all of a Pok�mon's moves to the corresponding summary UI slots.
        /// </summary>
        /// <param name="pokemon">The Pok�mon whose moves will be displayed.</param>
        public void Bind(Pokemon pokemon)
        {
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
                MenuButton button = child.GetComponent<MenuButton>();

                if (moveSlot != null && button != null)
                {
                    moveSlot.Unbind();
                    button.SetInteractable(false);
                }
            }
        }

        private void BindMoves(IReadOnlyList<Move> moves)
        {
            for (int i = 0; i < moves.Count && i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                MoveSlotUI moveSlot = child.GetComponent<MoveSlotUI>();
                MenuButton button = child.GetComponent<MenuButton>();

                if (moveSlot != null && button != null)
                {
                    moveSlot.Bind(moves[i]);
                    button.SetInteractable(true);
                }
            }
        }
    }
}
