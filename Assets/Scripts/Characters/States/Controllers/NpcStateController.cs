using PokemonGame.Characters.Inputs;
using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// A flexible state controller for all non-player characters.
    /// Works with NpcInput (wandering) or TrainerInput (fixed/staring).
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class NpcStateController : CharacterStateController
    {
        private CharacterInput aiInput;

        public override CharacterInput Input => aiInput;

        protected override void Awake()
        {
            base.Awake();
            // Finds whatever CharacterInput is on this GameObject
            aiInput = GetComponent<CharacterInput>();
        }
    }
}
