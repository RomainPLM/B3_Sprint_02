using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager _instance;
    public AudioSource _audioSourceObject;


    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    public void PlayAudioClip(AudioClip[] audioClips, Transform spawnTransform, bool isALoop, float volume)
    {
        int randomClip = Random.Range(0, audioClips.Length);
        AudioSource audioSource = Instantiate(_audioSourceObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClips[randomClip];
        audioSource.Play();
        audioSource.volume = volume;

        if (isALoop == false)
        {
            float clipDuration = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipDuration);
        }
    }
}
