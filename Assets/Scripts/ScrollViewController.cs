using PokemonClone.Config;
using PokemonGame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.ScrollView
{
    // TODO: Modify this code to work with any time of menu.
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
            if (Input.GetKeyDown(Configuration.DownKey) && currentbuttonIndex < transform.childCount - 1)
            {
                currentbuttonIndex++;
                rectTransform.anchoredPosition += (height + spacing) * Vector2.up;         
            }
            if (Input.GetKeyDown(Configuration.UpKey) && currentbuttonIndex > 0)
            {
                currentbuttonIndex--;
                rectTransform.anchoredPosition += (height + spacing) * Vector2.down;
            }
        }
    }
}
