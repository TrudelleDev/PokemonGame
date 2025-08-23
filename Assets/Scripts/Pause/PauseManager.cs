using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Pause
{
    public static class PauseManager
    {
        public static event Action<bool> OnPauseStateChanged;

        public static bool IsPaused { get; private set; }

        public static void SetPaused(bool paused)
        {
            if (IsPaused == paused)
                return;

            IsPaused = paused;
            OnPauseStateChanged?.Invoke(paused);
        }
    }
}
