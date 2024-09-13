using UnityEngine;
using UnityEngine.UI;

public class ButtonClose : MonoBehaviour
{
    void Start()
    {
        if (GetComponent<Button>() != null)
        {
            GetComponent<Button>().onClick.AddListener(Select);
        }
    }

    private void Select()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
