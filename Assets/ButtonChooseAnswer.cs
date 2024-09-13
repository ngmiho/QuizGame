using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChooseAnswer : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => Select("a"));
        }
    }
    public void Select(string question)
    {
        if (question.Equals(button.GetComponent<TextMeshProUGUI>().text))
            Debug.Log("true");
        else
            Debug.Log("false");
    }
}
