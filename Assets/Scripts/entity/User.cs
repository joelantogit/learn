using System;


namespace entity
{
    [Serializable]
    public class User
    {

        public string uid { get; set; }
        public string character { get; set; }
        public string current_level { get; set; }
        public string current_world { get; set; }
        public string emailid { get; set; }
        public bool enable_email { get; set; }
        public string levelselected { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public int total_points { get; set; }
        public string worldselected { get; set; }


    }
}


