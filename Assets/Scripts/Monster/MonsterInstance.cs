using MonsterTamer.Monster.Components;
using MonsterTamer.Move;
using MonsterTamer.Nature;

namespace MonsterTamer.Monster
{
    /// <summary>
    /// Represents a runtime Monster instance with dynamic state.
    /// Acts as the central hub for components governing health, stats, and progression.
    /// </summary>
    internal class MonsterInstance
    {
        internal ExperienceComponent Experience { get; }
        internal HealthComponent Health { get; }
        internal StatsComponent Stats { get; }
        internal MetadataComponent Meta { get; }
        internal MovesComponent Moves { get; }
        internal MonsterDefinition Definition { get; }
        internal NatureInstance Nature { get; }

        internal bool IsFainted => Health.CurrentHealth <= 0;

        internal MonsterInstance(int level, MonsterDefinition definition, NatureDefinition natureDefinition, MoveDefinition[] moveDefinitions)
        {
            Definition = definition;

            Nature = new NatureInstance(natureDefinition);
            Experience = new ExperienceComponent(level);
            Stats = new StatsComponent(this);  
            Health = new HealthComponent(Stats.Core.HealthPoint); 
            Meta = new MetadataComponent();
            Moves = new MovesComponent(moveDefinitions);
        }


        internal MoveInstance GetRandomMove()
        {
            if (Moves.Moves.Length == 0) return null;

            return Moves.Moves[UnityEngine.Random.Range(0, Moves.Moves.Length)];
        }
    }
}

