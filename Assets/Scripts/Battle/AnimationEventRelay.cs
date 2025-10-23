using UnityEngine;

namespace PokemonGame.Battle
{
    public class AnimationEventRelay : MonoBehaviour
    {
        public BattleAnimation sequence; // assign in Inspector

        public void OnThrowEvent()
        {
            sequence.SetCanThrowPokeball();
        }
    }
}
