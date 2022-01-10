using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    private GameObject player;

    public static SpawnPlayer instance;

    private void Awake() {
        instance = this;
    }
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player) {
            CheckPoint();
        }
    }

    public void CheckPoint() {
        player = Instantiate(player).gameObject;
        Vector3 pos = transform.position;
        pos.z = 0;
        player.transform.position = pos;
    }
}
