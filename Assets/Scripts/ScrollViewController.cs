using PokemonGame.Encyclopedia.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame
{
    public class ScrollViewController : MonoBehaviour
    {
        private int currentbuttonIndex;

        private RectTransform rectTransform;
        private VerticalLayoutGroup verticalLayoutGroup;
        private PokedexItemUI contentItem;

        private float spacing;
        private float height;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
            contentItem = transform.GetChild(0).GetComponent<PokedexItemUI>();

            spacing = verticalLayoutGroup.spacing;
            height = contentItem.GetComponent<RectTransform>().rect.height;
        }

        private void Update()
        {
            if (Input.GetKeyDown(Keybind.DownKey) && currentbuttonIndex < transform.childCount - 1)
            {
                currentbuttonIndex++;
                rectTransform.anchoredPosition += (height + spacing) * Vector2.up;
            }
            if (Input.GetKeyDown(Keybind.UpKey) && currentbuttonIndex > 0)
            {
                currentbuttonIndex--;
                rectTransform.anchoredPosition += (height + spacing) * Vector2.down;
            }
        }
    }
}
