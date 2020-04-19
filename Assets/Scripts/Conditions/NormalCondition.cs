using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalCondition : ConditionStrategy {

	private string originDialog;
	private string lastDialog;
	private Text UIText;
	private float startTime;
	[SerializeField] float stayTime;
	private bool saying;

	public void Start () {
		UIText = transform.parent.Find ("Text").GetComponent<Text>();
		originDialog = lastDialog = UIText.text;
		saying = false;
	}

	public void Update() {
		if (lastDialog != UIText.text) {
			lastDialog = UIText.text;
			startTime = Time.time;
			saying = true;
		}
	}

	public override bool fulfill () {
		if (saying && Time.time - startTime > stayTime) {
			saying = false;
			lastDialog = originDialog;
			return true;
		}
		return false;
	}
}
