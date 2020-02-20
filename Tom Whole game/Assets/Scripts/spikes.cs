using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(100);
        }   
    }
}
