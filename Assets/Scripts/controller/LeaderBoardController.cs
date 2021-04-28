using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LeaderBoardController : MonoBehaviour
{
    public GameObject LeaderBoard1;
    public GameObject LeaderBoard2_World1;
    public GameObject LeaderBoard2_World2;
    public GameObject LeaderBoard2_World3;
    public GameObject LeaderBoard3;
    public Button mainButton;
    //public Button worldButton;
    public Button classroomButton;
    //protected World_dropdown();

    // Start is called before the first frame update
    void Start()
    {
        LeaderBoard1 = GameObject.Find ("MainLeaderBoard");
        LeaderBoard2_World1 = GameObject.Find ("World1LeaderBoard");
        LeaderBoard2_World2 = GameObject.Find ("World2LeaderBoard");
        LeaderBoard2_World3 = GameObject.Find ("World3LeaderBoard");
        LeaderBoard3 = GameObject.Find ("ClassroomLeaderBoard");

        LeaderBoard1.SetActive(false);
        LeaderBoard2_World1.SetActive(false);
        LeaderBoard2_World2.SetActive(false);
        LeaderBoard2_World3.SetActive(false);
        LeaderBoard3.SetActive(false);

        mainButton.enabled = true;
        //worldButton.interactable = true;
        classroomButton.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void button1Activate()
    {
        LeaderBoard1.SetActive(true);
        LeaderBoard2_World1.SetActive(false);
        LeaderBoard2_World2.SetActive(false);
        LeaderBoard2_World3.SetActive(false);
        LeaderBoard3.SetActive(false);
    }

    // public void button2Activate()
    // {
    //     LeaderBoard1.SetActive(false);
    //     LeaderBoard2.SetActive(true);
    //     LeaderBoard3.SetActive(false);
    // }

    public void button3Activate()
    {
        LeaderBoard1.SetActive(false);
        LeaderBoard2_World1.SetActive(false);
        LeaderBoard2_World2.SetActive(false);
        LeaderBoard2_World3.SetActive(false);
        LeaderBoard3.SetActive(true);
    }

    public void handleWorldDropDown(int value)
    {
        if (value == 0)
        {
            LeaderBoard1.SetActive(false);
            LeaderBoard2_World1.SetActive(true);
            LeaderBoard2_World2.SetActive(false);
            LeaderBoard2_World3.SetActive(false);
            LeaderBoard3.SetActive(false);
        }
        if (value == 1)
        {
            LeaderBoard1.SetActive(false);
            LeaderBoard2_World1.SetActive(false);
            LeaderBoard2_World2.SetActive(true);
            LeaderBoard2_World3.SetActive(false);
            LeaderBoard3.SetActive(false);
        }
        if (value == 2)
        {
            LeaderBoard1.SetActive(false);
            LeaderBoard2_World1.SetActive(false);
            LeaderBoard2_World2.SetActive(false);
            LeaderBoard2_World3.SetActive(true);
            LeaderBoard3.SetActive(false);
        }
    }

}

//Button1 = Button.Find ("mainButton")
//Button2 = Button.Find ("worldButton")
//Button3 = Button.Find ("classroomButton")
