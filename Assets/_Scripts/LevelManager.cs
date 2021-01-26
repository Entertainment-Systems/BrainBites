using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int enemiesLeft;
    Animator transition;
    [SerializeField] bool finishedLevel = false;
    [SerializeField] GameObject openDoor;
    [SerializeField] GameObject closeDoor;
    [SerializeField] AudioClip openSound;
    // Start is called before the first frame update
    void Start()
    {
        transition = GetComponent<Animator>();
        if (enemiesLeft == 0)
            unlockExit();
    }

    void Update()
    {
        if(finishedLevel)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void nextLevel()
    {
        transition.SetBool("fadeOut", true);
    }

    public void unlockExit()
    {
        GetComponent<AudioSource>().PlayOneShot(openSound);
        closeDoor.SetActive(false);
        openDoor.SetActive(true);
    }
}
