using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	private bool pressed;
	[SerializeField] private Text text;

	// Use this for initialization
	void Start () {
		pressed = false;
		text.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!pressed && Input.anyKeyDown) {
			pressed = true;
			text.gameObject.SetActive (true);
		}
		else if(pressed && Input.anyKeyDown) {
			SceneManager.LoadScene ("Game");
		}
	}
}
