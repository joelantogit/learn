using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class profileInput : MonoBehaviour
{
    // Start is called before the first frame update
    public static string playernamestr;
    public Text playername;
    
    void Start()
    {
        playername.text = playernamestr;
    }
    void Update()
    {

    }
}

