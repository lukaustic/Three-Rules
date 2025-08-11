using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField] private AudioClip rainClip;
    [SerializeField] private AudioClip thunderClip;

    public float rainVolume = 0.05f;
    public float thunderVolume = 1f;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange)
        {
            SoundFXManager.instance.PlayLoopingSoundFXClip(rainClip, transform, rainVolume);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            SoundFXManager.instance.PlaySoundFXClip(thunderClip, transform, thunderVolume);
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

    public void IncreaseRainVolume(float amount)
    {
        rainVolume += amount;
        SoundFXManager.instance.StopLoopingSoundFX();
        SoundFXManager.instance.PlayLoopingSoundFXClip(rainClip, transform, rainVolume);
    }

    public void DecreaseRainVolume(float amount)
    {
        rainVolume -= amount;
        SoundFXManager.instance.StopLoopingSoundFX();
        SoundFXManager.instance.PlayLoopingSoundFXClip(rainClip, transform, rainVolume);
    }
}
