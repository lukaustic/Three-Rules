using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public GameObject player;
    public bool playerHasKey1 = false;
    public bool playerHasCrowbar = false;
    public bool playerHasKeycard = false;

    public void changeRoom(Vector2 destination)
    {
        player.transform.position = destination;
    }

    public void gainKey()
    {
        playerHasKey1 = true;
    }

    public void gainKeycard()
    {
        playerHasKeycard = true;
    }

    public bool CheckCrowbar()
    {
        if (playerHasCrowbar)
            return true;
        return false;
    }

    public bool CheckKeycard()
    {
        if (playerHasKeycard)
            return true;
        return false;
    }

    public bool CheckKey1()
    {
        if (playerHasKey1)
            return true;
        return false;
    }

    public void gainCrowbar()
    {
        playerHasCrowbar = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
