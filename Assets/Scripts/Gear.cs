using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {

	[SerializeField] private GameObject gearUI;

	// Use this for initialization
	void Start () {
		gearUI.SetActive (false);
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			gearUI.SetActive (true);
			Destroy (gameObject);
		}
	}
}
