using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Google;

public class SceneChanger : MonoBehaviour
{
	public void ChangeScene(string name)
	{
		SceneManager.LoadScene(name);
	}
	public void Logout()
	{
		//Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		//auth.SignOut();
		//SceneManager.LoadScene("Scenes/login");
	}
}