using System.Collections;
using PokemonGame.Dialogue;
using PokemonGame.Pokemon;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Executes the full battle introductory sequence, including animations and dialogue, 
    /// and transitions the state machine to the <see cref="PlayerActionState"/>.
    /// </summary>
    public sealed class IntroState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView BattleView => this.machine.BattleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntroState"/> class.
        /// </summary>
        /// <param name="machine">The battle state machine context.</param>
        public IntroState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        /// <summary>
        /// Called when entering the state. Starts the asynchronous introduction sequence.
        /// </summary>
        public void Enter()
        {
            BattleView.StartCoroutine(PlaySequence());
        }

        public void Update() { }
        public void Exit() { }

        private IEnumerator PlaySequence()
        {
            // Cache components and data from the BattleView
            var animation = BattleView.Components.Animation;
            var audio = BattleView.Components.Audio;
            var dialogue = BattleView.DialogueBox;
            var opponent = BattleView.OpponentPokemon;
            var player = BattleView.PlayerPokemon;

            string opponentName = opponent.Definition.DisplayName;
            string playerName = player.Definition.DisplayName;

            // Opponent Entrance
            yield return PlayOpponentEntrance(animation, audio, dialogue, opponentName, opponent);

            // Player Entrance
            yield return PlayPlayerEntrance(animation, audio, dialogue, playerName, player);

            // Transition to the main action phase
            machine.SetState(new PlayerActionState(machine));
        }

        private IEnumerator PlayOpponentEntrance(
            BattleAnimation animation,
            BattleAudio audio,
            DialogueBox dialogue,
            string opponentName,
            PokemonInstance opponent)
        {
            animation.ResetIntro();
            animation.PlayIntro();

            yield return animation.PlayOpponentPlatformEnter();
            yield return animation.PlayOpponentHudEnter();

            audio.PlayPokemonCry(opponent);

            // Wait for the player to advance the dialogue
            yield return dialogue.ShowDialogueAndWaitForPlayerAdvance(
                $"Wild {opponentName} appeared!",
                manualArrowControl: true
            );
        }

        private IEnumerator PlayPlayerEntrance(
            BattleAnimation animation,
            BattleAudio audio,
            DialogueBox dialogue,
            string playerName,
            PokemonInstance player)
        {
            dialogue.ShowDialogue($"Go {playerName}!");

            animation.PlayPlayerTrainerExit();
            yield return animation.PlayPlayerThrowBallSequence();

            yield return audio.PlayOpenPokeball();
            audio.PlayPokemonCry(player);

            yield return animation.PlayPlayerSendPokemonEnter();
            yield return animation.PlayPlayerHudEnter();
        }
    }
}