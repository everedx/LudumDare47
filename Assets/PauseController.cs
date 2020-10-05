using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResumeGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpaceshipHandler>().ResumeGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
