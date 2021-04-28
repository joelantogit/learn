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

    }

    

}
