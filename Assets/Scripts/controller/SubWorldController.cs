using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;
using Firebase;
using Firebase.Database;
using manager;


public class SubWorldController : MonoBehaviour
{
    public GameObject levelTemplate;
    public GameObject worldname;

    public GameObject scrollbar;

    public EventSystem eventSystem;
    public GameObject currentobj;
    private worldmanager worldmanager;
    private UserManager userManager;
    string levelselected;
    List<string> levels;
    private string currentworld;
    
    // Start is called before the first frame update

    Firebase.Auth.FirebaseUser currentUser;
    void Start()
    {
        worldmanager = new worldmanager();
        userManager = new UserManager();
        currentUser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        setdata();
        
        

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(eventSystem.currentSelectedGameObject != null)
        {
            currentobj = eventSystem.currentSelectedGameObject;
            if (currentobj != null)
            {
                GameObject obj = currentobj.transform.parent.gameObject;
                levelselected = obj.transform.GetChild(0).GetComponent<Text>().text;
                // currentobj.GetComponent<Button>().material.color = Color.green;
                SaveSelectedLevel(levelselected);
                Debug.LogWarning(levelselected);
            }
            
        }
        
    }

    private void SaveSelectedLevel(string number)
    {
        var userID = currentUser.UserId;
        
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

    public async void PopulateLevels(string currentworld)
    {
        levels = new List<string>();
        var reply = await worldmanager.GetLevellist(currentworld);
        foreach (var level in reply)
        {
            levels.Add(level);
        }
        Debug.Log(levels[0]);

        GameObject g;
        for (int i = 0; i < levels.Count; i++)
        {
            worldname.GetComponent<Text>().text = levels[i];
            g = Instantiate(levelTemplate, transform);
           

        }
        Destroy(levelTemplate);
    }

    public async void setdata()
    {
        var user = await userManager.GetCurrentUserFromDB();
        Debug.Log("current user is " + user.name);
        currentworld = user.current_world;
        PopulateLevels(currentworld);



    }
}
