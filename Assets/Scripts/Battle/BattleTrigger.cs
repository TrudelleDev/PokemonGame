using System.Collections.Generic;
using PokemonGame.Audio;
using PokemonGame.Party;
using PokemonGame.Pokemon;
using PokemonGame.Pokemon.Models;
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

        [SerializeField, Required]
        private List<WildPokemonEntry> pokemonEntries;

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
            AudioManager.Instance.SetBGMStartTime(1f);

            BattleView battle = ViewManager.Instance.Show<BattleView>();

            WildPokemonEntry wildPokemon = ChooseWildPokemon();

            int level = Random.Range(wildPokemon.MinLevel, wildPokemon.MaxLevel + 1);

            PokemonInstance pokemon = PokemonFactory.CreatePokemon(level, wildPokemon.Pokemon);

            battle.Initialize(partyManager.SelectedPokemon, pokemon);
        }

        private WildPokemonEntry ChooseWildPokemon()
        {
            int totalRate = 0;

            foreach (WildPokemonEntry entry in pokemonEntries)
            {
                totalRate += entry.EncounterRate;
            }

            int roll = Random.Range(0, totalRate);
            int cumulative = 0;

            foreach (WildPokemonEntry entry in pokemonEntries)
            {
                cumulative += entry.EncounterRate;

                if (roll < cumulative)
                {
                    return entry;
                }
            }

            return pokemonEntries[0];
        }
    }
}
