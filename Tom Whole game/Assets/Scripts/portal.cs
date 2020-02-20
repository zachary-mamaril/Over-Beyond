using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    public static portal portalControlInstance;
    [SerializeField]
    private GameObject portal1, portal2;
    [SerializeField]
    private Transform portalspawn1, portalspawn2;
    private Collider2D portal1collider, portal2collider;
    [SerializeField]
    private GameObject player;

    void Start()
    {
        portalControlInstance = this;
        portal1collider = portal1.GetComponent<Collider2D>();
        portal2collider = portal2.GetComponent<Collider2D>();
    }
    public void CreateClone(string whereToCreate)
    {
        if(whereToCreate == "at1")
        {
            var instantiatedClone = Instantiate(player, portalspawn1.position, Quaternion.identity);
            instantiatedClone.gameObject.tag = "Player";
        }
        else if(whereToCreate == "at2")
        {
            var instantiatedClone = Instantiate(player, portalspawn2.position, Quaternion.identity);
            instantiatedClone.gameObject.tag = "Player";
        }
    }

    public void DisableCollider(string colliderToDisable)
    {
        if(colliderToDisable == "portal1")
        {
            portal1collider.enabled = false;
        }
        else if(colliderToDisable == "portal2")
        {
            portal2collider.enabled = false;
        }
    }
    public void EnabledColliders()
    {
        portal1collider.enabled = true;
        portal2collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
