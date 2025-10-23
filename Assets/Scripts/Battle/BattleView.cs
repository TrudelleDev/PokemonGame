using System.Collections;
using PokemonGame.Battle.States;
using PokemonGame.Battle.UI;
using PokemonGame.Dialogue;
using PokemonGame.Pokemons;
using PokemonGame.Transitions;
using PokemonGame.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PokemonGame.Battle
{
    [DisallowMultipleComponent]
    public class BattleView : View
    {
        [SerializeField, Required]
        private DialogueBoxSetting setting;

        [SerializeField, Required]
        private BattleAnimation battleAnimation;

        private BattleStateMachine stateMachine;
        private DialogueBoxView dialogueBox;

        [SerializeField, Required]
        private PlayerBattleHud playerBattleHud;

        [SerializeField, Required]
        private OpponentBattleHud opponentBattleHud;

        public DialogueBoxView DialogueBox => dialogueBox;

        public BattleAnimation BattleAnimation => battleAnimation;

        public PlayerBattleHud PlayerBattleHud => playerBattleHud;

        public Pokemon PlayerPokemon { get; private set; }
        public Pokemon OpponentPokemon { get; private set; }

        private void Awake()
        {
            stateMachine = new BattleStateMachine(this);
            battleAnimation.Initialize(this);
        }

        private void OnEnable()
        {
            Transition.OnFadeOutComplete += PlayIntroSequence;
        }

        private void OnDisable()
        {
            Transition.OnFadeOutComplete -= PlayIntroSequence;
        }

        protected override void Update()
        {
            base.Update();
            stateMachine.Update();
        }

        public void SetupBattle(Pokemon playerPokemon, Pokemon opponentPokemon)
        {
            PlayerPokemon = playerPokemon;
            OpponentPokemon = opponentPokemon;

            playerBattleHud.Bind(playerPokemon);
            opponentBattleHud.Bind(opponentPokemon);
        }

        private void PlayIntroSequence()
        {
            StartCoroutine(PlayIntroSequenceRoutine());
        }

        private IEnumerator PlayIntroSequenceRoutine()
        {
            yield return new WaitUntil(() => !ViewManager.Instance.IsTransitioning);

            dialogueBox = ViewManager.Instance.Show<DialogueBoxView>();
            dialogueBox.ApplySetting(setting);

            stateMachine.SetState(new BattleIntroState(stateMachine));
        }
    }
}
