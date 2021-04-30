using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using UnityEngine.EventSystems;
using manager;

public class playcontroller : MonoBehaviour
{
    public int panCount  = 5; // need to replace with worldlist
    public GameObject scrollbar;
    public GameObject panPrefab;
    public GameObject worldname;
    private UserManager userManager;
    public EventSystem eventSystem;
    public GameObject currentobj;

    string worldselcted = "Basics";
    string currentworld = "Basics";
    List<string> worlds;
    Firebase.Auth.FirebaseUser currentUser;

    public worldmanager worldmanager;

    // Start is called before the first frame update
    void Start()
    {
        worldmanager = new worldmanager();
        userManager = new UserManager();
        currentUser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        PopulateWorlds();
        
        
    }


    public async void PopulateWorlds()
    {
        var user = await userManager.GetCurrentUserFromDB();
        Debug.Log("current user is " + user.name);
        currentworld = user.current_world;

        worlds = new List<string>();
        var reply = await worldmanager.GetWorldlist();
        foreach(var world in reply)
        {
            worlds.Add(world);
        }
        
        panCount = worlds.Count;
        for (int i = 0; i < panCount; i++)
        {
            int index = worlds.IndexOf(worlds[i]);
            int curworldindex = worlds.IndexOf(currentworld);
            if (index > curworldindex)//need to get currentworld from user 
            {
                worldname.GetComponent<Button>().enabled = false;
            }
            worldname.transform.GetChild(0).GetComponent<Text>().text = worlds[i];//update with worldnames
            Instantiate(panPrefab, transform, false);

        }
        Destroy(panPrefab);
        print("This is from populate world" + worlds[0]);
        


    }

   

    // Update is called once per frame
    void Update()
    {
        if(eventSystem.currentSelectedGameObject != null)
        {
            currentobj = eventSystem.currentSelectedGameObject;
            if(currentobj != null)
            {
                worldselcted  = currentobj.transform.GetChild(0).GetComponent<Text>().text;
            // currentobj.GetComponent<Button>().material.color = Color.green;
                SaveSelectedWorld(worldselcted);
                Debug.LogWarning(worldselcted );
            }
        }
        
        
    }

    private void SaveSelectedWorld(string number)
    {
        var userID = "1000";
        userID = currentUser.UserId;
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
                
            }      
        }
        );
        FirebaseDatabase.DefaultInstance.GetReference("User/"+userID+"/worldselected").SetValueAsync(number);  
    }

}
