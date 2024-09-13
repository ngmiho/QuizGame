using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonNext : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button = GetComponentInChildren<Button>();

        if (button != null)
            button.onClick.AddListener(Select);

        gameObject.SetActive(false);
    }

    private void Select()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);
    }
}
