using UnityEngine;
using UnityEngine.UI;

public class ButtonBack : MonoBehaviour
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
        transform.parent.gameObject.SetActive(false);
        FindObjectOfType<MenuManager>(true).gameObject.SetActive(true);
    }
}
