﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using System;
using manager;
using entity;



public class LeaderBoardControllerList : MonoBehaviour{

    public GameObject rowPrefab;
    public Transform rowsParent;
    private UserManager userManager;
    private List<Dictionary<string, int>> leaderBoard;


    public int totalChildren;
    public string userID1 = "8yoi7DcFUwN5sWDqO8LQDmlFtBh2"; 
    //public String userID2 = "vlcP7MmerUYrbds2RuiC7oLY5bn1";
    public string userName1;
    public int totalPoint1;
    //public String userName2 = "";
    //public Int totalPoint2;
    //var userID = new List<string>(){};


    public void displayOnLeaderBoard(int totalChildren){

        for (int i = 0; i < totalChildren; i++){
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            //texts[0].text = i.ToString();
            texts[0].text = "00";
            //texts[1].text = userName1;
            texts[1].text = "htetnay";
            //texts[2].text = totalPoint1.ToString();
            texts[2].text = "99";
            //print (userName1);
            //print (totalPoint1);
            print ("button is working");
            print (totalChildren);
        }

    }

    public void getUserpointslist()
    {
        var users = userManager.GetAllUsersList();
        foreach(var user in users.Result)   
        {
            print("user name is" + user.name);
            leaderBoard.Add(new Dictionary<string, int> { [user.name] = user.total_points });
        }


    }

    public void getTableData(){
    FirebaseDatabase.DefaultInstance
        .GetReference("User")
        .GetValueAsync().ContinueWith(task => 
        {        
            if (task.IsFaulted) 
            {
                print("Error");  // Handle the error...                      
            }     
            else if (task.IsCompleted) 
            {          
                DataSnapshot snapshot = task.Result;          // Do something with snapshot...       
                Debug.Log (userID1);
                userName1  = snapshot.Child(userID1).Child("name").Value.ToString(); 
                totalPoint1 = Convert.ToInt32(snapshot.Child(userID1).Child("total_points").Value.ToString());
                print (userName1);
                print (totalPoint1);
            }      
        }
        );
    }
    public void getData(){        
    FirebaseDatabase.DefaultInstance      
        .GetReference("User")      
        .GetValueAsync().ContinueWith(task => 
        {        
            if (task.IsFaulted) 
            {
                print("Error");  // Handle the error...                      
            }        
            else if (task.IsCompleted) 
            {          
                DataSnapshot snapshot = task.Result;          // Do something with snapshot... 
                totalChildren = (int)task.Result.ChildrenCount;   
                print (totalChildren);   
            }      
        }
        );
        //FirebaseDatabase.DefaultInstance.GetReference("User/"+userID+"/character").SetValueAsync(savedCharacter);
    }

    // Start is called before the first frame update
    void Start()
    {
        getData();
        getTableData();
        displayOnLeaderBoard(totalChildren);
        userManager = new UserManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}