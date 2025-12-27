using PokemonGame.Battle;
using PokemonGame.Pokemon;

namespace PokemonGame.Move.Models
{
    public readonly struct MoveContext
    {
        internal BattleView Battle { get; }
        public PokemonInstance User { get; }
        public PokemonInstance Target { get; }
        public MoveInstance Move { get; }

        internal MoveContext(BattleView battle, PokemonInstance user, PokemonInstance target, MoveInstance move)
        {
            Battle = battle;
            User = user;
            Target = target;
            Move = move;
        }
    }
}
