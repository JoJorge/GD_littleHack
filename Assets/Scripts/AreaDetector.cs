using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetector : MonoBehaviour {

	private Listener listener;

	public void setListener(Listener lsr) {
		listener = lsr;
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		listener.receive (gameObject, collider.name);
	}
	public void OnTriggerExit2D(Collider2D collider) {
		listener.receive (gameObject, collider.name + "Exit");
	}
}
