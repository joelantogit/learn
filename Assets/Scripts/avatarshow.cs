using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarshow : MonoBehaviour
 {
     public GameObject[] images;
     void Start()
     {
         for (int i = 0; i < images.Length; i++)
         {
             if (GameManager.avatarSelection == i)
             {
                 images[i].SetActive(true);
             }
             else
             {
                 images[i].SetActive(false);
             }
         }
     }
 }
