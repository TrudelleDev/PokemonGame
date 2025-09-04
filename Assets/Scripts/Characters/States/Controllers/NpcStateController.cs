using PokemonGame.Characters.Inputs;
using UnityEngine;

namespace PokemonGame.Characters.States
{
    /// <summary>
    /// State controller for NPC characters.
    /// Uses <see cref="NpcInput"/> to drive movement and actions.
    /// </summary>
    [RequireComponent(typeof(NpcInput))]
    public class NpcStateController : CharacterStateController
    {
        private NpcInput computerInput;

        /// <summary>
        /// Provides NPC input source (random wandering AI).
        /// </summary>
        public override CharacterInput Input => computerInput;

        protected override void Awake()
        {
            base.Awake();
            computerInput = GetComponent<NpcInput>();
        }
    }
}
