using UnityEngine;

namespace PokemonGame.Encyclopedia.UI
{
    public class PokedexContent : MonoBehaviour
    {
        [SerializeField] private Pokedex pokedex;
        [SerializeField] private PokedexItemUI pokedexItemUIPrefab;

        public void Initialize()
        {
            // Create a list of Pokedex Item UI at runetime instead of creating it manually in the Hierarchy.
            for (int i = 1; i <= Pokedex.TotalPokemon; i++)
            {
                PokedexItemUI itemUIInstance = Instantiate(pokedexItemUIPrefab);

                itemUIInstance.Initialize(i);
                itemUIInstance.transform.SetParent(transform, false);
            }

            pokedex.OnPokemonChange += OnPokedexPokemonChange;
        }


        private void OnPokedexPokemonChange(PokedexEntry data)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(data.Data.PokedexNumber - 1).GetComponent<PokedexItemUI>() != null)
                {
                    transform.GetChild(data.Data.PokedexNumber - 1).GetComponent<PokedexItemUI>().Bind(data);
                }
            }
        }
    }
}
