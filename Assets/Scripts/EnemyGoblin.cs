using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblin : MonoBehaviour {

    private Rigidbody2D rig;
    public Transform point;

    public float speed;
    public float maxVision;

    void Start() {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update() {

    }

    void FixedUpdate() {
        GetPlayer();
    }

    void GetPlayer() {
        RaycastHit2D hit = Physics2D.Raycast(point.position, Vector2.right, maxVision);

        if (hit.collider != null && hit.transform.CompareTag("Player")) {
            Debug.Log("Player Encontrado");
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawRay(point.position, Vector2.right * maxVision);
    }
}
