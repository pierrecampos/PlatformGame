using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblin : MonoBehaviour {

    private Rigidbody2D rig;
    private Animator anim;

    public Transform point;
    public Transform behindPoint;

    public bool isFront;
    public bool isRight;
    public float stopDistance;
    private Vector2 direction;

    public float speed;
    public float maxVision;

    void Start() {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        OnMove();
    }

    void FixedUpdate() {
        GetPlayer();
        OnMove();
    }

    void GetPlayer() {
        RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision);

        if (hit.collider != null && hit.transform.CompareTag("Player")) {
            isFront = true;
            
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            
            if (distance <= stopDistance) {
                isFront = false;
                anim.SetInteger("transition", 2);                
                rig.velocity = Vector2.zero;
                hit.transform.GetComponent<Player>().OnHit();
            }
        }

        RaycastHit2D behindHit = Physics2D.Raycast(behindPoint.position, -direction, maxVision);
        if (behindHit.collider != null && behindHit.transform.CompareTag("Player")) {
            isRight = !isRight;
        }
    }

    void OnMove() {
        Vector2 vel;
        if (isRight) {
            transform.eulerAngles = new Vector2(0, 0);
            vel = new Vector2(speed, rig.velocity.y);
            direction = Vector2.right;
        } else {
            transform.eulerAngles = new Vector2(0, 180);
            vel = new Vector2(-speed, rig.velocity.y);
            direction = Vector2.left;
        }

        if (isFront) {
            anim.SetInteger("transition", 1);
            rig.velocity = vel;
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawRay(point.position, direction * maxVision);
        Gizmos.DrawRay(behindPoint.position, -direction * maxVision);
    }
}
