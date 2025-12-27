using System;
using PokemonGame.Pokemon.Enums;
using UnityEngine;

namespace PokemonGame.Pokemon.Components
{
    public class HealthComponent
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public StatusCondition Condition { get; private set; } = StatusCondition.None;

        public event Action<int, int> HealthChange; // oldHp, newHp
        public event Action<StatusCondition> OnStatusChange;

        public HealthComponent(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public int TakeDamage(int amount)
        {
            if (amount <= 0 || CurrentHealth <= 0) return 0;

            int oldHp = CurrentHealth;
            CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);

            HealthChange?.Invoke(oldHp, CurrentHealth);
            return amount;
        }

        public int Heal(int amount)
        {
            if (amount <= 0 || CurrentHealth >= MaxHealth) return 0;

            int oldHp = CurrentHealth;
            CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);

            HealthChange?.Invoke(oldHp, CurrentHealth);
            return CurrentHealth - oldHp;
        }

        public bool TryCureStatus(StatusCondition status)
        {
            if (Condition != status) return false;

            Condition = StatusCondition.None;
            OnStatusChange?.Invoke(Condition);
            return true;
        }

        public void SetMaxHealth(int maxHealth)
        {
            float healthPercent = (float)CurrentHealth / MaxHealth;
            MaxHealth = maxHealth;
            CurrentHealth = Mathf.RoundToInt(MaxHealth * healthPercent);
        }
    }

}
