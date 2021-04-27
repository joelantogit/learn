using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;

public class WorldsController: MonoBehaviour
{
    // Start is called before the first frame update
    public int panCount  = 5; 
    [Header("Other Objects")]
    public GameObject scrollbar;
    [Header("Others")]
    public GameObject panPrefab;
    
    public GameObject worldname;

    private GameObject[] instPans;

    
    
    float scroll_pos = 0;
    float[] pos ;
    
    void Start()
    {
        for(int i = 1; i <= panCount ; i++)
        {
            worldname.transform.GetChild(0).GetComponent<Text>().text = "world " + i ;
            Instantiate(panPrefab, transform, false);
            
            // GetComponent<Text>().text = " testing " + i ;
            // Text testmsg = transform.GetChild(i).GetComponent<Text>();
            // // Text testmsg = GameObject.Find("Canvas/Scroll View/Viewport/Content/world(Clone)/Text").GetComponent<Text>();
            // testmsg.text = "Testing " + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }


        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                Debug.LogWarning("Current Selected Level" + i);
                SaveSelectedWorld(i);
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j!=i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }


        
    }

    public void ChangeScene(string newscene)
    {
        Application.LoadLevel(newscene);
    }

    private void SaveSelectedWorld(int number)
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
        FirebaseDatabase.DefaultInstance.GetReference("User/"+userID+"/worldselected").SetValueAsync(number);  
    }
}
