using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour {

    private Rigidbody2D rig;

    public float speed;
    public LayerMask layer;
    public Transform point;
    public float radius;
    void Start() {
        rig = GetComponent<Rigidbody2D>();
    }


    void Update() {

    }

    void FixedUpdate() {
        rig.velocity = new Vector2(speed, rig.velocity.y);
        OnCollision();
    }


    void OnCollision() {
        Collider2D hit = Physics2D.OverlapCircle(point.position, radius, layer);

        if (hit) {
            speed *= -1;
            transform.eulerAngles = speed > 0 ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(point.position, radius);
    }
}
