using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyScript : MonoBehaviour
{
    Collider2D[] colliders;
    private Rigidbody2D rb;
    public float walkSpeed = 5;

    public float maxRightPosition = 10f;
    public float maxLeftPosition = -10f;
    public float maxUpPosition = 10f;
    public float maxDownPosition = -10f;
    private float elapsedTime = 0f;

    void Start()
    {
        
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
        if (elapsedTime >= 0 && elapsedTime <= 9)
        {
            Walking(Vector2.right * walkSpeed * Time.deltaTime);
            colliders[4].enabled = false;
            colliders[1].enabled = true;
        }
        if (elapsedTime >= 10 && elapsedTime <= 19)
        {
            Walking(Vector2.up * walkSpeed * Time.deltaTime);
            colliders[1].enabled = false;
            colliders[2].enabled = true;
        }
        if (elapsedTime >= 20 && elapsedTime <= 29)
        {
            Walking(Vector2.left * walkSpeed * Time.deltaTime);
            colliders[2].enabled = false;
            colliders[3].enabled = true;
        }
        if (elapsedTime >= 30 && elapsedTime <= 39)
        {
            Walking(Vector2.down * walkSpeed * Time.deltaTime);
            colliders[3].enabled = false;
            colliders[4].enabled = true;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Seen");
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
}
