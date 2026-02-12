using MonsterTamer.Monster.Models;

namespace MonsterTamer.Monster.Components
{
    /// <summary>
    /// Manages the lifecycle of a Monster's stats, including IV/EV storage and recalculation.
    /// </summary>
    internal sealed class StatsComponent
    {
        private readonly MonsterInstance monster;

        internal MonsterStats IV { get; }
        internal MonsterStats EV { get; }
        internal MonsterStats Core { get; private set; }

        internal StatsComponent(MonsterInstance monster)
        {
            this.monster = monster;

            IV = StatsCalculator.GenerateRandomIVs();
            EV = new MonsterStats(0, 0, 0, 0, 0, 0); // Initializing empty EVs

            Refresh();
        }

        /// <summary>
        /// Recalculates core stats based on current level and nature. 
        /// Call this after leveling up or modifying EVs.
        /// </summary>
        public void Refresh()
        {
            Core = StatsCalculator.CalculateCoreStats(
                monster.Definition,
                IV,
                EV,
                monster.Experience.Level,
                monster.Nature.Definition
            );
        }
    }
}