using UnityEngine;

namespace PokemonGame.Pokemons.Moves.UI.Summary
{
    public class SummaryMoveManager : MonoBehaviour
    {
        public void Bind(Pokemon pokemon)
        {
            ClearSummaryMoves();
            BindSummaryMoves(pokemon.Moves);
        }

        private void BindSummaryMoves(Move[] moves)
        {
            for (int i = 0; i < moves.Length; i++)
            {
                if (transform.GetChild(i).GetComponent<SummaryMove>() != null)
                {
                    transform.GetChild(i).GetComponent<SummaryMove>().Bind(moves[i]);

                    // The controller will not ignore this menu button
                    transform.GetChild(i).GetComponent<MenuButton>().Interactable = true;             
                }
            }
        }

        private void ClearSummaryMoves()
        {
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                if (transform.GetChild(i).GetComponent<SummaryMove>() != null)
                {
                    transform.GetChild(i).GetComponent<SummaryMove>().Clear();

                    // The controller will ignore this menu button
                    transform.GetChild(i).GetComponent<MenuButton>().Interactable = false;
                }
            }
        }
    }
}
