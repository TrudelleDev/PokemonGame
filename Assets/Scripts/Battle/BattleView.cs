using System;
using System.Collections;
using PokemonGame.Battle.Models;
using PokemonGame.Battle.States;
using PokemonGame.Dialogue;
using PokemonGame.Party;
using PokemonGame.Pokemon;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle
{
    /// <summary>
    /// Manages the battle view, including HUDs, animations, dialogue, and state transitions.
    /// </summary>
    [DisallowMultipleComponent]
    public class BattleView : View
    {
        [SerializeField, Tooltip("The player's and opponent's HUDs for health, status, etc.")]
        private BattleHUDs battleHuds;

        [SerializeField, Tooltip("References to all battle-related components, including animations.")]
        private BattleComponents battleComponents;

        [SerializeField, Required, Tooltip("Dialogue box used to display battle messages during battle.")]
        private DialogueBox dialogueBox;

        private BattleStateMachine stateMachine;
        private PartyManager temporaryPlayerParty;
        private PartyManager playerParty;

        public PokemonInstance PlayerPokemon { get; private set; }
        public PokemonInstance OpponentPokemon { get; private set; }

        public BattleHUDs BattleHUDs => battleHuds;
        public BattleComponents Components => battleComponents;
        public DialogueBox DialogueBox => dialogueBox;
        public BattleStateMachine StateMachine => stateMachine;

        public event Action OnBattleViewClose;

        private bool isBattleInitialized = false;

        public bool IsInBattle = false;

        /// <summary>
        /// Initializes the battle with the player's party and opponent Pokémon.
        /// </summary>
        public void Initialize(PartyManager playerParty, PokemonInstance opponentPokemon)
        {
            this.playerParty = playerParty;

            // Preserve original party order and set up temporary battle party.
            playerParty.SaveOriginalPartyOrder();
            temporaryPlayerParty = playerParty;

            PlayerPokemon = temporaryPlayerParty.Members[0];
            OpponentPokemon = opponentPokemon;

            battleHuds.Player.Bind(PlayerPokemon);
            battleHuds.Opponent.Bind(OpponentPokemon);

            stateMachine = new BattleStateMachine(this);

            // Reset intro animation for a fresh start.
            battleComponents.Animation.ResetIntro();
            isBattleInitialized = true;
            IsInBattle = true;
        }

        private void OnEnable()
        {
            if (isBattleInitialized)
                StartCoroutine(PlayIntro());
        }

        private void OnDisable()
        {
            ExitBattle();
        }

        protected override void Update()
        {
            base.Update();
            stateMachine.Update();
        }

        public override void Hide()
        {
            battleComponents.Animation.ResetIntro();
            base.Hide();
        }

        /// <summary>
        /// Handles Pokémon switching using the temporary party order.
        /// </summary>
        public void TemporarySwap()
        {
            var partyMenu = ViewManager.Instance.Get<PartyMenuView>();
            partyMenu.Party.Swap(0, temporaryPlayerParty.SelectedIndex);

            PlayerPokemon = temporaryPlayerParty.SelectedPokemon;
            battleHuds.Player.Bind(PlayerPokemon);
        }

        /// <summary>
        /// Plays the battle intro after view transitions complete.
        /// </summary>
        private IEnumerator PlayIntro()
        {
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);
            stateMachine.SetState(new IntroState(stateMachine));
        }

        /// <summary>
        /// Cleans up the battle view when disabled or closed.
        /// </summary>
        private void ExitBattle()
        {
            battleHuds.Player.Unbind();
            battleHuds.Opponent.Unbind();

            playerParty.RestorePartyOrder();
            isBattleInitialized = false;
            IsInBattle = false;

            OnBattleViewClose?.Invoke();
        }
    }
}
