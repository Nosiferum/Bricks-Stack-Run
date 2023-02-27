using System;

namespace DogukanKarabiyik.BricksStackRun.Managers
{
    public static class GameManager
    {
        public static Action OnLevelStart;
        public static Action OnLevelOver;

        public static Action OnLevelSuccess;
        public static Action OnLevelFail;

        public static void GameStart()
        {
            OnLevelStart?.Invoke();
        }

        public static void GameFail()
        {
            OnLevelOver?.Invoke();
            OnLevelFail?.Invoke();
        }

        public static void GameSuccess()
        {
            OnLevelOver?.Invoke();
            OnLevelSuccess?.Invoke();
        }
    }
}


