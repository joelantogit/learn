using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using UnityEngine.EventSystems;

public class playcontroller : MonoBehaviour
{
    public int panCount  = 5; 
    public GameObject scrollbar;
    public GameObject panPrefab;
    public GameObject worldname;
    
    public EventSystem eventSystem;
    public GameObject currentobj;

    string worldselcted = "world 1";
    int currentworld = 3;

    public worldmanager worldmanager;

    // Start is called before the first frame update
    void Start()
    {
        //var arr = worldmanager.GetWorldlist();
        
        //Debug.LogWarning(arr);
        for(int i = 1; i <= panCount ; i++)
        {
            if(i > currentworld)
            {
                worldname.GetComponent<Button>().interactable = false;
            }
            worldname.transform.GetChild(0).GetComponent<Text>().text = "world " + i ;
            Instantiate(panPrefab, transform, false);
    
        }
        Destroy(panPrefab);
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
