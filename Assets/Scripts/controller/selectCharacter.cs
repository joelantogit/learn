using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class selectCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public string character = "Male1";
    public static string savedCharacter = "Male1";
    private string userID = "1000";
    Firebase.Auth.FirebaseUser currentUser;
    void Start()
    {
        currentUser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void selectMale1()
    {
        character = "Male1";
        print(character);
    }

    public void selectMale2()
    {
        character = "Male2";
        print(character);
    }

    public void selectMale3()
    {
        character = "Male3";
        print(character);
    }
    public void selectFemale1()
    {
        character = "Female1";
        print(character);
    }

    public void selectFemale2()
    {
        character = "Female2";
        print(character);
    }

    public void selectFemale3()
    {
        character = "Female3";
        print(character);
    }

    public void sendCharacter()
    {
        savedCharacter = character;
        userID = currentUser.UserId.ToString();
        
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
        FirebaseDatabase.DefaultInstance.GetReference("User/"+userID+"/character").SetValueAsync(savedCharacter);
    }
}
