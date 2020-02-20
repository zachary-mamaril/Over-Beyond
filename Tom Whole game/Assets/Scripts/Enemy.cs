using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer enemyface;
    private GameObject Player;
    public float speed = 4f;
    void Start()
    {
        enemyface = GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");
    }
    void Update()
    {
        //look at player
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
    }
}
