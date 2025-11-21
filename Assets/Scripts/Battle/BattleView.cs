using System;
using System.Collections;
using PokemonGame.Battle.States;
using PokemonGame.Battle.UI;
using PokemonGame.Dialogue;
using PokemonGame.Pokemons;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle
{
    /// <summary>
    /// Main view responsible for managing the Pokémon battle interface and state transitions.
    /// Coordinates battle animations, dialogue, UI panels, and state machine updates.
    /// </summary>
    [DisallowMultipleComponent]
    public class BattleView : View
    {
        [Title("Dialogue Box")]
        [SerializeField, Required]
        [Tooltip("Dialogue box theme applied when the battle starts.")]
        private DialogueBoxTheme theme;

        [Title("Animation")]
        [SerializeField, Required]
        [Tooltip("Controls all battle animations for player and opponent.")]
        private BattleAnimation battleAnimation;

        [Title("Audio")]
        [SerializeField, Required]
        [Tooltip("Handles all battle-related audio effects and music.")]
        private BattleAudio battleAudio;

        [Title("HUDs")]
        [SerializeField, Required]
        [Tooltip("Displays the player's Pokémon information, such as HP, level, and status.")]
        private PlayerBattleHud playerBattleHud;

        [SerializeField, Required]
        [Tooltip("Displays the opponent's Pokémon information, such as HP, level, and status.")]
        private OpponentBattleHud opponentBattleHud;

        [Title("UI Panels")]
        [SerializeField, Required]
        [Tooltip("Panel containing the player's battle action options (Fight, Bag, Pokémon, Run).")]
        private PlayerActionPanel playerActionPanel;

        [SerializeField, Required]
        [Tooltip("Panel displaying the available moves for the player's Pokémon.")]
        private MoveSelectionPanel moveSelectionPanel;

        [SerializeField, Required]
        [Tooltip("Controller that manages move selection logic and communicates with the battle state machine.")]
        private MoveSelectionView moveSelectionController;

        [SerializeField, Required]
        [Tooltip("The dialogue box used to display battle messages.")]
        private DialogueBox dialogueBox;

        private BattleStateMachine stateMachine;

        /// <summary>
        /// Gets the dialogue box used for displaying battle messages.
        /// </summary>
        public DialogueBox DialogueBox => dialogueBox;

        /// <summary>
        /// Gets the battle audio controller.
        /// </summary>
        public BattleAudio BattleAudio => battleAudio;

        /// <summary>
        /// Gets the battle animation controller.
        /// </summary>
        public BattleAnimation BattleAnimation => battleAnimation;

        /// <summary>
        /// Gets the move selection controller responsible for handling move selection logic.
        /// </summary>
        public MoveSelectionView MoveSelectionController => moveSelectionController;

        /// <summary>
        /// Gets the move selection panel used for displaying move options.
        /// </summary>
        public MoveSelectionPanel MoveSelectionPanel => moveSelectionPanel;

        /// <summary>
        /// Gets the opponent's battle HUD.
        /// </summary>
        public OpponentBattleHud OpponentBattleHud => opponentBattleHud;

        /// <summary>
        /// Gets the player's battle HUD.
        /// </summary>
        public PlayerBattleHud PlayerBattleHud => playerBattleHud;

        /// <summary>
        /// Gets the current battle state machine.
        /// </summary>
        public BattleStateMachine StateMachine => stateMachine;

        /// <summary>
        /// The player's active Pokémon in the current battle.
        /// </summary>
        public PokemonInstance PlayerPokemon { get; private set; }

        /// <summary>
        /// The opponent's active Pokémon in the current battle.
        /// </summary>
        public PokemonInstance OpponentPokemon { get; private set; }

        /// <summary>
        /// Event triggered when the battle view is closed (e.g., after a successful escape or faint).
        /// </summary>
        public event Action OnBattleViewClose;

        private bool isBattleInitialized = false;

        /// <summary>
        /// Initializes and configures a new battle session.
        /// Binds Pokémon data to HUDs and sets up the battle state machine.
        /// </summary>
        /// <param name="playerPokemon">The player's active Pokémon.</param>
        /// <param name="opponentPokemon">The opponent's active Pokémon.</param>
        public void Initialize(PokemonInstance playerPokemon, PokemonInstance opponentPokemon)
        {
            PlayerPokemon = playerPokemon;
            OpponentPokemon = opponentPokemon;

            playerBattleHud.Bind(playerPokemon);
            opponentBattleHud.Bind(opponentPokemon);

            stateMachine = new BattleStateMachine(this);

            moveSelectionController.Initialize(stateMachine, playerPokemon.Moves);
            BattleAnimation.ResetIntro();
            isBattleInitialized = true;
        }

        /// <summary>
        /// Called when the battle view becomes active.
        /// Applies dialogue settings and begins the intro sequence.
        /// </summary>
        private void OnEnable()
        {
            if (isBattleInitialized)
            {
                StartCoroutine(PlayIntro());
            }
        }

        /// <summary>
        /// Called when the battle view is disabled.
        /// Closes the dialogue box and triggers the view close event.
        /// </summary>
        private void OnDisable()
        {
            playerBattleHud.Unbind();
            opponentBattleHud.Unbind();

            OnBattleViewClose?.Invoke();
            isBattleInitialized = false;
        }

        /// <summary>
        /// Waits for scene transitions to complete before starting the battle intro state.
        /// </summary>
        private IEnumerator PlayIntro()
        {
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);
            stateMachine.SetState(new BattleIntroState(stateMachine));
        }

        /// <summary>
        /// Updates the battle state machine each frame.
        /// </summary>
        protected override void Update()
        {
            base.Update();
            stateMachine.Update();
        }

        /// <summary>
        /// Resets the intro animation state when the view is hidden.
        /// </summary>
        public override void Hide()
        {
            battleAnimation.ResetIntro();
            base.Hide();
        }

        /// <summary>
        /// Freezes player interaction with the battle UI.
        /// </summary>
        public override void Freeze()
        {
            // playerActionPanel.Freeze();
            base.Freeze();
        }

        /// <summary>
        /// Re-enables player interaction with the battle UI.
        /// </summary>
        public override void Unfreeze()
        {
            // playerActionPanel.UnFreeze();
            base.Unfreeze();
        }
    }
}
