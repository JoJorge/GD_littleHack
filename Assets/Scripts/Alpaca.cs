using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction {
	left, right
}

public class Alpaca : MonoBehaviour {

	[SerializeField] private float interval;
	[SerializeField] private Direction direction;
	[SerializeField] private float speed;
	[SerializeField] private GameObject saliva;
	[SerializeField] private Sprite alpacaSaliva;
	private Sprite alpacaOrigin;
	private float startTime;
	private Vector3 salivaPos;
	[SerializeField] private float openInterval = 0.5f;
	private float startOpenTime;

	public void Start() {
		startTime = Time.time;
		startOpenTime = -1;
		salivaPos = new Vector3 (0.35f, 0.1f, 0);
		if (direction == Direction.left) {
			salivaPos.x *= -1;
			speed *= -1;
		}
		alpacaOrigin = GetComponent<SpriteRenderer> ().sprite;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > interval) {
			GetComponent<SpriteRenderer> ().sprite = alpacaSaliva;
			GameObject tmpSaliva = GameObject.Instantiate (saliva);
			tmpSaliva.transform.parent = transform;
			if (direction == Direction.left) {
				tmpSaliva.GetComponent<SpriteRenderer> ().flipX = true;
			}
			tmpSaliva.GetComponent<Saliva> ().init (salivaPos, new Vector2(speed, 0));
			startTime = Time.time;
			startOpenTime = Time.time;


		}
		if (startOpenTime >= 0 &&  Time.time - startOpenTime > openInterval) {
			startOpenTime = -1;
			GetComponent<SpriteRenderer> ().sprite = alpacaOrigin;
		}
	}
}
