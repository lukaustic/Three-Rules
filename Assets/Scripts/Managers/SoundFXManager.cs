using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private AudioSource currentLoopingSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayLoopingSoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if (currentLoopingSource != null && currentLoopingSource.isPlaying)
            return;

        currentLoopingSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        currentLoopingSource.clip = audioClip;
        currentLoopingSource.volume = volume;
        currentLoopingSource.loop = true;
        currentLoopingSource.Play();
    }

    public void StopLoopingSoundFX()
    {
        if (currentLoopingSource != null)
        {
            currentLoopingSource.Stop();
            Destroy(currentLoopingSource.gameObject);
            currentLoopingSource = null;
        }
    }
}
