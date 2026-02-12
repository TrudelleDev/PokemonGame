using System;
using UnityEngine;

namespace MonsterTamer.Monster.Components
{
    /// <summary>
    /// Manages experience accumulation and level progression
    /// using a total-experience model.
    /// </summary>
    internal sealed class ExperienceComponent
    {
        internal int Level { get; private set; }
        internal int TotalExperience { get; private set; }

        /// <summary>
        /// Invoked when total experience changes.
        /// Parameters: previous EXP, new EXP.
        /// </summary>
        internal event Action<int, int> ExperienceChanged;

        /// <summary>
        /// Invoked when the monster levels up.
        /// Parameter: new level.
        /// </summary>
        internal event Action<int> LevelChanged;

        /// <summary>
        /// Creates a new experience component starting at the given level.
        /// Total experience is initialized to the minimum required for that level.
        /// </summary>
        internal ExperienceComponent(int startingLevel)
        {
            Level = startingLevel;
            TotalExperience = GetExpForCurrentLevel();
        }

        /// <summary>
        /// Adds experience and processes all pending level-ups.
        /// </summary>
        internal void AddExperience(int amount)
        {
            int oldExp = TotalExperience;
            TotalExperience += amount;

            ExperienceChanged?.Invoke(oldExp, TotalExperience);

            while (TotalExperience >= GetExpForNextLevel())
            {
                Level++;
                LevelChanged?.Invoke(Level);
            }
        }

        /// <summary>
        /// Calculates EXP gained from defeating an opponent
        /// using a simple level-based formula.
        /// </summary>
        internal int CalculateExpGain(MonsterInstance opponent)
        {
            const float LevelMultiplier = 5f;
            const float BaseReward = 10f;

            return Mathf.RoundToInt(
                opponent.Experience.Level * LevelMultiplier + BaseReward
            );
        }

        internal int GetExpForCurrentLevel()
        {
            return Mathf.FloorToInt(Mathf.Pow(Level, 3));
        }

        internal int GetExpForNextLevel()
        {
            return Mathf.FloorToInt(Mathf.Pow(Level + 1, 3));
        }
    }
}
