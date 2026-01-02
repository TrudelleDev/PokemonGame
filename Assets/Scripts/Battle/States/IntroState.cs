using System.Collections;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Executes the battle introductory sequence, including animations and dialogue.
    /// Transitions to <see cref="PlayerActionState"/> upon completion.
    /// </summary>
    internal sealed class IntroState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView BattleView => machine.BattleView;

        internal IntroState(BattleStateMachine machine)
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
            // Logic for the wild Pokemon appearing
            yield return PlayOpponentEntrance();

            // Logic for the player sending out their first Pokemon
            yield return PlayPlayerEntrance();

            machine.SetState(new PlayerActionState(machine));
        }

        private IEnumerator PlayOpponentEntrance()
        {
            var anim = BattleView.Components.Animation;
            var opponent = BattleView.OpponentPokemon;

            anim.ResetIntro();
            anim.PlayIntro();

            yield return anim.PlayOpponentPlatformEnter();
            yield return anim.PlayOpponentHudEnter();

            BattleView.Components.Audio.PlayPokemonCry(opponent);

            yield return BattleView.DialogueBox.ShowDialogueAndWaitForInput($"Wild {opponent.Definition.DisplayName} appeared!");
        }

        private IEnumerator PlayPlayerEntrance()
        {
            var anim = BattleView.Components.Animation;
            var player = BattleView.PlayerPokemon;
            var playerName = player.Definition.DisplayName;

            BattleView.DialogueBox.ShowDialogue($"Go {playerName}!");

            anim.PlayPlayerTrainerExit();
            yield return anim.PlayPlayerThrowBallSequence();

            yield return BattleView.Components.Audio.PlayOpenPokeball();
            BattleView.Components.Audio.PlayPokemonCry(player);

            yield return anim.PlayPlayerSendPokemonEnter();
            yield return anim.PlayPlayerHudEnter();
        }
    }
}