using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Pokemons.Moves.UI.Summary
{
    public class SummaryMoveManager : MonoBehaviour, IPokemonBind
    {
        public void Bind(Pokemon pokemon)
        {
            for (int i = 0; i < pokemon.Moves.Length; i++)
            {
                if (transform.GetChild(i).GetComponent<MenuButton>() != null)
                    transform.GetChild(i).GetComponent<MenuButton>().Interactable = true;

                transform.GetChild(i).GetComponent<IMoveBind>()?.Bind(pokemon.Moves[i]);
            }
        }
    }
}
