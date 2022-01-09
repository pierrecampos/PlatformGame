using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour {

    public string nextLevel;

    private void OnTriggerEnter2D(Collider2D collision) {
        GameController.instance.NextLevel(nextLevel);
    }
}
