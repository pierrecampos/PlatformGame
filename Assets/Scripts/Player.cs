using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rig;
    public Animator anim;
    public float speed;
    public float jumpForce;
    private bool isJumping;
    private bool doubleJump;

    void Start() {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Jump();
    }

    void FixedUpdate() {
        Move();
    }

    void Move() {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0) {
            if (!isJumping) {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (movement < 0) {
            if (!isJumping) {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (movement == 0 && !isJumping) {
            anim.SetInteger("transition", 0);

        }
    }

    void Jump() {
        if (Input.GetButtonDown("Jump")) {
            if (!isJumping) {
                anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                doubleJump = true;
            } else if (doubleJump) {
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doubleJump = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == 6) {
            isJumping = false;
        }
    }
}
