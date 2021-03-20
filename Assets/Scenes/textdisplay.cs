using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
 
public class textdisplay : MonoBehaviour {

    public Text Textfield;

    public void SetText(string text)
    {
        Textfield.text = text;
    }
}