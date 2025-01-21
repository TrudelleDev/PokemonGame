using TMPro;
using UnityEngine;

namespace PokemonGame.Items.UI
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemCount;

        public Item ItemReference { get; private set; }

        public void Bind(Item item)
        {
            ItemReference = item;

            itemName.text = item.Data.name;
            itemCount.text = $"{item.Count}";
        }
    }
}
