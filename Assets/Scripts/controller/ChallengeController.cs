using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChallengeController : MonoBehaviour
{
    public int panCount  = 5; 
    public GameObject scrollbar;
    public GameObject panPrefab;
    public GameObject worldname;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= panCount ; i++)
        {
            worldname.transform.GetChild(0).GetComponent<Text>().text = "user " + i ;
            worldname.transform.GetChild(1).GetComponent<Text>().text = "level " + i ;
            worldname.transform.GetChild(2).GetComponent<Text>().text = "points " + i ;
            Instantiate(panPrefab, transform, false);
    
        }
        Destroy(panPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
