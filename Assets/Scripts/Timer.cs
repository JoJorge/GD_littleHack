using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	#region Singleton
	static private Timer instance;
	static public Timer Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType (typeof(Timer)) as Timer;
				if (instance == null) {
					GameObject player = new GameObject ("Player");
					instance = player.AddComponent<Timer> ();
				}
			}
			return instance;
		}
	}
	#endregion

	[SerializeField] private float timeLimit;
	[SerializeField] private Text timer;

	public float getTime() {
		return timeLimit;
	}

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
