using PokemonGame.Items.UI;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Items.Storage.UI
{
    public class BagPocketContent : MonoBehaviour
    {
        [SerializeField] private ItemUI bagItemPrefab;
        [SerializeField] private MenuButton cancelButton;

        private void Awake()
        {
            cancelButton.OnClick += () => ViewManager.Instance.ShowLast();
        }

        public void CreateItemList(BagPocket bagPocket)
        {
            foreach (Item item in bagPocket.Items)
            {
                ItemUI bagItemInstance = Instantiate(bagItemPrefab);

                bagItemInstance.Bind(item);
                bagItemInstance.transform.SetParent(transform, false);
            }

            // Make sure the cancel button is the last item in the list.
            cancelButton.transform.SetAsLastSibling();
        }
    }
}
