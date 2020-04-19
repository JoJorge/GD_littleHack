using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaCondition : ConditionStrategy, Listener {

	[SerializeField]private List<AreaDetector> areas;
	private bool playerIn;

	// Use this for initialization
	void Start () {
		foreach (AreaDetector area in areas) {
			area.setListener (this);
		}
		playerIn = false;
	}

	public override bool fulfill () {
		if (playerIn) {
			playerIn = false;
			return true;
		}
		return false;
	}
	public void receive (GameObject obj, string str) {
		if (str == "Player") {
			playerIn = true;
		}
	}
}

