using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using entity;
using manager;
using Newtonsoft.Json;


public class PlayManager
{

    public  Dictionary<string, string> challenges;
    Firebase.Auth.FirebaseUser currentUser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
    private UserLevelData userLevelData;

    DatabaseReference challenge_reference = FirebaseDatabase.DefaultInstance.GetReference("Challenge_scores");

    public async Dictionary<string, string> getChallenges()
    {

        var data = await challenge_reference.Child(currentUser.UserId).GetValueAsync();
        foreach(var challenge in data.Children)
        {
            string str = challenge.GetRawJsonValue();
            userLevelData =  JsonConvert.DeserializeObject<UserLevelData>(str);
            challenges.Add(challenge.Key, userLevelData.level);
        }
        return challenges;
    }
}
