using System;
using System.Collections;
using MonsterTamer.Audio;
using MonsterTamer.Battle.Models;
using MonsterTamer.Battle.States.Core;
using MonsterTamer.Battle.States.Intro;
using MonsterTamer.Characters;
using MonsterTamer.Dialogue;
using MonsterTamer.Monster;
using MonsterTamer.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTamer.Battle
{
    /// <summary>
    /// Manages the battle view lifecycle, including HUD binding,
    /// animations, dialogue, and battle state machine coordination.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class BattleView : View
    {
        [SerializeField, Tooltip("Background music clip that plays when a battle begins.")]
        private AudioClip battleBgm;

        [SerializeField, Tooltip("The player's and opponent's HUDs for health, status, etc.")]
        private BattleHuds battleHuds;

        [SerializeField, Tooltip("References to all battle-related components, including animations.")]
        private BattleComponents components;

        [SerializeField, Required, Tooltip("Dialogue box used to display battle messages.")]
        private DialogueBox dialogueBox;

        [SerializeField, Required, Tooltip("Sprite used to display the opponent trainer during battle.")]
        private Image opponentTrainerSprite;

        private BattleStateMachine stateMachine;
        private IBattleState introState;

        internal event Action OnBattleViewClose;

        internal readonly WaitForSecondsRealtime TurnPauseYield = new(1f);

        internal Character Player { get; private set; }
        internal Character Opponent { get; private set; }
        internal MonsterInstance PlayerActiveMonster { get; private set; }
        internal MonsterInstance OpponentActiveMonster { get; private set; }

        internal BattleHuds BattleHUDs => battleHuds;
        internal BattleComponents Components => components;
        internal DialogueBox DialogueBox => dialogueBox;

        private void OnEnable()
        {
            StartCoroutine(PlayIntro());
        }

        protected override void Update()
        {
            stateMachine?.Update();
            base.Update();
        }

        /// <summary>
        /// Initializes a wild battle against a single Monster.
        /// </summary>
        internal void InitializeWildBattle(Character player, MonsterInstance wildMonster)
        {
            InitializeBattle(player, null, player.Party.Members[0], wildMonster);
            introState = new WildBattleIntroState(stateMachine);

            AudioManager.Instance.PlayBGM(battleBgm);
        }

        /// <summary>
        /// Initializes a trainer battle against another character.
        /// </summary>
        internal void InitializeTrainerBattle(Character player, Character opponent)
        {
            InitializeBattle(player, opponent, player.Party.Members[0], opponent.Party.Members[0]);
            introState = new TrainerBattleIntroState(stateMachine);

            opponentTrainerSprite.sprite = opponent.Definition.BattleSprite;
            AudioManager.Instance.PlayBGM(battleBgm);
        }

        /// <summary>
        /// Generalized initialization logic for wild and trainer battles.
        /// </summary>
        private void InitializeBattle(Character player, Character opponentOrNull, MonsterInstance playerMonster, MonsterInstance opponentMonster)
        {
            Player = player;
            Opponent = opponentOrNull;

            Player.Party.SaveOriginalPartyOrder();

            PlayerActiveMonster = playerMonster;
            OpponentActiveMonster = opponentMonster;

            battleHuds.PlayerBattleHud.Bind(PlayerActiveMonster);
            battleHuds.OpponentBattleHud.Bind(OpponentActiveMonster);

            stateMachine = new BattleStateMachine(this);
        }

        /// <summary>
        /// Replaces the opponent's active Monster (trainer battles only).
        /// </summary>
        internal void SetNextOpponentMonster(MonsterInstance monster)
        {
            OpponentActiveMonster = monster;
            battleHuds.OpponentBattleHud.Bind(monster);
        }

        /// <summary>
        /// Replaces the player's active Monster and updates party order.
        /// </summary>
        internal void SetNextPlayerMonster(MonsterInstance monster)
        {
            PlayerActiveMonster = monster;
            battleHuds.PlayerBattleHud.Bind(monster);
            Player.Party.Swap(0, Player.Party.SelectedIndex);
        }

        /// <summary>
        /// Cleans up the battle state and closes the battle view.
        /// </summary>
        internal void CloseBattle()
        {
            battleHuds.PlayerBattleHud.Unbind();
            battleHuds.OpponentBattleHud.Unbind();

            Player.Party.RestorePartyOrder();
            components.Animation.ResetIntro();

            OnBattleViewClose?.Invoke();
            dialogueBox.Clear();

            ViewManager.Instance.Close<BattleView>();
        }

        /// <summary>
        /// Plays the battle intro once the view transition has completed.
        /// </summary>
        private IEnumerator PlayIntro()
        {
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);
            stateMachine.SetState(introState);
        }
    }
}
