using System;


namespace entity
{
    [Serializable]
    public class UserLevelData
    {

        public World world { get; set; }

    }

    [Serializable]
    public class Level
    {
        public int attempts { get; set; }
        public int points { get; set; }
    }

    [Serializable]
    public class World
    {
        public Level level { get; set; }
    }

}
