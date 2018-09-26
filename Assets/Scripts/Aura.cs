using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour {

	[SerializeField] private GameObject auraUI;
	private GameObject playerAura;
	private Goal goal;

	// Use this for initialization
	void Start () {
		auraUI.SetActive (false);
		goal = GameObject.Find ("Goal").GetComponent<Goal>();
		playerAura = Player.Instance.transform.GetChild (0).gameObject;
		playerAura.SetActive (false);
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			auraUI.SetActive (true);
			playerAura.SetActive (true);
			goal.getAura ();
			Destroy (gameObject);
		}
	}
}
