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
    internal struct BattleHuds
    {
        [SerializeField, Required, Tooltip("Displays the player's Monster information.")]
        private PlayerBattleHud playerBattleHud;

        [SerializeField, Required, Tooltip("Displays the opponent's Monster information.")]
        private OpponentBattleHud opponentBattleHud;

        internal readonly PlayerBattleHud PlayerBattleHud => playerBattleHud;
        internal readonly OpponentBattleHud OpponentBattleHud => opponentBattleHud;
    }
}
