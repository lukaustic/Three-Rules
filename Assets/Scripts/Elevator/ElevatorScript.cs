using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ElevatorScript : MonoBehaviour
{
    public Animator animator;
    public InputActionAsset InputActions;
    private InputAction interactAction;
    public LogicScript logic;

    [SerializeField] private AudioClip openSoundClip;

    public bool isOpen = false;
    private bool playerInRange = false;

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
                if (logic.CheckKeycard())
                {
                    if (isOpen == false)
                    {
                        SoundFXManager.instance.PlaySoundFXClip(openSoundClip, transform, 0.5f);
                        isOpen = true;
                        animator.SetBool("isOpen", true);
                    }
                    else
                    {
                        EndGame();
                    }
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

    public void EndGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
