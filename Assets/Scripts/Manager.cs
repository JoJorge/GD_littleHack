using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
		
	public void Retry() {
		SceneManager.LoadScene ("Game");
	}
	public void ExitGame() {
		Application.Quit ();
	}
}
