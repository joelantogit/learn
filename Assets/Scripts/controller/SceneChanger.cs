using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Google;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{

	public Text infoText;

	public void ChangeScene(string name)
	{
		SceneManager.LoadScene(name);
	}
	public void Logout()
	{
		GoogleSignIn.DefaultInstance.SignOut();
		AddToInformation("Signed out user");
		SceneManager.LoadScene("Scenes/login");

	}

	private void AddToInformation(string str) { infoText.text += "\n" + str; }
}