using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class worlds : MonoBehaviour

{
    List <string> worldlist = new List<string>(); // list of worlds

    public void changescene(string scenename)
    {
        Application.LoadLevel(scenename);
        
    }

    // Start is called before the first frame update
    void StartW()
    {
        //get world list and display
        checkAccess("john");
        //get 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkAccess(string username)
    {
        //to check which levels user can access
    }
}
