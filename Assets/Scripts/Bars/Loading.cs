using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public float fillLoading = 0;
    void Start()
    {
        StartCoroutine(FillLoading());
    }

    public IEnumerator FillLoading()
    {
        while (fillLoading <= 1)
        {
            fillLoading += 0.1f;
            GetComponentInChildren<BarFill>().GetComponent<Image>().fillAmount += 0.1f;
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.SetActive(false);
        FindObjectOfType<MenuManager>(true).gameObject.SetActive(true);
    }
}
