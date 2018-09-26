using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour {

	[SerializeField] private GameObject wingUI;
	[SerializeField] private float newJumpSpeed;
	private Goal goal;

	// Use this for initialization
	void Start () {
		wingUI.SetActive (false);
		goal = GameObject.Find ("Goal").GetComponent<Goal>();
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			Player.Instance.setJumpSpeed (newJumpSpeed);
			wingUI.SetActive (true);
			goal.getWing ();
			Destroy (gameObject);
		}
	}
}
