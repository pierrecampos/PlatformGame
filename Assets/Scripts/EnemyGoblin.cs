using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblin : MonoBehaviour {

    private Rigidbody2D rig;
    public Transform point;
    public bool isFront;
    public bool isRight;
    public float stopDistance;
    private Vector2 direction;

    public float speed;
    public float maxVision;

    void Start() {
        rig = GetComponent<Rigidbody2D>();
        OnMove();
    }

    void Update() {

    }

    void FixedUpdate() {
        GetPlayer();
        OnMove();
    }

    void GetPlayer() {
        RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision);

        isFront = true;

        if (hit.collider != null && hit.transform.CompareTag("Player")) {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance <= stopDistance) {
                isFront = false;
                rig.velocity = Vector2.zero;
                hit.transform.GetComponent<Player>().OnHit();
            }
        }
    }

    void OnMove() {
        if (isFront) {
            if (isRight) {
                transform.eulerAngles = new Vector2(0, 0);
                rig.velocity = new Vector2(speed, rig.velocity.y);
                direction = Vector2.right;
            } else {
                transform.eulerAngles = new Vector2(0, 180);
                rig.velocity = new Vector2(-speed, rig.velocity.y);
                direction = Vector2.left;
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawRay(point.position, direction * maxVision);
    }
}
