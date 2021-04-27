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
    List<string> list = new List<string>();
    string[] arr;



    public async Task<string[]> GetWorldlist()
    {
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
                for(int i = 1; i<= totalChildren ; i++)
                {
                    Debug.LogWarning("here for loop");
                    string wrld = i.ToString();
                    Debug.LogWarning(wrld);
                    
                    Debug.LogWarning(snapshot.Child(wrld).Child("worldname").Value.ToString());
                    list.Add(snapshot.Child(wrld).Child("worldname").Value.ToString());
                    
                }  
            }      
        }
        );
        arr = list.ToArray();
        Debug.LogWarning(arr[0] + arr[1] + arr[2]);
        
        return arr;
    }
    public string[] returnlist()
    {
        return arr;
    }

    public void GetLevellist(string worldnum)
    {
        string[] arr;

         FirebaseDatabase.DefaultInstance      
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
                for(int i = 1; i<= totalChildren ; i++)
                {
                    Debug.LogWarning("here for loop");
                    string lvl = i.ToString();
                    Debug.LogWarning(lvl);
                    
                    Debug.LogWarning(snapshot.Child(lvl).Child("levelname").Value.ToString());
                    list.Add(snapshot.Child(lvl).Child("levelname").Value.ToString());
                    
                }
                arr = list.ToArray();
                Debug.LogWarning(arr[0]);
                
            }      
        }
        );
        
    }

}
