using System;
using UnityEngine;

namespace MonsterTamer.Pokemon.Components
{
    internal class ExperienceComponent
    {
        public int Level { get; private set; }
        public int CurrentExp { get; private set; }

        public event Action<int, int> OnExperienceChange; // oldExp, newExp
        public event Action<int> LevelChanged;

        public ExperienceComponent(int level)
        {
            Level = level;
            CurrentExp = GetExpForCurrentLevel();
        }
       
        public void AddExperience(int amount)
        {
            int oldExp = CurrentExp;
            CurrentExp += amount;

            OnExperienceChange?.Invoke(oldExp, CurrentExp);

            while (ShouldLevelUp())
            {
                LevelUp();
            }
        }

        public int CalculateExpGain(PokemonInstance opponent)
        {
            const float LevelMultiplier = 5f;
            const float BaseReward = 10f;

            int exp = Mathf.RoundToInt(opponent.Experience.Level * LevelMultiplier + BaseReward);
            return exp;
        }

        public bool ShouldLevelUp()
        {
            int requiredExp = GetExpForNextLevel();
            return CurrentExp >= requiredExp;
        }

        public void LevelUp()
        {
            Level++;
            LevelChanged?.Invoke(Level);
        }

        public int GetExpForCurrentLevel()
        {
            return Mathf.FloorToInt(Mathf.Pow(Level, 3));
        }

        public int GetExpForNextLevel()
        {
            return Mathf.FloorToInt(Mathf.Pow(Level + 1, 3));
        }
    }
}
