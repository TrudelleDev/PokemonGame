using System.Collections.Generic;
using PokemonGame.Audio;
using PokemonGame.Inventory;
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
    public class WildEncounterManager : MonoBehaviour
    {
        [Title("References")]
        [SerializeField, Required]
        [Tooltip("Background music clip that plays when a battle begins.")]
        private AudioClip battleBgm;

        [SerializeField, Required]
        [Tooltip("Reference to the PartyManager that provides the player's current Pokémon.")]
        private PartyManager partyManager;

        [SerializeField, Required]
        private InventoryManager inventory;

        [Title("Encounter Settings")]
        [SerializeField, Range(0, 100)] private int encounterChance = 10; // 10% chance per step

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
            // Roll the dice: if the random number is higher than our chance, don't battle
            if (Random.Range(0, 100) >= encounterChance)
                return;

            TriggerBattle();
        }

        private void TriggerBattle()
        {
            AudioManager.Instance.PlayBGM(battleBgm);
            AudioManager.Instance.SetBGMStartTime(1f);

            BattleView battle = ViewManager.Instance.Show<BattleView>();

            WildPokemonEntry wildPokemon = ChooseWildPokemon();

            int level = Random.Range(wildPokemon.MinLevel, wildPokemon.MaxLevel + 1);

            PokemonInstance pokemon = PokemonFactory.CreatePokemon(level, wildPokemon.Pokemon);

            battle.Initialize(partyManager, inventory, pokemon);
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
