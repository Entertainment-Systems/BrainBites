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
    [SerializeField] private GameObject lifeBar;
    [SerializeField] private bool endgame = false;
    private LevelManager lm;

    private void Start()
    {
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

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
        if (collision.transform.gameObject.name == "OpenExit")
        {
            if (!endgame)
            {
                lm.nextLevel();
                Destroy(gameObject);
            }
            else
            {
                Destroy(GameObject.Find("Canvas"));
                GameObject.Find("Main Camera").GetComponent<Animator>().enabled = true;
                lm.lockDoor();
                StartCoroutine(fadeMusic(GameObject.Find("Boombox").GetComponent<AudioSource>(), 2f));
            }
        }
    }

    IEnumerator fadeMusic(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        Destroy(audioSource.gameObject);

        yield return new WaitForSecondsRealtime(5);

        SceneManager.LoadSceneAsync(0);

        //audioSource.volume = startVolume;
    }

    private void HideMe()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        lifeBar.SetActive(true);
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        transform.position = new Vector3(0, -100, 0);
        GetComponent<AudioSource>().PlayOneShot(clips[0]);
        if (CollidedEnemy != null)
            StartCoroutine(CollidedEnemy.GetComponent<EnemyAnimationHandler>().takeover());
    }

    public void returnMe()
    {
        lifeBar.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        if (CollidedEnemy != null)
        {
            GetComponent<AudioSource>().PlayOneShot(clips[1]);
            CollidedEnemy.GetComponent<EnemyAnimationHandler>().die();
            transform.position = CollidedEnemy.transform.position;
            Destroy(CollidedEnemy.GetComponent<Rigidbody2D>());
            Destroy(CollidedEnemy.GetComponent<BoxCollider2D>());
            Destroy(CollidedEnemy.GetComponent<EnemyAnimationHandler>());
            CollidedEnemy = null;
        }
    }
}
