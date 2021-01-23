using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteCollider : MonoBehaviour
{
    public bool CollidedToEnemy = false;
    public GameObject CollidedEnemy;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            CollidedToEnemy = true;
            CollidedEnemy = collision.gameObject;
            Debug.Log("Get it off me");
            HideMe();
        }
    }

    private void HideMe()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void returnMe()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //gameObject.GetComponent<Collider2D>().enabled = true;
        if(CollidedEnemy !=null)
            transform.position = CollidedEnemy.transform.position;
    }
}
