using System;
using UnityEngine;

namespace MonsterTamer.Monster.Components
{
    /// <summary>
    /// Manages health state and applies damage or healing.
    /// Notifies listeners when health changes.
    /// </summary>
    internal sealed class HealthComponent
    {
        internal int MaxHealth { get; }
        internal int CurrentHealth { get; private set; }

        /// <summary>
        /// Invoked when health changes.
        /// Parameters: previous HP, new HP.
        /// </summary>
        internal event Action<int, int> HealthChanged;

        /// <summary>
        /// Creates a new health component initialized at full health.
        /// </summary>
        internal HealthComponent(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        /// <summary>
        /// Applies damage (clamped at zero) and returns the actual damage dealt.
        /// </summary>
        internal int TakeDamage(int amount)
        {
            if (amount <= 0 || CurrentHealth <= 0)
            {
                return 0;
            }

            int oldHp = CurrentHealth;
            CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);

            HealthChanged?.Invoke(oldHp, CurrentHealth);
            return oldHp - CurrentHealth;
        }

        /// <summary>
        /// Restores health (clamped to MaxHealth) and returns the actual amount healed.
        /// </summary>
        internal int Heal(int amount)
        {
            if (amount <= 0 || CurrentHealth >= MaxHealth)
            {
                return 0;
            }

            int oldHp = CurrentHealth;
            CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);

            HealthChanged?.Invoke(oldHp, CurrentHealth);
            return CurrentHealth - oldHp;
        }

        internal void RestoreFullHealth()
        {
            if (CurrentHealth == MaxHealth)
            {
                return;
            }

            int oldHp = CurrentHealth;
            CurrentHealth = MaxHealth;

            HealthChanged?.Invoke(oldHp, CurrentHealth);
        }
    }
}
