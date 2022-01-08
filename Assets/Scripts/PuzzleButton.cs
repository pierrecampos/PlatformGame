using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour {

    private Animator anim;
    public Animator barrierAnim;
    public LayerMask validMask;
    public float radius;

    void Start() {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        OnCollision();
    }

    private void OnCollision() {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, validMask);
        bool isPressed;
        if (hit) {
            isPressed = true;
            hit = null;
        } else {
            isPressed = false;
        }

        anim.SetBool("isPressed", isPressed);
        barrierAnim.SetBool("isPressed", isPressed);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
