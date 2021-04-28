using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class giveTeacherAccessController : MonoBehaviour
{
    public InputField usernameInputField;
    //public InputField passwordInputField;
    public Button submitButton;
    public Text infoText;
    // bool inputFieldIsSet = false;

    // Start is called before the first frame update
    void Start()
    {
        submitButton.enabled = false;
        infoText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(string.IsNullOrEmpty(usernameInputField.text)))
        {
            submitButton.enabled = true;
        }
    }

    public void giveTeacherAcess()
    {
    string Tr_username = usernameInputField.text.ToString();
    AddToInformation("The user " + Tr_username + " is successfully credited with teacher access");
    }
    private void AddToInformation(string str) { infoText.text += str; }
}
// if(!(string.IsNullOrEmpty(usernameInputField.text)) && !(string.IsNullOrEmpty(passwordInputField.text)))
// inputFieldIsSet = true;
// string pass = passwordInputField.text;