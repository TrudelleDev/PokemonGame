using PokemonGame.Dialogue;
using PokemonGame.Tile;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle
{
    public class BattleTrigger : MonoBehaviour
    {
        private void Start()
        {
            GrassRustleSpawner.Instance.OnEnterGrass += OnGrassRustleSpawnerEnterGrass;
        }

        private void OnGrassRustleSpawnerEnterGrass()
        {
            ViewManager.Instance.Show<BattleView>();
           
        }
    }
}
