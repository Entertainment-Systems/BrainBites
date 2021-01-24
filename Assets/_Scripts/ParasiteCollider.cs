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
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        transform.position = new Vector3(0, -100, 0);
    }

    public void returnMe()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        if (CollidedEnemy != null)
        {
            transform.position = CollidedEnemy.transform.position;
            Destroy(CollidedEnemy.GetComponent<Rigidbody2D>());
            Destroy(CollidedEnemy.GetComponent<BoxCollider2D>());
        }
    }
}
