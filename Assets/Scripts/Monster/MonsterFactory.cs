using System;
using System.Linq;
using MonsterTamer.Move;
using MonsterTamer.Move.Models;
using MonsterTamer.Nature;
using MonsterTamer.Utilities;

namespace MonsterTamer.Monster
{
    /// <summary>
    /// Factory for generating Monster instances. 
    /// Handles randomization for wild encounters and initial move-set selection.
    /// </summary>
    internal static class MonsterFactory
    {
        private const int MaxMoves = 4;

        /// <summary>
        /// Creates a Monster with randomized nature and the best moves for its level.
        /// </summary>
        public static MonsterInstance Create(int level, MonsterDefinition definition)
        {
            if (definition == null)
            {
                Log.Error(nameof(MonsterFactory), "Generation failed: MonsterDefinition is null.");
                return null;
            }

            NatureDefinition nature = definition.PossibleNatures.GetRandomNature();
            MoveDefinition[] moves = GetQualifiedMoves(definition.LevelUpMoves, level);

            return new MonsterInstance(level, definition, nature, moves);
        }

        private static MoveDefinition[] GetQualifiedMoves(LevelUpMove[] learnset, int currentLevel)
        {
            if (learnset == null || learnset.Length == 0)
            {
                return Array.Empty<MoveDefinition>();
            }

            // 1. Filter moves by level requirement
            // 2. Sort by level descending (most recent first)
            // 3. Take the top 4 and convert to MoveDefinitions
            return learnset
                .Where(m => m.Level <= currentLevel)
                .OrderByDescending(m => m.Level)
                .Take(MaxMoves)
                .Select(m => m.MoveDefinition)
                .ToArray();
        }
    }
}