using System;
using MonsterTamer.Battle.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonsterTamer.Battle.Models
{
    /// <summary>
    /// Holds references to all HUD and UI components used in a battle.
    /// Provides quick access to the player's HUD, opponent HUD, action panels, and move selection UI.
    /// </summary>
    [Serializable]
    internal struct BattleHUDs
    {
        [SerializeField, Required, Tooltip("Displays the player's Monster information.")]
        private PlayerBattleHud playerBattleHud;

        [SerializeField, Required, Tooltip("Displays the opponent's Monster information.")]
        private OpponentBattleHud opponentBattleHud;

        /// <summary>
        /// Displays the player's Monster information.
        /// </summary>
        internal readonly PlayerBattleHud PlayerBattleHud => playerBattleHud;

        /// <summary>
        /// Displays the opponent's Monster information.
        /// </summary>
        internal readonly OpponentBattleHud OpponentBattleHud => opponentBattleHud;
    }
}
