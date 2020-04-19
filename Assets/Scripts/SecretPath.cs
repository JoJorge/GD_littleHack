using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPath : MonoBehaviour {

	[SerializeField] private float timeToOpenPath;
	private Listener listener;
	private bool startMove;

	public void setListener(Listener lsr) {
		listener = lsr;
	}

	// Use this for initialization
	void Start () {
		startMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!startMove && Timer.Instance.getTime () < timeToOpenPath) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 2);
			listener.receive (gameObject, "open");
			startMove = true;
		}
	}
}
