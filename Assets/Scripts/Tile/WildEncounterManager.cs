using System.Linq;
using MonsterTamer.Battle;
using MonsterTamer.Characters;
using MonsterTamer.Characters.Player;
using MonsterTamer.Pokemon;
using MonsterTamer.Pokemon.Models;
using MonsterTamer.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MonsterTamer.Tile
{
    /// <summary>
    /// Handles wild Monster encounters triggered by player movement on specific tiles (e.g. grass).
    /// Listens to completed tile movements and rolls for encounters based on configured chance.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class WildEncounterManager : MonoBehaviour
    {
        [SerializeField, Required]
        [Tooltip("Tilemap representing encounter tiles (e.g. tall grass).")]
        private Tilemap encounterTilemap;

        [SerializeField, Range(0, 100)]
        [Tooltip("Chance (percentage) to trigger a wild encounter per completed step.")]
        private int encounterChance = 10;

        [SerializeField, Required]
        [Tooltip("List of possible wild Monsters and their encounter weights.")]
        private WildMonsterDatabase monsterDatabase;

        private Character player;
        private CharacterStateController playerStateController;
        private bool encounterLocked;

        private void Awake()
        {
            player = PlayerRegistry.Player;
            playerStateController = player.GetComponent<CharacterStateController>();
        }

        /// <summary>
        /// Subscribes to the player's movement completion event.
        /// </summary>
        private void OnEnable()
        {
            if (player == null)
            {
                return;
            }

            playerStateController.TileMover.MoveCompleted += HandleMoveCompleted;
        }

        /// <summary>
        /// Unsubscribes from movement events to prevent duplicate callbacks.
        /// </summary>
        private void OnDisable()
        {
            if (player == null)
            {
                return;
            }

            playerStateController.TileMover.MoveCompleted -= HandleMoveCompleted;
        }

        /// <summary>
        /// Called after each completed tile movement.
        /// Checks tile validity, rolls encounter chance, and triggers battle if successful.
        /// </summary>
        private void HandleMoveCompleted()
        {
            if (encounterLocked) return;
            if (!IsPlayerOnEncounterTile()) return;
            if (!RollEncounter()) return;

            TriggerBattle();
        }

        private bool IsPlayerOnEncounterTile()
        {
            Vector3Int cell = encounterTilemap.WorldToCell(player.transform.position);
            return encounterTilemap.HasTile(cell);
        }

        private bool RollEncounter()
        {
            return Random.Range(0, 100) < encounterChance;
        }

        private void UnlockEncounter()
        {
            encounterLocked = false;
        }

        /// <summary>
        /// Initializes a wild battle using a randomly selected Monster entry.
        /// Locks encounters until the battle ends.
        /// </summary>
        private void TriggerBattle()
        {
            encounterLocked = true;

            WildPokemonEntry entry = ChooseWildMonster();
            int level = Random.Range(entry.MinLevel, entry.MaxLevel + 1);
            PokemonInstance monster = PokemonFactory.CreatePokemon(level, entry.Pokemon);

            BattleView battle = ViewManager.Instance.Show<BattleView>();
            battle.InitializeWildBattle(player, monster);
            battle.OnBattleViewClose += UnlockEncounter;
        }

        /// <summary>
        /// Selects a wild Monster based on weighted encounter rates.
        /// </summary>
        private WildPokemonEntry ChooseWildMonster()
        {
            int totalWeight = monsterDatabase.Entries.Sum(e => e.EncounterRate);
            int roll = Random.Range(0, totalWeight);
            int cumulative = 0;

            return monsterDatabase.Entries.First(e => (cumulative += e.EncounterRate) > roll);
        }
    }
}
