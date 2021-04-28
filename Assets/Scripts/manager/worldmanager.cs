using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;
using System.Threading.Tasks;

public class worldmanager
{
    int totalChildren = 0;
    List<string> worldlist = new List<string>();
    List<string> levellist = new List<string>();
    string[] arr;



    public async Task<List<string>>  GetWorldlist()
    {
        Debug.LogWarning("getting");
        await FirebaseDatabase.DefaultInstance      
        .GetReference("Worlds")      
        .GetValueAsync().ContinueWith(task => 
        {        
            if (task.IsFaulted) 
            {
                //print("Error");  // Handle the error...     
                // arr[0]="error";          
            }        
            else if (task.IsCompleted) 
            {    
                DataSnapshot snapshot = task.Result;          // Do something with snapshot...       
                totalChildren = (int)task.Result.ChildrenCount;
                
                Debug.LogWarning("count " + totalChildren);
                Debug.LogWarning("here");
                Debug.LogWarning("here for loop");
                foreach(var player in snapshot.Children){
                    Debug.Log("key " + player.Key);
                    worldlist.Add(player.Key.ToString());
                }
                
            }      
        }
        );

        return worldlist;
    }
   
    public async Task<List<string>> GetLevellist(string worldnum)
    {
        string[] arr;

        await FirebaseDatabase.DefaultInstance      
        .GetReference("Worlds").Child(worldnum).Child("levels").GetValueAsync().ContinueWith(task => 
        {        
            if (task.IsFaulted) 
            {
                // print("Error");  // Handle the error...    
                // arr[0]="error";                  
            }        
            else if (task.IsCompleted) 
            {          
                DataSnapshot snapshot = task.Result;          // Do something with snapshot...       
                totalChildren = (int)task.Result.ChildrenCount;
                Debug.LogWarning("count " + totalChildren);
                Debug.LogWarning("here");
                foreach(var player in snapshot.Children){
                    Debug.Log("key " + player.Key);
                    levellist.Add(player.Key.ToString());
                }
                arr = levellist.ToArray();
                Debug.LogWarning(arr[0]);
                
            }      
        }
        );
        return levellist;
        
    }

}
