using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public GameObject player;
    public bool playerHasKey1 = false;

    public void changeRoom(Vector2 destination)
    {
        player.transform.position = destination;
    }

    public void gainKey()
    {
        playerHasKey1 = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
