using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParasiteCollider : MonoBehaviour
{
    public bool CollidedToEnemy = false;
    public GameObject CollidedEnemy;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private LayerMask finishLayer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            CollidedToEnemy = true;
            CollidedEnemy = collision.gameObject;
            //Debug.Log("Get it off me");
            HideMe();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("we done here "+ collision.transform.gameObject.layer + " " + finishLayer. value);
        if (collision.transform.gameObject.name == "OpenExit")
        {
            print("we done here 2");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void HideMe()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        transform.position = new Vector3(0, -100, 0);
        GetComponent<AudioSource>().PlayOneShot(clips[0]);
        if (CollidedEnemy != null)
            StartCoroutine(CollidedEnemy.GetComponent<EnemyAnimationHandler>().takeover());
    }

    public void returnMe()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        if (CollidedEnemy != null)
        {
            CollidedEnemy.GetComponent<EnemyAnimationHandler>().die();
            transform.position = CollidedEnemy.transform.position;
            Destroy(CollidedEnemy.GetComponent<Rigidbody2D>());
            Destroy(CollidedEnemy.GetComponent<BoxCollider2D>());
            Destroy(CollidedEnemy.GetComponent<EnemyAnimationHandler>());
            CollidedEnemy = null;
        }
    }
}
