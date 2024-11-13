using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.UI
{
    public class PokedexItemUI : MonoBehaviour
    {
        [SerializeField] private Image ownIcon;
        [SerializeField] private Image firstType;
        [SerializeField] private Image secondType;
        [Space]
        [SerializeField] private TextMeshProUGUI pokedexNumber;
        [SerializeField] private TextMeshProUGUI pokemonName;

        public void Initialize(int number)
        {
            pokedexNumber.text = $"{number:000}";
            pokemonName.text = "-----";
            ownIcon.enabled = false;
            firstType.enabled = false;
            secondType.enabled = false;
        }

        public void Bind(PokedexData data)
        {
            pokemonName.text = data.Data.PokemonName;
            ownIcon.enabled = data.IsOwn;
            firstType.sprite = data.Data.FirstType.Sprite;

            firstType.enabled = true;

            // If the Pokemon has 2 types
            if (data.Data.SecondType != null)
            {
                secondType.sprite = data.Data.SecondType.Sprite;
                secondType.enabled = true;
            }
        }
    }
}

