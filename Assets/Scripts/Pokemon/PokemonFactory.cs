using System;
using PokemonGame.Ability;
using PokemonGame.Move;
using PokemonGame.Move.Models;
using PokemonGame.Nature;
using PokemonGame.Utilities;
using UnityEngine;

namespace PokemonGame.Pokemon
{
    /// <summary>
    /// Factory class responsible for creating Pokémon instances at runtime.
    /// Supports creating wild Pokémon with randomized nature, ability, and moves.
    /// </summary>
    public static class PokemonFactory
    {
        private const int MaxMove = 4;

        /// <summary>
        /// Creates a wild Pokémon instance with the specified level and species definition.
        /// Randomly selects a nature, ability, and the latest moves according to the Pokémon's level-up moves.
        /// Logs an error if the Pokémon definition is null.
        /// </summary>
        /// <param name="level">The level to assign to the new Pokémon.</param>
        /// <param name="pokemonDefinition">The species definition used to create the Pokémon.</param>
        /// <returns>A new <see cref="PokemonInstance"/> instance, or null if <paramref name="pokemonDefinition"/> is null.</returns>
        public static PokemonInstance CreatePokemon(int level, PokemonDefinition pokemonDefinition)
        {
            if (pokemonDefinition == null)
            {
                Log.Error(nameof(PokemonFactory), "pokemonDefinition is null!");
                return null;
            }

            int abilityCount = pokemonDefinition.PossibleAbilities.Length;

            NatureDefinition natureDefinition = pokemonDefinition.PossibleNatures.GetRandomNature();
            AbilityDefinition abilityDefinition = pokemonDefinition.PossibleAbilities[UnityEngine.Random.Range(0, abilityCount)];
            MoveDefinition[] moveDefinitions = GetNewestLevelUpMoves(pokemonDefinition.LevelUpMoves);

            return new PokemonInstance(level, pokemonDefinition, natureDefinition, abilityDefinition, moveDefinitions);
        }

        private static MoveDefinition[] GetNewestLevelUpMoves(LevelUpMove[] levelUpMoves)
        {
            if (levelUpMoves == null || levelUpMoves.Length == 0)
            {
                return Array.Empty<MoveDefinition>();
            }

            // Sort the array in-place by level ascending
            Array.Sort(levelUpMoves, (a, b) => a.Level.CompareTo(b.Level));

            int takeCount = Mathf.Min(MaxMove, levelUpMoves.Length);
            MoveDefinition[] newestMoves = new MoveDefinition[takeCount];

            // Copy the last 'takeCount' moves directly
            for (int i = 0; i < takeCount; i++)
            {
                newestMoves[i] = levelUpMoves[levelUpMoves.Length - takeCount + i].MoveDefinition;
            }

            return newestMoves;
        }
    }
}
