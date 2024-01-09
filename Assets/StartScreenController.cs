using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController
{
    public void PlayGame ()

    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
