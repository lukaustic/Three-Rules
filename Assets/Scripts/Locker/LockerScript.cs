using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEditor.U2D.ScriptablePacker;

public class LockerScript : MonoBehaviour
{
    public BackgroundScript roomScript;
    public EnemyScript enemyScript;

    public Animator animator;
    public InputActionAsset InputActions;
    private InputAction interactAction;
    private Transform lockTransform;
    private Transform keyCardTransform;
    public LogicScript logic;

    [SerializeField] private AudioClip slamSoundClip;
    [SerializeField] private AudioClip lockedSoundClip;

    public bool isOpen = false;
    public bool hasKeycard = true;
    private bool playerInRange = false;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        roomScript = GameObject.FindGameObjectWithTag("Background").GetComponent<BackgroundScript>(); ;
        enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>(); ;
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
        lockTransform = transform.Find("Lock");
        keyCardTransform = transform.Find("Keycard");
    }

    void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            if (playerInRange)
            {
                if (logic.CheckCrowbar())
                {
                    if (isOpen == false)
                    {
                        SoundFXManager.instance.PlaySoundFXClip(slamSoundClip, transform, 0.05f);
                        isOpen = true;
                        animator.SetBool("IsOpen", true);
                        lockTransform.gameObject.SetActive(false);
                        keyCardTransform.gameObject.SetActive(true);
                        if (roomScript.rainVolume < 0.2f)
                            enemyScript.KillPlayer();
                    }
                    else
                    {
                        keyCardTransform.gameObject.SetActive(false);
                        logic.gainKeycard();
                    }
                }
                else
                {
                    SoundFXManager.instance.PlaySoundFXClip(lockedSoundClip, transform, 0.5f);
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
