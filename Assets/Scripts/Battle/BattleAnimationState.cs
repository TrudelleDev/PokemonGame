using UnityEngine;

namespace PokemonGame.Battle
{
    /// <summary>
    /// Contains static Animator state hashes used by battle animation sequences.
    /// Using hashed ints improves performance compared to string lookups.
    /// </summary>
    public static class BattleAnimationState
    {
        public static readonly int IdleStatic = Animator.StringToHash("IdleStatic");
        public static readonly int Idle = Animator.StringToHash("Idle");
        public static readonly int Enter = Animator.StringToHash("Enter");
        public static readonly int Exit = Animator.StringToHash("Exit");
        public static readonly int Throw = Animator.StringToHash("Throw");
        public static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly int Withdraw = Animator.StringToHash("Withdraw");
        public static readonly int TrainerPokemonEnter = Animator.StringToHash("TrainerPokemonEnter");
        public static readonly int WildPokemonEnter = Animator.StringToHash("WildPokemonEnter");
    }
}
