using System;
using System.Collections.Generic;
using PokemonGame.Characters.States;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PokemonGame.Tile
{
    /// <summary>
    /// Spawns grass rustle effects as the player moves through tall grass.
    /// Uses a single FX Tilemap for both entry and trailing rustles.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterStateController))]
    public class GrassRustleSpawner : Singleton<GrassRustleSpawner>
    {
        [Title("Settings")]

        [SerializeField, Required]
        [Tooltip("Animated tile used for the grass rustle effect.")]
        private OneShotAnimatedTile rustleTile;

        [SerializeField, MinValue(1)]
        [Tooltip("Maximum number of rustle tiles kept visible behind the player.")]
        private int maxTrailCount = 3;

        private Tilemap grassTilemap;
        private Tilemap fxTilemap;

        private CharacterStateController controller;
        private Coroutine currentRustle;

        private readonly Queue<Vector3Int> rustleTrailQueue = new();

        public event Action OnEnterGrass;

        protected override void Awake()
        {
            base.Awake();
            controller = GetComponent<CharacterStateController>();
        }

        private void OnEnable()
        {
            controller.TileMover.OnMoveStart += OnMoveStart;
            controller.TileMover.OnMoveComplete += OnMoveComplete;
        }

        private void OnDisable()
        {
            controller.TileMover.OnMoveStart -= OnMoveStart;
            controller.TileMover.OnMoveComplete -= OnMoveComplete;
        }

        /// <summary>
        /// Assigns the Tilemaps used for high grass detection and FX rustle animations.
        /// </summary>
        /// <param name="grassTilemap">Tilemap containing high grass tiles that trigger rustles.</param>
        /// <param name="fxTilemap">Tilemap used to display temporary rustle FX tiles.</param>
        public void SetTilemaps(Tilemap grassTilemap, Tilemap fxTilemap)
        {
            this.grassTilemap = grassTilemap;
            this.fxTilemap = fxTilemap;
        }

        /// <summary>
        /// Called when movement starts.
        /// Moves the current rustle under the player into the trailing queue
        /// and begins a one-shot rustle animation.
        /// </summary>
        private void OnMoveStart()
        {
            if (grassTilemap == null || fxTilemap == null)
            {
                return;
            }

            Vector3Int currentCell = grassTilemap.WorldToCell(transform.position);

            if (!grassTilemap.HasTile(currentCell))
            {
                return;
            }

            // Move current rustle under player into trail
            if (fxTilemap.HasTile(currentCell))
            {
                rustleTrailQueue.Enqueue(currentCell);

                if (rustleTrailQueue.Count > maxTrailCount)
                {
                    Vector3Int oldestTrailCell = rustleTrailQueue.Dequeue();

                    fxTilemap.SetTile(oldestTrailCell, null);
                    fxTilemap.RefreshTile(oldestTrailCell);
                }
            }

            StartCoroutine(rustleTile.PlayOnce(fxTilemap, currentCell));
        }

        /// <summary>
        /// Called when movement completes.
        /// Triggers the OnEnterGrass event if the player steps onto a grass tile
        /// and plays the corresponding rustle animation.
        /// </summary>
        private void OnMoveComplete()
        {
            if (grassTilemap == null || fxTilemap == null)
            {
                return;
            }

            Vector3Int currentCell = grassTilemap.WorldToCell(transform.position);
            bool isGrassTile = grassTilemap.HasTile(currentCell);

            if (isGrassTile)
            {
                OnEnterGrass?.Invoke();

                if (currentRustle != null)
                {
                    StopCoroutine(currentRustle);
                }

                fxTilemap.ClearAllTiles();
                currentRustle = StartCoroutine(rustleTile.PlayOnce(fxTilemap, currentCell));
            }
        }
    }
}
