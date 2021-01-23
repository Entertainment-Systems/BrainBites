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
            Debug.Log(CollidedEnemy);
        }
    }
}
