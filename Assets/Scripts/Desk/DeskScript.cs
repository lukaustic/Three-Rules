using UnityEngine;
using UnityEngine.InputSystem;

public class DeskScript : MonoBehaviour
{
    public InputActionAsset InputActions;
    public LogicScript logic;

    private InputAction interactAction;

    [SerializeField] private AudioClip takeKey;
    [SerializeField] private AudioClip takeCrowbar;

    public bool hasCrowbar = false;
    public bool hasKey = false;
    private bool playerInRange = false;
    private Transform keyTransform;
    private Transform crowbarTransform;

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
        crowbarTransform = transform.Find("Crowbar");
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
                if (hasCrowbar == true)
                {
                    SoundFXManager.instance.PlaySoundFXClip(takeCrowbar, transform, 1f);
                    hasCrowbar = false;
                    crowbarTransform.gameObject.SetActive(false);
                    logic.gainCrowbar();
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
