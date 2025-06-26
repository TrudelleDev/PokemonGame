using UnityEngine;

namespace PokemonGame.Pokemons.Moves.UI.Summary
{
    /// <summary>
    /// UI component that binds a Pokémon's moves to the summary screen slots.
    /// Handles enabling interaction and populating move data.
    /// </summary>
    public class SummaryMoveListUI : MonoBehaviour
    {
        /// <summary>
        /// Binds all of a Pokémon's moves to the corresponding summary UI slots.
        /// </summary>
        /// <param name="pokemon">The Pokémon whose moves will be displayed.</param>
        public void Bind(Pokemon pokemon)
        {
            UnbindAll();
            BindMoves(pokemon.Moves);
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
                SummaryMoveSlotUI move = child.GetComponent<SummaryMoveSlotUI>();
                MenuButton button = child.GetComponent<MenuButton>();

                if (move != null && button != null)
                {
                    move.Bind(moves[i]);
                    button.Interactable = true;
                }
            }
        }

        /// <summary>
        /// Clears all move slots and disables interactivity for each one.
        /// </summary>
        private void UnbindAll()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                SummaryMoveSlotUI move = child.GetComponent<SummaryMoveSlotUI>();
                MenuButton button = child.GetComponent<MenuButton>();

                if (move != null && button != null)
                {
                    move.Unbind();
                    button.Interactable = false;
                }
            }
        }
    }
}
