using PokemonGame.Items.Datas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Items.UI
{
    public class ItemDescription : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private Image itemIcon;

        public void Bind(ItemData data)
        {
            itemDescription.text = data.Description;
            itemIcon.sprite = data.Icon;
        }
    }
}
