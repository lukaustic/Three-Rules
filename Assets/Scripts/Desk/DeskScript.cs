using UnityEngine;
using UnityEngine.InputSystem;

public class DeskScript : MonoBehaviour
{
    public InputActionAsset InputActions;
    public LogicScript logic;

    private InputAction interactAction;

    [SerializeField] private AudioClip takeKey;

    public bool hasKey;
    private bool playerInRange = false;
    private Transform keyTransform;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        keyTransform = transform.Find("Key");
    }

    void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            if (playerInRange)
            {
                if (hasKey == true)
                {
                    SoundFXManager.instance.PlaySoundFXClip(takeKey, transform, 1f);
                    hasKey = false;
                    keyTransform.gameObject.SetActive(false);
                    logic.gainKey();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            playerInRange = false;
        }
    }
}
