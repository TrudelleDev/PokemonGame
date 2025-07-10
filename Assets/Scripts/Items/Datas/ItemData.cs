using UnityEngine;

namespace PokemonGame.Items.Datas
{
    // This is the base data container that is use for every item in the game.
    // This can also be use for every other item that does not require additional data.

    [CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/Items/Item Data", order = -1)]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string Id;
        [SerializeField] private new string name;
        [SerializeField] private Sprite icon;

        [TextArea(5, 10)]
        [SerializeField] private string description;
        [SerializeField] private ItemType type;

        public string ID => Id;
        public string Name => name;
        public string Description => description;
        public Sprite Icon => icon;
        public ItemType Type => type;
    }
}
