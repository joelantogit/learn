using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class challenge : MonoBehaviour
{
    // Start is called before the first frame update
    List <string> playerlist = new List<string>(); //list of users
    int worldnum = 2;
    string selectedplayer ;

    void Start()
    {
        getPlayerList(worldnum);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void onSelect(string player)
    {
        selectedplayer = player;
    }

    void inviteplayer()
    {
        //send notification to selected player
    }

    void getPlayerList(int worldnum)
    {

    }
}
