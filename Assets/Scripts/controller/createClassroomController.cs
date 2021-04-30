using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class createClassroomController : MonoBehaviour
{
    public InputField ClassroomName;
    //public InputField passwordInputField;
    public Button Submit;
    public Text infoText;
    // bool inputFieldIsSet = false;

    // Start is called before the first frame update
    void Start()
    {
        Submit.enabled = false;
        infoText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(string.IsNullOrEmpty(ClassroomName.text)))
        {
            Submit.enabled = true;
        }
    }

    public void giveTeacherAcess()
    {
    string Tr_username = ClassroomName.text.ToString();
    AddToInformation("The user " + Tr_username + " is successfully credited with teacher access");
    }
    private void AddToInformation(string str) { infoText.text += str; }
}
// if(!(string.IsNullOrEmpty(usernameInputField.text)) && !(string.IsNullOrEmpty(passwordInputField.text)))
// inputFieldIsSet = true;
// string pass = passwordInputField.text;