﻿using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PokemonGame.Tile
{
    /// <summary>
    /// Manages all grass-related Tilemaps in the scene.
    /// Responsible for registering the grass layers with the <see cref="GrassRustleSpawner"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public class GrassManager : MonoBehaviour
    {
        [Title("Grass Layers")]

        [SerializeField, Required]
        [Tooltip("Base environment Tilemap containing grass tiles.")]
        private Tilemap grassTilemap;

        [SerializeField, Required]
        [Tooltip("FX Tilemap rendered in front of the player for grass rustle effects.")]
        private Tilemap fxTilemap;

        /// <summary>
        /// Registers the grass Tilemaps with the GrassRustleSpawner on startup.
        /// </summary>
        private void Start()
        {
            GrassRustleSpawner.Instance.SetTilemaps(grassTilemap, fxTilemap);
        }
    }
}
