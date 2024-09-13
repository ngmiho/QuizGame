using UnityEngine;
using UnityEngine.UI;

public class ButtonExit : MonoBehaviour
{

    public Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(Select);
        }
    }

    private void Select()
    {
        Application.Quit();
    }
}
