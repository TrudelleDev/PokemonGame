using UnityEngine;

namespace MonsterTamer.Battle.Animations
{
    /// <summary>
    /// Contains static Animator state hashes used by battle animation sequences.
    /// </summary>
    public static class BattleAnimationState
    {
        // Player
        public static readonly int PlayerHudEnter = Animator.StringToHash("PlayerHudEnter");
        public static readonly int PlayerHudExit = Animator.StringToHash("PlayerHudExit");
        public static readonly int PlayerMonsterEnter = Animator.StringToHash("PlayerMonsterEnter");
        public static readonly int PlayerMonsterExit = Animator.StringToHash("PlayerMonsterExit");
        public static readonly int PlayerMonsterTakeDamage = Animator.StringToHash("PlayerMonsterTakeDamage");
        public static readonly int PlayerMonsterDeath = Animator.StringToHash("PlayerMonsterDeath");
        public static readonly int PlayerTrainerEnter = Animator.StringToHash("PlayerTrainerEnter");
        public static readonly int PlayerTrainerExit = Animator.StringToHash("PlayerTrainerExit");

        // Opponent
        public static readonly int OpponentHudEnter = Animator.StringToHash("OpponentHudEnter");
        public static readonly int OpponentHudExit = Animator.StringToHash("OpponentHudExit");
        public static readonly int OpponentMonsterEnter = Animator.StringToHash("OpponentMonsterEnter");
        public static readonly int OpponentMonsterExit = Animator.StringToHash("OpponentMonsterExit");
        public static readonly int OpponentMonsterTakeDamage = Animator.StringToHash("OpponentMonsterTakeDamage");
        public static readonly int OpponentMonsterDeath = Animator.StringToHash("OpponentMonsterDeath");
        public static readonly int OpponentTrainerEnter = Animator.StringToHash("OpponentTrainerEnter");
        public static readonly int OpponentTrainerExit = Animator.StringToHash("OpponentTrainerExit");
        public static readonly int OpponentTrainerDefeatOutro = Animator.StringToHash("OpponentTrainerDefeatOutro");

        // Wild Monster
        public static readonly int WildMonsterEnter = Animator.StringToHash("WildMonsterEnter");
    }
}
