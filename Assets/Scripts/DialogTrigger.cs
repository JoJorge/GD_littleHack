using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour {

	[SerializeField] private ConditionStrategy condition;
	[SerializeField] private Sprite head;
	[SerializeField] private List<string> dialogs;
	private int idx;
	private Image UIHead;
	private Text UIText;

	// Use this for initialization
	void Start () {
		idx = 0;
		UIHead = transform.parent.Find ("Head").GetComponent<Image> ();
		UIText = transform.parent.Find ("Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (condition.fulfill ()) {
			UIHead.sprite = head;
			UIText.text = dialogs [idx];
			idx = (idx + 1) % dialogs.Count;
		}
	}
}
