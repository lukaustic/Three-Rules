using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject contols;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenControls()
    {
        mainMenu.SetActive(false);
        contols.SetActive(true);
    }
    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        contols.SetActive(false);
    }
}
