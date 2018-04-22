using System.Collections.Generic;

namespace DefaultNamespace
{
    public static class GameSettings
    {
        private static readonly List<BaseDifficultySettings> Difficulties = new List<BaseDifficultySettings>()
        {
            new EasyDifficulty(),
            new MediumDifficulty(),
            new HardDifficulty()
        };

        public static int CurrentDifficulty = 0;

        public static BaseDifficultySettings DifficultySettings
        {
            get
            {
                return Difficulties[CurrentDifficulty];
            }
        }
    }
}