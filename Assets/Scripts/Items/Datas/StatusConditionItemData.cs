using PokemonGame.Pokemons;
using UnityEngine;

namespace PokemonGame.Items.Datas
{
    // This is the data container that is use for every status item in the game.

    [CreateAssetMenu(fileName = "NewStatusConditionItemData", menuName = "ScriptableObjects/Items/Status Condition Item Data")]
    public class StatusConditionItemData : ItemData
    {
        [SerializeField] private StatusCondition statusCondition;
    }
}
