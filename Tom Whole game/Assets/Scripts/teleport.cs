using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    private float exitVelocity;
    public GameObject portal;
    public GameObject player;
    bool ifteleport=true ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && ifteleport==true)
        {
            player.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
            ifteleport = false;
        }

    }
}
