using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using UnityEngine.EventSystems;
using manager;

public class inboxController : MonoBehaviour
{
    public GameObject challengetemplate;
    public GameObject template;
    public EventSystem eventSystem;
    public GameObject currentobj;
    private int challengecount = 3;
    private string challengeselected = "";
    Firebase.Auth.FirebaseUser currentUser;
    private Dictionary<string, string> challenges;
    private PlayManager playManager;
    // Start is called before the first frame update
    void Start()
    {
        playManager = new PlayManager();
        challenges = new Dictionary<string, string>();
        currentUser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        
        setdata();
        

    }

    private async void setdata()
    {
        challenges = await playManager.getChallenges();
        foreach (KeyValuePair<string, string> entry in challenges)
        {
            Debug.Log("challenge id - " + entry.Key + "level - " + entry.Value);

        }
        challengecount = challenges.Count;
        populatelist();
    }

    // Update is called once per frame
    void Update()
    {
        if(eventSystem.currentSelectedGameObject != null)
        {
            currentobj = eventSystem.currentSelectedGameObject;
            if (currentobj != null )
            {
                Debug.LogWarning("hello");
                GameObject obj = currentobj.transform.parent.gameObject;
                try
                {
                    challengeselected = obj.transform.GetChild(0).GetComponent<Text>().text;
                }
                catch
                {

                }
                
                // currentobj.GetComponent<Button>().material.color = Color.green;
                //SaveSelectedLevel(challengeselected);
                Debug.LogWarning(challengeselected);
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
        FirebaseDatabase.DefaultInstance.GetReference("User/"+userID+"/challengeselected").SetValueAsync(number);  
    }



    void populatelist()
    {
        foreach (KeyValuePair<string, string> entry in challenges)
        {
            template.transform.GetChild(0).GetComponent<Text>().text = "Challenge " ;//update with worldnames
            template.transform.GetChild(1).GetComponent<Text>().text = entry.Value;

            Instantiate(challengetemplate, transform, false);

        }
        //for (int i = 0; i < challengecount; i++)
        //{
            
        //    template.transform.GetChild(0).GetComponent<Text>().text = challenges[ ;//update with worldnames
        //    template.transform.GetChild(1).GetComponent<Text>().text = "points " + i;
            
        //    Instantiate(challengetemplate, transform, false);

        //}
        Destroy(challengetemplate);
    }
}
