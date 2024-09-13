using UnityEngine;
using UnityEngine.UI;

public class AnnoucementResult : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button = GetComponentInChildren<Button>();

        if (button != null)
            button.onClick.AddListener(Select);
    }

    private void Select()
    {
        //inactive UIAnnoucementResult
        gameObject.SetActive(false);
        //inactive UIQuiz
        GameObject goQuiz = FindObjectOfType<QuizManager>(true).gameObject;
        goQuiz.SetActive(false);
        //active UIResult
        GameObject goResult = FindObjectOfType<ResultManager>(true).gameObject;
        goResult.SetActive(true);
    }
}
