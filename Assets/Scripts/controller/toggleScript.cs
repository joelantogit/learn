using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class toggleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void emailToggle(bool tog)
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
        FirebaseDatabase.DefaultInstance.GetReference("User/"+userID+"/enable_email").SetValueAsync(tog);       
    }

    
}
