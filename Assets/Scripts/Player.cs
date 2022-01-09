using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rig;
    public Animator anim;
    public Transform point;
    public float radius;
    public LayerMask enemyLayer;

    public int health;
    public float speed;
    public float jumpForce;

    private bool isJumping;
    private bool doubleJump;
    private bool isAttacking;
    private bool isDeath;

    private Player instance;
    private void Awake() {
        DontDestroyOnLoad(this);

        if (instance) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    void Start() {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Jump();
        Attack();
    }

    void FixedUpdate() {
        Move();
    }

    void Move() {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0) {
            if (!isJumping && !isAttacking) {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (movement < 0) {
            if (!isJumping && !isAttacking) {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (movement == 0 && !isJumping && !isAttacking) {
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

    void Attack() {
        if (Input.GetButtonDown("Fire1") && !isAttacking) {
            isAttacking = true;
            anim.SetInteger("transition", 3);

            Collider2D hit = Physics2D.OverlapCircle(point.position, radius, enemyLayer);

            if (hit) {
                if (hit.GetComponent<EnemySlime>()) {
                    hit.GetComponent<EnemySlime>().OnHit();
                }

                if (hit.GetComponent<EnemyGoblin>()) {
                    hit.GetComponent<EnemyGoblin>().OnHit();
                }
            }
            StartCoroutine(OnAttack());
        }

    }
    public void OnHit() {
        health--;
        anim.SetTrigger("hit");

        if (health <= 0 && !isDeath) {
            anim.SetTrigger("death");
            isDeath = true;
        }
    }

    IEnumerator OnAttack() {
        yield return new WaitForSeconds(.33f);
        isAttacking = false;
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(point.position, radius);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == 6) {
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.layer == 7) {
            OnHit();
        }

        if (col.CompareTag("Coin")) {
            col.GetComponent<Animator>().SetTrigger("pickUp");
            Destroy(col.gameObject, 0.417f);
            GameController.instance.GetCoin();
        }
    }


}
