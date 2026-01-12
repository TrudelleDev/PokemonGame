using System.Collections;
using PokemonGame.Battle.Models;

namespace PokemonGame.Battle.States
{
    internal sealed class TrainerBattleIntroState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView BattleView => machine.BattleView;

        internal TrainerBattleIntroState(BattleStateMachine machine)
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
            var anim = BattleView.Components.Animation;
            var opponent = BattleView.OpponentActivePokemon;

            anim.ResetIntro();

            anim.PlayPlayerPlatformEnter();
            anim.PlayPlayerTrainerSpriteEnter();
            anim.PlayOpponentTrainerEnter();

            yield return anim.PlayOpponentPlatformEnter();
            //anim.PlayOpponentPokemonBarEnter();
          //  anim.PlayPlayerPokemonBarEnter();

            yield return anim.PlayPokemonBars();


            // Logic for the wild Pokemon appearing
            yield return PlayOpponentEntrance();

            // Logic for the player sending out their first Pokemon
            yield return PlayPlayerEntrance();

            machine.SetState(new PlayerActionState(machine));
        }

        private IEnumerator PlayOpponentEntrance()
        {
            var anim = BattleView.Components.Animation;
            var opponent = BattleView.OpponentActivePokemon;

         

          //  anim.PlayOpponentTrainerEnter();
          
           // yield return anim.PlayOpponentHudEnter();

            //BattleView.Components.Audio.PlayPokemonCry(opponent);

            yield return BattleView.DialogueBox.ShowDialogueAndWaitForInput($"{BattleView.Opponent.DisplayName}\nwould like to battle!");
            yield return BattleView.DialogueBox.ShowDialogueAndWait($"{BattleView.Opponent.DisplayName}\nsend out {opponent.Definition.DisplayName}!");

            anim.PlayOpponentPokemonBarExit();
            yield return anim.PlayOpponentTrainerExit();
            yield return anim.PlayTrainerPokemonEnter();
            yield return anim.PlayOpponentHudEnter();


            //  yield return BattleView.DialogueBox.ShowDialogueAndWaitForInput($"Wild {opponent.Definition.DisplayName} appeared!");
        }

        private IEnumerator PlayPlayerEntrance()
        {
            var anim = BattleView.Components.Animation;
            var player = BattleView.PlayerActivePokemon;
            var playerName = player.Definition.DisplayName;

            BattleView.DialogueBox.ShowDialogue($"Go {playerName}!");

            anim.PlayPlayerTrainerExit();
            anim.PlayPlayerPokemonBarExit();
            yield return anim.PlayPlayerThrowBallSequence();

            yield return BattleView.Components.Audio.PlayOpenPokeball();
            BattleView.Components.Audio.PlayPokemonCry(player);

            yield return anim.PlayPlayerSendPokemonEnter();
            
            yield return anim.PlayPlayerHudEnter();
        }
    }
}