using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCondition : ConditionStrategy, Listener {

	[SerializeField] GameObject secretArea;
	private bool hasOpen;
	private bool hasPlayer;
	private bool hasFulfill;

	// Use this for initialization
	void Start () {
		secretArea.GetComponent<AreaDetector>().setListener (this);
		secretArea.GetComponent<SecretPath>().setListener (this);
		hasOpen = false;
		hasPlayer = false;
		hasFulfill = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void receive(GameObject obj, string str) {
		if (str == "open") {
			hasOpen = true;
		}
		if (str == "Player") {
			hasPlayer = true;
		}
		if (str == "PlayerExit") {
			hasPlayer = false;
		}
	}
	public override bool fulfill () {
		if (!hasFulfill && hasOpen && hasPlayer) {
			hasFulfill = true;
			return true;
		}
		return false;
	}
}
