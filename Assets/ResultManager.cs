using UnityEngine;

public class ResultManager : MonoBehaviour
{
    //audio
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void OnDisable()
    {
        //audio
        audioSource.Stop();
    }

    private void OnEnable()
    {
        //audio
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(audioClip);
    }
}
