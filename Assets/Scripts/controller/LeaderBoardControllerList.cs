using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using manager;
using entity;
using System.Linq;



public class LeaderBoardControllerList : MonoBehaviour
{
    private UserManager userManager;
    private int usercount = 2;
    public GameObject usertemplate;
    public GameObject template;

    public List <string> nameList;
    public List <int> pointList;
    
    public int totalChildren;
    public string userID1 = "8yoi7DcFUwN5sWDqO8LQDmlFtBh2"; 
    
    public string username = " ";
    public int totalPoints = 0;

    public Dictionary<string, int> orderedList = new Dictionary<string, int>();

    async void getUserData()
    {
        await getUserpointslist();
        await getData();
        await populatelist();
    }
    public async Task getUserpointslist()
    {
        Dictionary<string, int> leaderBoard = new Dictionary<string, int>();
        DatabaseReference user_reference = FirebaseDatabase.DefaultInstance.GetReference("User");
       // var users = new List<UserLevelData>();
        Task<DataSnapshot> task = user_reference.GetValueAsync();
        DataSnapshot snapshot = await task; 
        
        foreach(var _users in snapshot.Children)
        {
            
            username = _users.Child("name").Value.ToString();
            
            totalPoints = Convert.ToInt32(_users.Child("total_points").Value);
            
            leaderBoard.Add(username, totalPoints);
            
            

        }

        //orderedList = leaderBoard.OrderByDescending (x => x.Value).ToList();
        //var sortedDict = from entry in leaderBoard orderby entry.Value ascending select entry;
        
        foreach(var kvp in leaderBoard.OrderByDescending (x => x.Value))
		{
            nameList.Add(kvp.Key);
            pointList.Add(kvp.Value);
        }
        return;
    }

    public async Task getData(){        
    await FirebaseDatabase.DefaultInstance      
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
                totalChildren = (int)task.Result.ChildrenCount;   
                print (totalChildren);   
            }      
        }
        );
        return ;
        //FirebaseDatabase.DefaultInstance.GetReference("User/"+userID+"/character").SetValueAsync(savedCharacter);
    }

    async Task populatelist()
    {
        print(totalChildren);
        for (int i = 0; i < totalChildren; i++)
        {
            print(i);
            
            template.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();//update with worldnames
            template.transform.GetChild(1).GetComponent<Text>().text = nameList[i];
            template.transform.GetChild(2).GetComponent<Text>().text = pointList[i].ToString();
            Instantiate(usertemplate, transform, false);

        }
        Destroy(usertemplate);
        return;
    }
    // Start is called before the first frame update
    void Start()
    {
        userManager = new UserManager();
        getUserData();
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
