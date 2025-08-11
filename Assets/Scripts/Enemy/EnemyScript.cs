using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerLocation;

    Collider2D[] colliders;
    private Rigidbody2D rb;
    public float walkSpeed = 5;
    public float jumpSpeed = 0f;

    public GameObject gameOver;

    public float maxRightPosition = 10f;
    public float maxLeftPosition = -10f;
    public float maxUpPosition = 10f;
    public float maxDownPosition = -10f;
    private float elapsedTime = 0f;
    public bool killPlayer = false;

    public bool IsPatroling = false;

    public void KillPlayer()
    {
        playerLocation = (player.transform.position - transform.position).normalized;
        jumpSpeed = 10f;
        killPlayer = true;
    }

    void Update()
    {
        if (elapsedTime >= 40)
        {
            elapsedTime = 0;
        }
        elapsedTime += Time.deltaTime;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponents<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (IsPatroling)
        {
            if (elapsedTime >= 0 && elapsedTime < 10)
            {
                Walking(Vector2.right * walkSpeed * Time.deltaTime);
                colliders[2].enabled = false;
                colliders[1].enabled = true;
            }
            if (elapsedTime >= 10 && elapsedTime < 20)
            {
                Walking(Vector2.up * walkSpeed * Time.deltaTime);
                colliders[1].enabled = false;
                colliders[4].enabled = true;
            }
            if (elapsedTime >= 20 && elapsedTime < 30)
            {
                Walking(Vector2.left * walkSpeed * Time.deltaTime);
                colliders[4].enabled = false;
                colliders[3].enabled = true;
            }
            if (elapsedTime >= 30 && elapsedTime < 40)
            {
                Walking(Vector2.down * walkSpeed * Time.deltaTime);
                colliders[3].enabled = false;
                colliders[2].enabled = true;
            }
        }
        if (killPlayer)
        {
            Jumping(playerLocation * jumpSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            KillPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ShowGameOverUI();
        }
    }

    private void Walking(Vector2 direction)
    {
        Vector3 newPosition = rb.position + direction;
        if (newPosition.x >= maxLeftPosition && newPosition.x <= maxRightPosition && newPosition.y >= maxDownPosition && newPosition.y <= maxUpPosition) 
        {
            rb.MovePosition(newPosition);
        }
    }

    private void Jumping(Vector2 direction)
    {
        Vector3 newPosition = rb.position + direction;
        rb.MovePosition(newPosition);
    }

    private void ShowGameOverUI()
    {
        Time.timeScale = 0f;
        gameOver.SetActive(true);
    }
}
