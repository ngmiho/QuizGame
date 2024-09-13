using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonReStart : MonoBehaviour
{
    public Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(Select);
    }

    private void Select()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
