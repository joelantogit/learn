using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField playername;

    public void start_profile()
    {
        Debug.Log("Player Name is:" +playername.text);
        profileInput.playernamestr = playername.text;
        Debug.Log("Profile");
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Player Name is:" + playername.text);
        profileInput.playernamestr = playername.text;
        Debug.Log("Profile");
    }
}
