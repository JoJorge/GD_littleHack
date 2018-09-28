using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saliva : MonoBehaviour {

	private Vector3 init_pos;
	private Rigidbody2D rigid;
	[SerializeField] private Vector2 velocity;
	[SerializeField] private Vector2 force;
	[SerializeField] private float freezeTime;

	public void init(Vector3 pos, Vector2 v) {
		transform.localPosition = pos;
		init_pos = pos;
		velocity = v;
		if (v.x < 0) {
			force.x *= -1;
		}
	}

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = velocity;
        init_pos = transform.position;
    }

    // Update is called once per frame
    void Update () {
        
        if(Mathf.Abs(transform.position.x - init_pos.x) > 2.5)
        {
            Destroy(gameObject);
        }

    }

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			// collider.gameObject.GetComponent<Rigidbody2D> ().AddForce (force);
			collider.gameObject.GetComponent<Player>().StartCoroutine(collider.GetComponent<Player>().hit(force, freezeTime));
			Destroy (gameObject);
		}
	}
}
