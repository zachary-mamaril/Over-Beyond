using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingobject : MonoBehaviour
{
    public float speed = 1f;
    Rigidbody2D rgbd;
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        rgbd.velocity = new Vector2(speed, 0);
    }
}

