using UnityEngine;
using UnityEngine.InputSystem;

public class DoorOpenScript : MonoBehaviour
{
    public InputActionAsset InputActions;
    public LogicScript logic;

    private InputAction interactAction;

    private bool playerInRange = false;
    public bool doorOpen = false;
    public int id = 3;
    public Vector2 destination;
    [SerializeField] private AudioClip openDoorSoundClip;
    [SerializeField] private AudioClip locketDoorSoundClip;
    [SerializeField] private AudioClip unlockDoorSoundClip;

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
    }

    void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            if (playerInRange)
            {
                if (doorOpen == true)
                {
                    SoundFXManager.instance.PlaySoundFXClip(openDoorSoundClip, transform, 1f);
                    logic.changeRoom(destination);
                }
                else if (logic.CheckKey1() && id == 3)
                {
                    SoundFXManager.instance.PlaySoundFXClip(unlockDoorSoundClip, transform, 1f);
                    doorOpen = true;
                }
                else
                {
                    SoundFXManager.instance.PlaySoundFXClip(locketDoorSoundClip, transform, 1f);
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
