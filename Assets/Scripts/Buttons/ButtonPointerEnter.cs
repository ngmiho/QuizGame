using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPointerEnter : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;
    public AudioClip audioButtonHover;
    public AudioClip audioButtonClick;

    private void Start()
    {
        //sound button - mouse hover
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        gameObject.GetComponent<Button>().onClick.AddListener(Select);
    }

    private void Select()
    {
        if (audioButtonClick != null)
            audioSource.PlayOneShot(audioButtonClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioButtonHover != null)
            audioSource.PlayOneShot(audioButtonHover);
    }
}
