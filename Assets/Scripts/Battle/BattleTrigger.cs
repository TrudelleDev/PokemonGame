using PokemonGame.Audio;
using PokemonGame.Party;
using PokemonGame.Tile;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle
{
    /// <summary>
    /// Handles wild battle triggering when the player enters tall grass.
    /// Plays the battle music and opens the BattleView with the player's selected Pokémon.
    /// </summary>
    [DisallowMultipleComponent]
    public class BattleTrigger : MonoBehaviour
    {
        [Title("References")]
        [SerializeField, Required]
        [Tooltip("Background music clip that plays when a battle begins.")]
        private AudioClip battleBgm;

        [SerializeField, Required]
        [Tooltip("Reference to the PartyManager that provides the player's current Pokémon.")]
        private PartyManager partyManager;

        private void OnEnable()
        {
            GrassRustleSpawner.Instance.OnEnterGrass += OnEnterGrass;
        }

        private void OnDisable()
        {
            GrassRustleSpawner.Instance.OnEnterGrass -= OnEnterGrass;
        }

        /// <summary>
        /// Called when the player steps into tall grass, triggering a wild battle.
        /// </summary>
        private void OnEnterGrass()
        {
            AudioManager.Instance.PlayBGM(battleBgm);
            BattleView battle = ViewManager.Instance.Show<BattleView>();

            // TODO: Replace with random wild Pokémon selection once encounter system is implemented.
            battle.SetupBattle(partyManager.SelectedPokemon, partyManager.SelectedPokemon);
        }
    }
}
