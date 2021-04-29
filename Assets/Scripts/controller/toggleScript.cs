using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using manager;
using UnityEngine.UI;

public class toggleScript : MonoBehaviour
{

    Firebase.Auth.FirebaseUser currentUser;
    UserManager userManager;
    bool toggle;
    public GameObject Toggle;
    // Start is called before the first frame update
    void Start()
    {
        currentUser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        userManager = new UserManager();
        setdata();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void emailToggle(bool tog)
    {
        var userID = currentUser.UserId;
        toggle = !toggle;
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
        FirebaseDatabase.DefaultInstance.GetReference("User/"+userID+"/enable_email").SetValueAsync(toggle);       
    }

    public async void setdata()
    {
        var user = await userManager.GetCurrentUserFromDB();
        toggle = user.enable_email;
        Toggle.GetComponent<Toggle>().isOn=toggle;
    }

    
}
