using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    private Transform player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player) {
            Vector3 pos = transform.position;
            pos.z = 0;
            player.transform.position = pos;
        }
    }
}
