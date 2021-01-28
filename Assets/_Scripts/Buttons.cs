using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void quitGame() => Application.Quit();

    public void startGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
