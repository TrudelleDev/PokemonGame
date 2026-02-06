using System;
using System.Collections;
using PokemonGame.Audio;
using PokemonGame.Battle.Models;
using PokemonGame.Battle.States.Core;
using PokemonGame.Battle.States.Intro;
using PokemonGame.Characters;
using PokemonGame.Dialogue;
using PokemonGame.Pokemon;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Battle
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
        private BattleHUDs battleHuds;

        [SerializeField, Tooltip("References to all battle-related components, including animations.")]
        private BattleComponents components;

        [SerializeField, Required, Tooltip("Dialogue box used to display battle messages.")]
        private DialogueBox dialogueBox;

        [SerializeField, Required, Tooltip("Sprite used to display the opponent trainer during battle.")]
        private Image opponentTrainerSprite;

        private BattleStateMachine stateMachine;
        private IBattleState introState;

        /// <summary>
        /// Raised when the battle view has fully exited and cleaned up.
        /// </summary>
        internal event Action OnBattleViewClose;

        /// <summary>
        /// Cached pause duration used between turn-based battle actions.
        /// </summary>
        internal readonly WaitForSecondsRealtime TurnPauseYield = new(0.5f);

        /// <summary>
        /// The player character participating in the current battle.
        /// </summary>
        internal Character Player { get; private set; }

        /// <summary>
        /// The opposing character in a trainer battle.
        /// Null for wild battles.
        /// </summary>
        internal Character Opponent { get; private set; }

        /// <summary>
        /// The player's currently active Monster in battle.
        /// </summary>
        internal PokemonInstance PlayerActiveMonster { get; private set; }

        /// <summary>
        /// The opponent's currently active Monster in battle.
        /// </summary>
        internal PokemonInstance OpponentActiveMonster { get; private set; }

        /// <summary>
        /// Provides access to all battle HUD elements.
        /// </summary>
        internal BattleHUDs BattleHUDs => battleHuds;

        /// <summary>
        /// Provides access to shared battle components such as animations.
        /// </summary>
        internal BattleComponents Components => components;

        /// <summary>
        /// Dialogue box used to display battle messages and prompts.
        /// </summary>
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
        /// <param name="player">The player character participating in the battle.</param>
        /// <param name="monster">The wild Monster to battle against.</param>
        internal void InitializeWildBattle(Character player, PokemonInstance monster)
        {
            Player = player;
            Opponent = null;

            player.Party.SaveOriginalPartyOrder();

            PlayerActiveMonster = player.Party.Members[0];
            OpponentActiveMonster = monster;

            battleHuds.PlayerBattleHud.Bind(PlayerActiveMonster);
            battleHuds.OpponentBattleHud.Bind(OpponentActiveMonster);

            stateMachine = new BattleStateMachine(this);
            introState = new WildBattleIntroState(stateMachine);

            dialogueBox.Clear();

            AudioManager.Instance.PlayBGM(battleBgm);
        }

        /// <summary>
        /// Initializes a trainer battle against another character.
        /// </summary>
        /// <param name="player">The player character participating in the battle.</param>
        /// <param name="opponent">The opposing trainer character.</param>
        internal void InitializeTrainerBattle(Character player, Character opponent)
        {
            Player = player;
            Opponent = opponent;

            player.Party.SaveOriginalPartyOrder();

            PlayerActiveMonster = player.Party.Members[0];
            OpponentActiveMonster = opponent.Party.Members[0];

            battleHuds.PlayerBattleHud.Bind(PlayerActiveMonster);
            battleHuds.OpponentBattleHud.Bind(OpponentActiveMonster);

            opponentTrainerSprite.sprite = opponent.Definition.BattleSprite;

            stateMachine = new BattleStateMachine(this);
            introState = new TrainerBattleIntroState(stateMachine);

            dialogueBox.Clear();

            AudioManager.Instance.PlayBGM(battleBgm);
        }

        /// <summary>
        /// Replaces the opponent's active Monster (trainer battles only).
        /// </summary>
        /// <param name="monster">The new opponent Monster to display in battle.</param>
        internal void SetNextOpponentMonster(PokemonInstance monster)
        {
            OpponentActiveMonster = monster;
            battleHuds.OpponentBattleHud.Bind(monster);
        }

        /// <summary>
        /// Replaces the player's active Monster and updates party order.
        /// </summary>
        /// <param name="monster">The new player Monster to display in battle.</param>
        internal void SetNextPlayerMonster(PokemonInstance monster)
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
