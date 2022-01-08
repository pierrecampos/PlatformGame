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

    public int health;
    public float speed;
    public float maxVision;
    private bool isDeath;

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

        if (hit.collider != null && hit.transform.CompareTag("Player") && !isDeath) {
            isFront = true;

            float distance = Vector2.Distance(transform.position, hit.transform.position);

            if (distance <= stopDistance) {
                isFront = false;
                anim.SetInteger("transition", 2);
                rig.velocity = Vector2.zero;
                AttackTime(hit.transform.GetComponent<Player>());
            }
        } else if (hit.collider != null) {
            isFront = false;
            anim.SetInteger("transition", 0);
            rig.velocity = Vector2.zero;
        }


        RaycastHit2D behindHit = Physics2D.Raycast(behindPoint.position, -direction, maxVision);
        if (behindHit.collider != null && behindHit.transform.CompareTag("Player")) {
            isRight = !isRight;
        }
    }

    float timerAttack;
    void AttackTime(Player player) {
        timerAttack += Time.deltaTime;
        if (timerAttack >= 1f) {
            player.OnHit();
            timerAttack = 0;
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

        if (isFront && !isDeath) {
            anim.SetInteger("transition", 1);
            rig.velocity = vel;
        }
    }

    public void OnHit() {
        health--;
        anim.SetTrigger("hit");

        if (health <= 0 && !isDeath) {
            isDeath = true;
            anim.SetTrigger("death");
            Destroy(gameObject, 0.5f);
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawRay(point.position, direction * maxVision);
        Gizmos.DrawRay(behindPoint.position, -direction * maxVision);
    }
}
