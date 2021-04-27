using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
 
public class ChangeColor : MonoBehaviour {

    public Image img;

    public void ChangeColorToBlack(){
        img.GetComponent<Image>().color = Color.black;
    }
}