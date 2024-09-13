using UnityEngine;
using UnityEngine.UI;


public class ButtonOption : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public Slider volumeSlider;     // Reference to a UI Slider component for volume control
    public Button muteButton;       // Reference to a Button component for muting/unmuting

    private bool isMuted = false;

    public Button button;

    void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
            button.onClick.AddListener(ActiveUIOption);

        // Set up initial volume based on the slider value
        if (volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        // Set up mute button
        if (muteButton != null)
        {
            muteButton.onClick.AddListener(ToggleMute);
        }

    }

    private void ActiveUIOption()
    {
        GameObject goOption = FindObjectOfType<DialogOption>(true).gameObject;
        goOption.SetActive(true);
    }

    void SetVolume(float volume)
    {
        // Set the volume of the audio source
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }



    void ToggleMute()
    {
        // Toggle mute state
        isMuted = !isMuted;
        if (audioSource != null)
        {
            //image
            if (isMuted)
            {
                Sprite image = Resources.Load<Sprite>("Mutes/image1");
                if (image != null)
                {
                    FindObjectOfType<ButtonMute>().GetComponent<Image>().sprite = image;
                }
                else
                {
                    Debug.Log("Null");
                }

            }
            else
            {
                Sprite image = Resources.Load<Sprite>("Mutes/image0");
                if (image != null)
                {
                    FindObjectOfType<ButtonMute>().GetComponent<Image>().sprite = image;
                }
                else
                {
                    Debug.Log("Null");
                }
            }
            audioSource.mute = isMuted;
        }
    }
}
