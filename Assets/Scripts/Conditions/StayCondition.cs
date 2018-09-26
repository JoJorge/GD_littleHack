using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayCondition : ConditionStrategy {

	private Player player;
	[SerializeField] private float stayTime;
	private float startStayTime;
	private bool staying;

	// Use this for initialization
	void Start () {
		player = Player.Instance;
		staying = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<Rigidbody2D> ().velocity == Vector2.zero) {
			if (!staying) {
				staying = true;
				startStayTime = Time.time;
			}
		}
		else {
			staying = false;
		}
	}

	public override bool fulfill () {
		if (staying && Time.time - startStayTime > stayTime) {
			startStayTime = Time.time;
			return true;
		}
		return false;
	} 
}
