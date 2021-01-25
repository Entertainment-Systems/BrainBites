using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int enemiesLeft;
    [SerializeField] GameObject openDoor;
    [SerializeField] GameObject closeDoor;
    [SerializeField] AudioClip openSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void unlockExit()
    {
        GetComponent<AudioSource>().PlayOneShot(openSound);
        closeDoor.SetActive(false);
        openDoor.SetActive(true);
    }
}
