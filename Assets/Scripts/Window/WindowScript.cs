using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class WindowScript : MonoBehaviour
{
    public bool IsOpen = false;
    public InputActionAsset InputActions;
    private InputAction interactAction;
    private bool playerInRange = false;
    public float rainSoundEffect = 0.025f;
    [SerializeField] private AudioClip openWindowSoundClip; 
    [SerializeField] private AudioClip closeWindowSoundClip;
    public Animator animator;

    public BackgroundScript background;

    void Start()
    {
        background = GameObject.FindGameObjectWithTag("Background").GetComponent<BackgroundScript>();
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
                if (IsOpen == false)
                {
                    SoundFXManager.instance.PlaySoundFXClip(openWindowSoundClip, transform, 0.5f);
                    IsOpen = true;
                    background.IncreaseRainVolume(rainSoundEffect);
                    animator.SetBool("IsOpen", true);
                }
                else
                {
                    SoundFXManager.instance.PlaySoundFXClip(closeWindowSoundClip, transform, 0.5f);
                    IsOpen = false;
                    background.DecreaseRainVolume(rainSoundEffect);
                    animator.SetBool("IsOpen", false);
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
