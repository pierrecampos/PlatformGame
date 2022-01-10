using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    private Transform player;

    public static SpawnPlayer instance;

    private void Awake() {
        instance = this;
    }
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player) {
            CheckPoint();
        }
    }

    public void CheckPoint() {
        Vector3 pos = transform.position;
        pos.z = 0;
        player.transform.position = pos;
        if (player.GetComponent<Player>().isDeath) {
            player.GetComponent<Player>().ResetStats();
        }
    }
}
