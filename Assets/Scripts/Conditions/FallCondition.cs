using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCondition : ConditionStrategy {

	private Player player;
	private float prvHeight;
	private bool highFall;
	private float startTime;
	private float keepTime = 5.0f;
	[SerializeField] float fallHeight;

	// Use this for initialization
	void Start () {
		player = Player.Instance;	
		prvHeight = player.transform.position.y;
		highFall = false;
	}
	
	// Update is called once per frame
	void Update () {
		float nowHeight = player.transform.position.y;
		if (player.isOnFloor () && nowHeight != prvHeight) {
			if (prvHeight - nowHeight > fallHeight) {
				startTime = Time.time;
				highFall = true;
			}
			prvHeight = nowHeight;
		}
		if (Time.time - startTime > keepTime) {
			highFall = false;
		}
	}

	public override bool fulfill () {
		if (highFall) {
			highFall = false;
			return true;
		}
		return false;
	}
}
