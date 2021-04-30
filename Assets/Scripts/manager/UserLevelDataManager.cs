using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using Random=UnityEngine.Random;
using manager;
using entity;
using Newtonsoft.Json;
using System.Linq;

public class UserLevelDataManager : MonoBehaviour
{


public string level = "";
public string world = "";
public string masterlevel ="";
public int mastervalue =0;
public int score=0;
public int currentscore =0;
public int newscore =0;
public int attempt =0;
List<string> levelslist = new List<string>();
List<string> userworldslist = new List<string>();
List<string> Allevels = new List<string>();
List<int> Allevelcount = new List<int>();
Dictionary<string,int> masterleveldict= new Dictionary<string,int>();
//Dictionary<string,int> mymasterleveldict = new Dictionary<string,int>();

    public async Task<Dictionary<string,int>> getUserLevelData()
    {   
        DatabaseReference user_reference = FirebaseDatabase.DefaultInstance.GetReference("UserLevelData");
        //var users = new List<UserLevelData>();
        Task<DataSnapshot> task = user_reference.GetValueAsync();
        DataSnapshot snapshot = await task;  
        foreach(var _users in snapshot.Children)
            {
                Debug.Log(_users);

                foreach( var _leveldata in _users.Children)
                {
                    Debug.Log(_leveldata);
                    level = _leveldata.Child("level").Value.ToString();
                    Debug.Log(level);
                    levelslist.Add(level);
            }
            }
            Debug.Log(level);
            Debug.Log(levelslist);
            foreach( var i in levelslist)
                {
                    Debug.Log(i);
                }
                    var levelcounts =  levelslist.GroupBy(s => s)
                    .ToDictionary(g => g.Key, g => g.Count());
        return levelcounts;
    }

    public async Task<Dictionary<string,int>> getUserWorldData()
    {   
        DatabaseReference user_reference = FirebaseDatabase.DefaultInstance.GetReference("UserLevelData");
        //var users = new List<UserLevelData>();
        Task<DataSnapshot> task = user_reference.GetValueAsync();
        DataSnapshot snapshot = await task;  
        foreach(var _users in snapshot.Children)
            {
                Debug.Log(_users);

                foreach( var _worlddata in _users.Children)
                {
                    Debug.Log(_worlddata);
                    world = _worlddata.Child("world").Value.ToString();
                    Debug.Log(world);
                    userworldslist.Add(world);
                    userworldslist.Select(x => x).Distinct().ToList();
                    Debug.Log(userworldslist.ToString());
                }
            Debug.Log("indivudal user list");
            Debug.Log(world);
            Debug.Log(userworldslist);
            foreach( var i in userworldslist)
                {
                    Debug.Log(i);
                }
            }
        var worldcounts =  userworldslist.GroupBy(s => s)
        .ToDictionary(g => g.Key, g => g.Count());
            
        return worldcounts;
    }


    public async Task<Dictionary<string,int>> getUserMasteredData()
    {   
        DatabaseReference user_reference = FirebaseDatabase.DefaultInstance.GetReference("UserLevelData");
        //var users = new List<UserLevelData>();
        Task<DataSnapshot> task = user_reference.GetValueAsync();
        DataSnapshot snapshot = await task;  
        foreach(var _users in snapshot.Children)
            {
                Debug.Log(_users);

                foreach( var _leveldata in _users.Children)
                {
                    Debug.Log(_leveldata);
                    masterlevel = _leveldata.Child("level").Value.ToString();
                    attempt = Convert.ToInt32(_leveldata.Child("attempts").Value);
                    //Convert.ToInt32(Object)
                    score = Convert.ToInt32(_leveldata.Child("points").Value);
                    mastervalue = score - attempt;
                    //totalmastervalue = totalmastervalue + mastervalue;

                    Debug.Log(masterlevel);
                    Debug.Log(attempt);
                    Debug.Log(mastervalue);
                    //Debug.Log(totalmastervalue);
                    if (masterleveldict.ContainsKey(masterlevel))
                    {
                        currentscore = masterleveldict[masterlevel];
                        Debug.Log(currentscore);
                        Debug.Log(mastervalue);
                        newscore = currentscore + mastervalue;
                        Debug.Log(newscore);
                        masterleveldict[masterlevel]= newscore;
                        //masterleveldict.OrderBy(key => key.Value);
                    }
                    else {
                        masterleveldict[masterlevel]= mastervalue;
                        //masterleveldict.OrderBy(key => key.Value);
                    }
                }
       
            }
            //mymasterleveldict = masterleveldict.OrderBy(key => key.Value);
    
     return masterleveldict;
    }
}


