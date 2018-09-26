using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	
	[SerializeField] private float timeLimit;
	[SerializeField] private Text timer;

	public void Start() {
		timer.text =  Mathf.CeilToInt(timeLimit).ToString();
	}

	public void Update() {
		timeLimit -= Time.deltaTime;
		timer.text =  Mathf.CeilToInt(timeLimit).ToString();
		if (timeLimit < 0) {
			SceneManager.LoadScene ("BadEnd");
		}
	}
}
