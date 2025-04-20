using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
   public static SoundFXManager instance;

   [SerializeField] private AudioSource soundFXObject;
   bool canPlayAnother = true;
   float timer = 0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (timer > 0f) {
            timer -= Time.deltaTime;
        } else {
            canPlayAnother = true;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position , Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLen = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLen);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClips, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position , Quaternion.identity);

        audioSource.clip = GetRandomAudioClip(audioClips);

        audioSource.volume = volume;

        audioSource.Play();

        float clipLen = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLen);
    }

    // The function name could be better, but this function uses a system
    // that doesnt allow another clip to be played until the last one is finished
    // ideal for places where the this function will be called rapidly like walking
    public void PlaySoundFXClipAfterAnother(AudioClip[] audioClips, Transform transform, float volume)
    {
        if (canPlayAnother) {
            int randIdx = Random.Range(0, audioClips.Length);

            AudioSource audioSource = Instantiate(soundFXObject, transform.position , Quaternion.identity);
            canPlayAnother = false;

            audioSource.clip = audioClips[randIdx];

            audioSource.volume = volume;

            audioSource.Play();

            float clipLen = audioSource.clip.length;
            timer = 0.3f;

            Destroy(audioSource.gameObject, clipLen);
            // canPlayAnother = true;
        }
    }

    private AudioClip GetRandomAudioClip(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }
}
