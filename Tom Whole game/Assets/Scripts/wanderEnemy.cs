using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderEnemy : MonoBehaviour
{
    SpriteRenderer spr;
    Rigidbody2D rgbd;
    public float wanderTime = 10f;
    public float speed = 0f;
    public float resetTime = 5f;
    private bool faceright;
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

   
    void Update()
    {
        Vector3 scal = transform.localScale;
        if (wanderTime > 3)
        {
            rgbd.velocity= new Vector2(1*speed,0);
            wanderTime -= Time.deltaTime;
            spr.flipX = false;
        }
        else if(wanderTime <= 3 && wanderTime >0)
        {
            Wander();
            wanderTime -= Time.deltaTime;
        }
        else
        {
            wanderTime = resetTime;
        }
    }
    void Wander()
    {
        rgbd.velocity = new Vector2(-1*speed,0);
        spr.flipX = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            FindObjectOfType<Audiomanager>().Play("dont");

        }
    }
}
