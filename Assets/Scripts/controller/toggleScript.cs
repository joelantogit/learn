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
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        print(reference.Child("World1").Child("Addition & Subtraction").Child("question1").Child("question").Value.ToString()); 
        print(tog);        
    }
}
