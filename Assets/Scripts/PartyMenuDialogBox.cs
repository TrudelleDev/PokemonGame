using TMPro;
using UnityEngine;

namespace PokemonGame
{
    public class PartyMenuDialogBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI description;

        public void SetText(string text)
        {
            description.text = text;          
        }

        public void SetSize(float width, float height)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        }
    }
}
