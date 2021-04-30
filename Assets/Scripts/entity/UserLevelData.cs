using System;


namespace entity
{
    [Serializable]
    public class UserLevelData
    {

        public int attempts { get; set; }
        public string level { get; set; }
        public int points { get; set; }
        public string world { get; set; }

        public UserLevelData()
        {
            
        }

        public UserLevelData(string world, string level)
        {
            this.world = world;
            this.level = level;
        }

    }

  
    

}
