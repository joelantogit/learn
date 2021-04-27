using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;
using Firebase;
using Firebase.Database;

public class SubWorldController : MonoBehaviour
{
    public GameObject levelTemplate;
    public GameObject worldname;

    public GameObject scrollbar;

    public EventSystem eventSystem;
    public GameObject currentobj;
    string levelselected;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g ;
        for (int i = 1; i < 5 ; i++)
        {
            worldname.GetComponent<Text>().text = " Level " + i ;
            g = Instantiate(levelTemplate , transform);
            
            
        }

        Destroy(levelTemplate);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        currentobj = eventSystem.currentSelectedGameObject;
        GameObject obj = currentobj.transform.parent.gameObject;
        levelselected = obj.transform.GetChild(0).GetComponent<Text>().text;
        // currentobj.GetComponent<Button>().material.color = Color.green;
        SaveSelectedLevel(levelselected);
        Debug.LogWarning(levelselected);
    }

    private void SaveSelectedLevel(string number)
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
                print("The user is already existing in the database " + snapshot.Child(userID).Child("name").Value.ToString()); 
            }      
        }
        );
        FirebaseDatabase.DefaultInstance.GetReference("User/"+userID+"/levelselected").SetValueAsync(number);  
    }
}
