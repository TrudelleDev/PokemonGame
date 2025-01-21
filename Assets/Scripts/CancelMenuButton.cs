using PokemonGame.Items.Datas;
using UnityEngine;

namespace PokemonGame
{
    public class CancelMenuButton : MonoBehaviour
    {
        [SerializeField] private ItemData data;

        public ItemData Data => data;
    }
}
