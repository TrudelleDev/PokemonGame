using System;
using PokemonGame.Battle.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle.Models
{
    /// <summary>
    /// Holds references to all HUD and UI components used in a battle.
    /// Provides quick access to the player's HUD, opponent HUD, action panels, and move selection UI.
    /// </summary>
    [Serializable]
    internal struct BattleHUDs
    {
        [SerializeField, Required, Tooltip("Displays the player's Pokémon information, such as HP, level, and status.")]
        private PlayerBattleHud player;

        [SerializeField, Required, Tooltip("Displays the opponent's Pokémon information, such as HP, level, and status.")]
        private OpponentBattleHud opponent;

        [SerializeField, Required, Tooltip("Panel containing the player's battle action options (Fight, Bag, Pokémon, Run).")]
        private BattleActionView playerActions;

        [SerializeField, Required, Tooltip("Panel displaying the player's available moves as buttons.")]
        private BattleMoveSelectionPanel moveSelection;

        [SerializeField, Required, Tooltip("Controller responsible for handling move selection logic and updating the move selection panel.")]
        private MoveSelectionView moveController;

        public PlayerBattleHud Player => player;
        public OpponentBattleHud Opponent => opponent;
        internal BattleActionView PlayerActions => playerActions;
        public BattleMoveSelectionPanel MoveSelection => moveSelection;
        internal MoveSelectionView MoveController => moveController;
    }
}
