using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helper : MonoBehaviour
{
    public GameObject help;
    public Animator anim;
    bool alive = true;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            anim.SetTrigger("player");
            alive = false;
            FindObjectOfType<Audiomanager>().Play("pickup");
        }
    }
    void Update()
    {
        if (alive == false)
        {
            Destroy(help, 2f);
        }  
    }

}
