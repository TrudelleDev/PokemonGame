using System.Collections;
using PokemonGame.Pokemon;

namespace PokemonGame.Battle.States
{
    /// <summary>
    /// Handles sending the opponent's next usable Pokémon into battle
    /// after the previous one has fainted.
    /// </summary>
    internal sealed class OpponentSendNextPokemonState : IBattleState
    {
        private readonly BattleStateMachine machine;
        private BattleView Battle => machine.BattleView;

        public OpponentSendNextPokemonState(BattleStateMachine machine)
        {
            this.machine = machine;
        }

        public void Enter()
        {
            Battle.StartCoroutine(PlaySequence());
        }

        private IEnumerator PlaySequence()
        {
            var anim = Battle.Components.Animation;
            var opponent = Battle.OpponentActivePokemon;

            PokemonInstance nextPokemon = GetNextUsablePokemon();

            if (nextPokemon == null)
            {
                machine.SetState(new VictoryState(machine));
                yield break;
            }

            Battle.SetNextOpponentPokemon(nextPokemon);

            yield return Battle.DialogueBox.ShowDialogueAndWait($"{Battle.Opponent.Definition.DisplayName}\nsend out {opponent.Definition.DisplayName}!");

            yield return anim.PlayTrainerPokemonEnter();
            yield return anim.PlayOpponentHudEnter();

            machine.SetState(new PlayerActionState(machine));
        }


        public void Exit()
        {
        }

  
        private PokemonInstance GetNextUsablePokemon()
        {
            foreach (var pokemon in Battle.Opponent.Party.Members)
            {
                if (pokemon.Health.CurrentHealth > 0)
                {
                    return pokemon;
                }
            }

            return null;
        }

        public void Update()
        {
         
        }
    }
}
