using System;

namespace PokemonClone.Manager
{
    public class GameStateManager
    {
        private static GameStateManager instance;
        private GameState currentGameState = GameState.Resume;

        public event Action<GameState> OnGameStateChange;

        public static GameStateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameStateManager();
                }
                   
                return instance;
            }
        }

        public void SetState(GameState newState)
        {
            if (currentGameState != newState)
            {
                currentGameState = newState;
                OnGameStateChange?.Invoke(newState);
            }
        }
    }
}
