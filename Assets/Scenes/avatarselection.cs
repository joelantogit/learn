using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarselection : MonoBehaviour
 {
     public int NumberOfItems
     {
         get { return numberOfItems; }
         set { if (value != numberOfItems)
             {
                 doSwitch = true;
             }
             numberOfItems = value;
             numberOfItems = Mathf.Clamp(numberOfItems, 0, images.Length);
             GameManager.avatarSelection = numberOfItems;
         }
     }
     private int numberOfItems;
     public GameObject[] images;
     public bool doSwitch;
     void Start()
     {
         for (int i = 0; i < images.Length; i++)
         {
             images[i].SetActive(false);
         }
         //defaulImage.SetActive (false);
     }
 
     void Update()
     {
         if (doSwitch)
         {
             for (int i = 0; i < images.Length; i++)
             {
                 if (i == NumberOfItems)
                 {
                     images[i].SetActive(true);
                 }
                 else
                 {
                     images[i].SetActive(false);
                 }
             }
             doSwitch = false;
         }
     }
     public void setSelection(bool direction)
     {
         if (direction)
         {
             NumberOfItems += 1;
         }
         else
         {
             NumberOfItems -= 1;
         }
     }
 }
