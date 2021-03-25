using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Google;
using UnityEngine.UI;
using Firebase.Auth;

public class SceneChanger : MonoBehaviour
{

	public Text infoText;
	private FirebaseAuth auth;

	public void ChangeScene(string name)
	{
		SceneManager.LoadScene(name);
	}
	public void Logout()
	{
		auth = FirebaseAuth.DefaultInstance;
		GoogleSignIn.DefaultInstance.SignOut();
		auth.SignOut();
		AddToInformation("Signed out user");
		
		SceneManager.LoadScene("Scenes/login");

	}

	private void AddToInformation(string str) { infoText.text += "\n" + str; }
}