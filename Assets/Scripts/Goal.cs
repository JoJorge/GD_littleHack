using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

	private bool hasWing;
	private bool hasAura;

	// Use this for initialization
	void Start () {
		hasWing = hasAura = false;
	}
	
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			if (hasWing && hasAura) {
				Debug.Log("Win!!!");
				SceneManager.LoadScene ("GoodEnd");
			}
		}
	}

	public void getWing() {
		hasWing = true;
	}
	public void getAura() {
		hasAura = true;
	}
}
