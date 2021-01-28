using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapGamePerson : MonoBehaviour
{
    [SerializeField] GameObject A;
    [SerializeField] GameObject B;
    private void OnCollisionEnter2D(Collision2D collision) => swap(collision);

    public void swap()
    {
            A.SetActive(false);
            B.SetActive(true);
    }

    public void swap(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            A.SetActive(false);
            B.SetActive(true);
        }
    }
}
