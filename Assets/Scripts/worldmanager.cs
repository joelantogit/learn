using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;
public class worldmanager : MonoBehaviour
{
    public GameObject buttonTemplate;
    public GameObject worldname;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject buttonTemplate = transform.GetChild (0).gameobject;
        GameObject g ;
        for (int i = 0; i < 5 ; i++)
        {
            g = Instantiate(buttonTemplate , transform);
            worldname.GetComponent<Text>().text = " testing" + i ;
        }

        Destroy(buttonTemplate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
