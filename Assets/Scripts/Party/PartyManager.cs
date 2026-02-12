using System;
using System.Collections.Generic;
using System.Linq;
using MonsterTamer.Monster;
using MonsterTamer.Utilities;

namespace MonsterTamer.Party
{
    /// <summary>
    /// Manages a player's monster party at runtime.
    /// Tracks party state, selection, and supports battle-related operations
    /// such as swapping, healing, and restoring original order.
    /// Raises events when the party changes.
    /// </summary>
    internal sealed class PartyManager
    {
        public const int MaxPartySize = 6;

        private readonly List<MonsterInstance> members = new();
        private List<MonsterInstance> originalPartyOrder;

        /// <summary>
        /// The currently selected monster in the party.
        /// </summary>
        internal MonsterInstance SelectedMonster { get; private set; }

        /// <summary>
        /// The index of the currently selected monster, or -1 if none is selected.
        /// </summary>
        public int SelectedIndex => SelectedMonster != null ? members.IndexOf(SelectedMonster) : -1;

        /// <summary>
        /// Read-only list of all monsters in the party.
        /// </summary>
        public IReadOnlyList<MonsterInstance> Members => members;

        /// <summary>
        /// Raised whenever the party composition changes.
        /// </summary>
        public event Action PartyChanged;

        /// <summary>
        /// Initializes the party manager with a predefined party definition.
        /// </summary>
        /// <param name="partyDefinition">The party definition containing initial monsters and levels.</param>
        public PartyManager(PartyDefinition partyDefinition)
        {
            InitializeParty(partyDefinition);
        }

        private void InitializeParty(PartyDefinition partyDefinition)
        {
            if (partyDefinition == null || partyDefinition.Members.Count == 0)
            {
                Log.Warning(nameof(PartyManager), "No initial party members defined.");
                return;
            }

            foreach (var entry in partyDefinition.Members)
            {
                if (entry.MonsterDefinition == null)
                {
                    continue;
                }

                var monster = MonsterFactory.Create(
                    entry.Level,
                    entry.MonsterDefinition
                );

                AddMonster(monster);
            }

            if (members.Count > 0)
            {
                SelectedMonster = members[0];
            }
        }

        /// <summary>
        /// Adds a monster to the party if there is space.
        /// </summary>
        /// <param name="monster">The monster instance to add.</param>
        public void AddMonster(MonsterInstance monster)
        {
            if (monster == null)
            {
                return;
            }

            if (members.Count >= MaxPartySize)
            {
                Log.Warning(nameof(PartyManager),
                    $"Cannot add more than {MaxPartySize} Monster.");
                return;
            }

            members.Add(monster);
            PartyChanged?.Invoke();
        }

        /// <summary>
        /// Saves the current party order for later restoration.
        /// </summary>
        public void SaveOriginalPartyOrder()
        {
            originalPartyOrder = new List<MonsterInstance>(members);
        }

        /// <summary>
        /// Restores the party to the previously saved order.
        /// </summary>
        public void RestorePartyOrder()
        {
            if (originalPartyOrder == null)
            {
                return;
            }

            members.Clear();
            members.AddRange(originalPartyOrder);

            PartyChanged?.Invoke();
        }

        /// <summary>
        /// Swaps two monsters in the party by their indices.
        /// </summary>
        /// <param name="indexA">Index of the first monster.</param>
        /// <param name="indexB">Index of the second monster.</param>
        /// <returns>True if the swap was successful; false otherwise.</returns>
        public bool Swap(int indexA, int indexB)
        {
            if (indexA < 0 || indexA >= members.Count ||
                indexB < 0 || indexB >= members.Count ||
                indexA == indexB)
            {
                return false;
            }

            (members[indexA], members[indexB]) = (members[indexB], members[indexA]);
            PartyChanged?.Invoke();
            return true;
        }

        /// <summary>
        /// Selects the monster at the given slot index.
        /// </summary>
        /// <param name="index">Index of the monster to select.</param>
        public void SelectSlotIndex(int index)
        {
            if (index < 0 || index >= members.Count)
            {
                return;
            }

            SelectedMonster = members[index];
        }

        public MonsterInstance GetFirstUsablePokemon()
        {
            // The Party class is the "expert" on its own members
            return Members.FirstOrDefault(m => m.Health.CurrentHealth > 0);
        }

        /// <summary>
        /// Fully restores health for all monsters in the party.
        /// </summary>
        public void HealAll()
        {
            foreach (var monster in members)
            {
                monster.Health.RestoreFullHealth();
            }
        }

        /// <summary>
        /// Checks whether any monsters in the party have remaining health.
        /// </summary>
        /// <returns>True if at least one monster can fight; false otherwise.</returns>
        public bool HasUsablePokemon()
        {
            foreach (var monster in members)
            {
                if (monster.Health.CurrentHealth > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
