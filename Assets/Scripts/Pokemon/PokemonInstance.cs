using MonsterTamer.Move;
using MonsterTamer.Nature;
using MonsterTamer.Pokemon.Components;

namespace MonsterTamer.Pokemon
{
    /// <summary>
    /// Represents a runtime Pokémon instance with dynamic state (level, stats, health, moves, etc.)
    /// constructed from static definitions (species, nature, ability, moves).
    /// </summary>
    internal class PokemonInstance
    {
        public ExperienceComponent Experience { get; }
        public HealthComponent Health { get; }
        public StatsComponent Stats { get; }
        public MetadataComponent Meta { get; }
        public MovesComponent Moves { get; }
        public GenderComponent Gender { get; }
        public PokemonDefinition Definition { get; }
        public NatureInstance Nature { get; }

        public PokemonInstance(int level, PokemonDefinition definition, NatureDefinition natureDefinition, MoveDefinition[] moveDefinitions)
        {
            Definition = definition;

            Stats = new StatsComponent(level, definition);
            Meta = new MetadataComponent();
            Moves = new MovesComponent(moveDefinitions);
            Gender = new GenderComponent(level);
            Health = new HealthComponent(Stats.Core.HealthPoint);
            Experience = new ExperienceComponent(level);
            Nature = new NatureInstance(natureDefinition);
        }
    }
}

