using PokemonGame.Abilities.Definition;
using PokemonGame.Audio;
using PokemonGame.Moves.Definition;
using PokemonGame.Natures.Enums;
using PokemonGame.Party;
using PokemonGame.Pokemons;
using PokemonGame.Pokemons.Definition;
using PokemonGame.Pokemons.Enums;
using PokemonGame.Pokemons.Natures;
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

        private void Start()
        {
            GrassRustleSpawner.Instance.OnEnterGrass += OnEnterGrass;
        }

        /// <summary>
        /// Called when the player steps into tall grass, triggering a wild battle.
        /// </summary>
        private void OnEnterGrass()
        {
            AudioManager.Instance.PlayBGM(battleBgm);
            BattleView battle = ViewManager.Instance.Show<BattleView>();


            // TODO: Replace with random wild Pokémon selection once encounter system is implemented.
            Pokemon weedle = new Pokemon(
                5,
                PokemonDefinitionLoader.Get(PokemonId.Weedle),
                NatureDefinitionLoader.Get(NatureId.Adamant),
                AbilityDefinitionLoader.Get(Abilities.Enums.AbilityId.None),
                new[] { MoveDefinitionLoader.Get(Moves.Enums.MoveId.Tackle) }
                );

         
            battle.Initialize(partyManager.SelectedPokemon, weedle);
        }
    }
}
