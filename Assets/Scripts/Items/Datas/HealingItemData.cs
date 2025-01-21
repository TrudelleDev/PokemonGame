using UnityEngine;

namespace PokemonGame.Items.Datas
{
    // This is the data container that is use for every healing item in the game.

    [CreateAssetMenu(fileName = "NewHealingItemData", menuName = "ScriptableObjects/Items/Healing Item Data")]
    public class HealingItemData : ItemData
    {
        [SerializeField] private int healingAmount;
    }
}
