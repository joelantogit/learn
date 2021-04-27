using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;
public class SubWorldController : MonoBehaviour
{
    public GameObject levelTemplate;
    public GameObject worldname;

    public GameObject scrollbar;

    public EventSystem eventSystem;
    public GameObject currentobj;
    string levelselected;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g ;
        for (int i = 1; i < 5 ; i++)
        {
            g = Instantiate(levelTemplate , transform);
            worldname.GetComponent<Text>().text = " Level" + i ;
            
        }

        // Destroy(levelTemplate);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        currentobj = eventSystem.currentSelectedGameObject;
        GameObject obj = currentobj.transform.parent.gameObject;
        levelselected = obj.transform.GetChild(0).GetComponent<Text>().text;
        // currentobj.GetComponent<Button>().material.color = Color.green;
        
        Debug.LogWarning(levelselected);
            
   
        
    
        
    }
}
