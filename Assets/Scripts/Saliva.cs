using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saliva : MonoBehaviour {

    public Vector3 init_pos;
    public Rigidbody2D rigid;
    public Vector2 velocity;
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
}
