using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button button;
    //audio
    public AudioSource audioSource;
    public AudioClip audioClip;
    private void Start()
    {
        button = GetComponentInChildren<ButtonStart>().GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(Select);
    }

    private void OnDisable()
    {
        //audio
        audioSource.Stop();
    }

    private void OnEnable()
    {
        //audio
        audioSource.PlayOneShot(audioClip);
    }

    private void Select()
    {
        gameObject.SetActive(false);
        FindObjectOfType<QuizManager>(true).gameObject.SetActive(true);
    }
}
