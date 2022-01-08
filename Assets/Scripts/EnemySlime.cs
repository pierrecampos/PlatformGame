using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour {

    private Rigidbody2D rig;
    private Animator anim;

    public float speed;
    public int health;

    public LayerMask layer;
    public Transform point;
    public float radius;
    void Start() {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

    public void OnHit() {
        anim.SetTrigger("hit");
        health--;

        if (health <= 0) {
            speed = 0;
            anim.SetTrigger("death");
            Destroy(gameObject, 0.6f);
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(point.position, radius);
    }
}
