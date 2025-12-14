using System.Collections;
using PokemonGame.Battle.UI;
using PokemonGame.Views;
using UnityEngine;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Displays the player's primary action menu (Fight, Bag, Pokémon, Run) and waits for user input.
    /// Transitions the state machine based on the selection.
    /// </summary>
    public sealed class PlayerActionState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private PlayerActionPanel actionPanel;

        private BattleView Battle => machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerActionState"/> class.
        /// </summary>
        /// <param name="machine">The battle state machine context.</param>
        public PlayerActionState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        // --- IBattleState Implementation ---

        /// <summary>
        /// Called when entering the state. Initializes animations and starts the UI display coroutine.
        /// </summary>
        public void Enter()
        {
            // Set Pokemon and HUD to idle/ready state
            Battle.Components.Animation.PlayPlayerPokemonIdle();
            Battle.Components.Animation.PlayPlayerHUDIdle();

            // Start the coroutine for asynchronous UI setup and waiting for input
            Battle.StartCoroutine(SetupUIAndAwaitInput());
        }

        public void Update() { }

        /// <summary>
        /// Cleans up the UI and unsubscribes all event handlers before the state is replaced.
        /// </summary>
        public void Exit()
        {
            if (actionPanel == null)
                return;

            // 1. Unsubscribe events to prevent memory leaks/dangling references
            actionPanel.OnFightSelected -= OnFightSelected;
            actionPanel.OnPartySelected -= OnPartySelected;
            actionPanel.OnBagSelected -= OnBagSelected;
            actionPanel.OnRunSelected -= OnRunSelected;

            // 2. Hide the action panel and clear the dialogue box
            ViewManager.Instance.Close<PlayerActionPanel>();
            Battle.DialogueBox.Clear();

            actionPanel = null;
        }

        private IEnumerator SetupUIAndAwaitInput()
        {
            // Wait until scene transitions are complete before showing the menu
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            // Show the action menu UI panel
            actionPanel = ViewManager.Instance.Show<PlayerActionPanel>();

            // Display the prompt message
            Battle.DialogueBox.ShowDialogue(
                $"What will\n{Battle.PlayerPokemon.Definition.DisplayName} do?",
                instant: true,
                manualArrowControl: false
            );

            // Subscribe to panel events to listen for player input
            actionPanel.OnFightSelected += OnFightSelected;
            actionPanel.OnPartySelected += OnPartySelected;
            actionPanel.OnBagSelected += OnBagSelected;
            actionPanel.OnRunSelected += OnRunSelected;
        }

        private void OnFightSelected() => machine.SetState(new MoveSelectionState(machine));
        private void OnPartySelected() => machine.SetState(new SelectPokemonState(machine));
        private void OnBagSelected() => machine.SetState(new UseItemState(machine));
        private void OnRunSelected() => machine.SetState(new EscapeState(machine));
    }
}