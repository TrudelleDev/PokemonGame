using System;
using PokemonGame.Ability;
using PokemonGame.Move;
using PokemonGame.Nature;
using PokemonGame.Pokemon.Components;

namespace PokemonGame.Pokemon
{
    /// <summary>
    /// Runtime Pokémon instance built from static definitions (species, nature, ability, moves).
    /// Holds dynamic state like level, stats, gender, health, and owner info.
    /// </summary>
    [Serializable]
    public class PokemonInstance
    {
        public ExperienceComponent Experience { get; private set; }
        public HealthComponent Health { get; private set; }
        public StatsComponent Stats { get; private set; }
        public MetadataComponent Meta { get; private set; }
        public MovesComponent Moves { get; private set; }
        public GenderComponent Gender { get; private set; }
        public PokemonDefinition Definition { get; private set; }
        public NatureInstance Nature { get; private set; }
        public AbilityInstance Ability { get; private set; }

        public PokemonInstance(int level, PokemonDefinition definition, NatureDefinition natureDefinition, AbilityDefinition abilityDefinition, MoveDefinition[] moveDefinitions)
        {
            Definition = definition;

            Experience = new ExperienceComponent(level);
            Health = new HealthComponent(level);
            Stats = new StatsComponent(level, definition);
            Meta = new MetadataComponent();
            Moves = new MovesComponent(moveDefinitions);
            Gender = new GenderComponent(level);

            Ability = new AbilityInstance(abilityDefinition);
            Nature = new NatureInstance(natureDefinition);
        }
    }
}

