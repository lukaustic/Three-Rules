using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
