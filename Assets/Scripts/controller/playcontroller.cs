using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using UnityEngine.EventSystems;

public class playcontroller : MonoBehaviour
{
    public int panCount  = 5; // need to replace with worldlist
    public GameObject scrollbar;
    public GameObject panPrefab;
    public GameObject worldname;
    
    public EventSystem eventSystem;
    public GameObject currentobj;

    string worldselcted = "world 1";
    int currentworld = 3;
    List<string> worlds,levels;

    public worldmanager worldmanager;

    // Start is called before the first frame update
    void Start()
    {
        worldmanager = new worldmanager();
        PopulateWorlds();
        
        

        

        //Debug.LogWarning(arr);

        //panCount = arr.Length;
        //for (int i = 1; i <= panCount; i++)
        //{
        //    if (i > currentworld)//need to get currentworld from user 
        //    {
        //        worldname.GetComponent<Button>().enabled = false;
        //    }
        //    worldname.transform.GetChild(0).GetComponent<Text>().text = "world " + i;//update with worldnames
        //    Instantiate(panPrefab, transform, false);

        //}
        //Destroy(panPrefab);
    }


    public async void PopulateWorlds()
    {
        worlds = new List<string>();
        var reply = await worldmanager.GetWorldlist();
        foreach(var world in reply)
        {
            worlds.Add(world);
        }
        
        panCount = worlds.Count;
        for (int i = 0; i < panCount; i++)
        {
            if (i+1 > currentworld)//need to get currentworld from user 
            {
                worldname.GetComponent<Button>().enabled = false;
            }
            worldname.transform.GetChild(0).GetComponent<Text>().text = worlds[i];//update with worldnames
            Instantiate(panPrefab, transform, false);

        }
        Destroy(panPrefab);
        print("This is from populate world" + worlds[0]);
        PopulateLevels(worlds[0]);


    }

    public async void PopulateLevels(string worldname)
    {
        levels = new List<string>();
        var reply = await worldmanager.GetLevellist(worldname);
        foreach(var level in reply)
        {
            levels.Add(level);
        }
        Debug.Log(levels[0]);
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
        userID = "8yoi7DcFUwN5sWDqO8LQDmlFtBh2";
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
