using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D rb2D;
    private ParasiteCollider parasite;

    private float thrust = 10.0f;

    void Start()
    {
        GetPlayerBody();
    }

    private void Update()
    {
        if (parasite != null)
        {
            if( player.gameObject.GetComponent<ParasiteCollider>().CollidedToEnemy)
            {
                player.gameObject.GetComponent<ParasiteCollider>().CollidedToEnemy = false;
                player = player.gameObject.GetComponent<ParasiteCollider>().CollidedEnemy;
                GetPlayerBody();
          
            }
        }
    }

    /// <summary>
    /// For controls and pysics updates
    /// </summary>
    void FixedUpdate()
    {
        //Controls
        if (Input.GetButton("Fire1"))
        {
            rb2D.AddForce(transform.right * thrust);
        }
        if (Input.GetButton("Fire2"))
        {
            ReturnToParasite();
        }
    }

    /// <summary>
    /// refreshes the players attributes to the new target. ie. changing rb2D to new target rb2D
    /// </summary>
    private void GetPlayerBody()
    {
        rb2D = player.GetComponent<Rigidbody2D>();
        if (player.gameObject.GetComponent<ParasiteCollider>() != null)
        {
            parasite = player.gameObject.GetComponent<ParasiteCollider>();
        }
        else
        {
            parasite = null;
        }
    }

    /// <summary>
    /// returns the target back to the parasite
    /// </summary>
    private void ReturnToParasite()
    {
        Debug.Log("we running");
        player = GameObject.FindGameObjectWithTag("Player");
        GetPlayerBody();
    }

}