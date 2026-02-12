using MonsterTamer.Battle;
using MonsterTamer.Monster;

namespace MonsterTamer.Move.Models
{
    /// <summary>
    /// Contains all relevant data for executing a Monster move,
    /// including the battle, user, target, and move instance.
    /// </summary>
    internal struct MoveContext
    {
        /// <summary>
        /// The active battle where the move is being executed.
        /// </summary>
        internal BattleView Battle { get; }

        /// <summary>
        /// The Monster using the move.
        /// </summary>
        internal MonsterInstance User { get; }

        /// <summary>
        /// The Monster targeted by the move.
        /// </summary>
        internal MonsterInstance Target { get; }

        /// <summary>
        /// The move instance being used.
        /// </summary>
        internal MoveInstance Move { get; }

        /// <summary>
        /// Creates a new MoveContext with all required information.
        /// </summary>
        /// <param name="battle">The battle in which the move is executed.</param>
        /// <param name="user">The Monster using the move.</param>
        /// <param name="target">The Monster targeted by the move.</param>
        /// <param name="move">The move instance being used.</param>
        internal MoveContext(BattleView battle, MonsterInstance user, MonsterInstance target, MoveInstance move)
        {
            Battle = battle;
            User = user;
            Target = target;
            Move = move;
        }
    }
}
