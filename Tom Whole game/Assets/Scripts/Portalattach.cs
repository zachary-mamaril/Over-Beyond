using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portalattach : MonoBehaviour
{
    private Rigidbody2D enterRB;
    private float enterV, exitV;//help manage player teleportation
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enterRB = collision.gameObject.GetComponent<Rigidbody2D>();//when character touches portal collider, get component
        enterV = enterRB.velocity.x;//control player when entering portal
        if (gameObject.name == "portal1")//if player enter's portal1
        {
            portal.portalControlInstance.DisableCollider("portal2");//prevent cloning of player
            portal.portalControlInstance.CreateClone("at2");//portal to connected portal
        }
        else if (gameObject.name == "portal2")
        {
            portal.portalControlInstance.DisableCollider("portal1");
            portal.portalControlInstance.CreateClone("at1");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)//when player leaves portal collider
    {
        exitV = enterRB.velocity.x;//control player when exiting portal
        if (enterV != exitV)
        {
            Destroy(GameObject.Find("clone"));//if player doesnt wish to portal while portaling
        }
        else if (gameObject.name != "clone")
        {
            Destroy(collision.gameObject);//when leaving proper site
            portal.portalControlInstance.EnabledColliders();
            GameObject.Find("clone").name = GameObject.Find("player").tag;//the clone is now the player
        }

    }
}
