using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Reflection;
using System;

public enum Interpolator{
	Linear,
	AccelDecel
}

public class CameraController : MonoBehaviour {
    private Player player;
	protected GameObject target;
	protected GameObject lastTarget;
	protected float time;
	protected float duration;
	protected Interpolator interpolator;

	protected GameObject bg;
	protected float viewLeft;
	protected float viewRight;
	protected float viewUp;
	protected float viewDown;

	public void setTarget(GameObject target, float duration, Interpolator interpolator){
		lastTarget = this.target;
		this.target = target;
		this.duration = duration;
		this.interpolator = interpolator;
		time = 0;
	}

	public bool isMoving(){
		return time < duration;
	}


    public void Start() {
        player = Player.Instance;
		target = player.gameObject;
		lastTarget = target;
		duration = 1f;
		time = 1f;
		interpolator = Interpolator.Linear;

		bg = GameObject.Find ("Stage").transform.Find ("bg").gameObject;
		Vector2 size = bg.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		size = Vector2.Scale (size, new Vector2 (bg.transform.lossyScale.x, bg.transform.lossyScale.y));
		viewLeft = viewRight = size.x / 2 - Camera.main.orthographicSize * Camera.main.aspect;
		viewUp = viewDown = size.y / 2 - Camera.main.orthographicSize;
    }


	void LateUpdate(){
		if (player != null) {
			Vector3 center = bg.transform.position;
			Vector3 now = transform.position;

			float rate = Mathf.Clamp(time/duration, 0, 1);
			switch (interpolator) {
				case Interpolator.Linear:
					break;
				case Interpolator.AccelDecel:
					rate = Mathf.Cos ((rate + 1) * Mathf.PI) / 2 + 0.5f;
					break;
			}
			Vector3 pos = Vector3.Lerp(lastTarget.transform.position, target.transform.position, rate);

			pos.x = Mathf.Clamp (pos.x, center.x - viewLeft, center.x + viewRight);
			pos.y = Mathf.Clamp (pos.y, center.y - viewDown, center.y + viewUp);

			now.x = pos.x;
			now.y = pos.y;
			transform.position = now;
			time += Time.deltaTime;
		}
	}


}
