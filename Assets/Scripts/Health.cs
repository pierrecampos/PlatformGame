using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField]
    private int healthCount;

    private int health;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Image[] hearts;

    void Update() {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health;

        for (int x = 0; x < hearts.Length; x++) {
            hearts[x].sprite = x < health ? fullHeart : emptyHeart;
        }

        for (int x = 0; x < hearts.Length; x++) {
            hearts[x].enabled = x < healthCount;
        }

    }
}
