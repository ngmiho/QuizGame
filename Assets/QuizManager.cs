using ExcelDataReader;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshProUGUIs;
    public Button[] buttons;
    public DataTable table;
    public int[] arrNumberQuestion;

    public int countQuestion;
    public int correctAnswers;

    public int sheetIndex;

    //audio
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void HandleCorrectAnswer(GameObject go)
    {
        GetRow();
        SetQuestionTotal();
        go.SetActive(false);
    }

    void Start()
    {

        //button - assign function with pass params
        for (int i = 0; i < buttons.Length; ++i)
        {
            int index = i;
            Button button = buttons[index];
            buttons[index].onClick.AddListener(() => StartCoroutine(Select(textMeshProUGUIs[index].text, textMeshProUGUIs[4].text, button)));
        }
        //button - assign function next question for dialog correct
        GameObject goDAC = GetComponentInChildren<DialogAnnoucementCorrect>(true).gameObject;
        Button btnDAC = goDAC.GetComponentInChildren<Button>();
        btnDAC.onClick.AddListener(() => HandleCorrectAnswer(goDAC));
        goDAC.SetActive(false);

        GetExcel();

        countQuestion = 0;
        GetComponentInChildren<BarFill>().GetComponent<Image>().fillAmount = (countQuestion + 1) * (100f / arrNumberQuestion.Length / 100f);

        GetRow();

        //invoke once when begin to assign
        SetQuestionTotal();
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

        countQuestion = 0;
        GetComponentInChildren<BarFill>().GetComponent<Image>().fillAmount = (countQuestion + 1) * (100f / arrNumberQuestion.Length / 100f);

        correctAnswers = 0;
        GetComponentInChildren<CorrectTotal>().gameObject.GetComponent<TextMeshProUGUI>().text = "0 correct";

        GetExcel();
        GetRow();
        SetQuestionTotal();

    }

    public void Shuffle(int[] array)
    {
        System.Random rng = new System.Random();
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int index = i;

            int j = rng.Next(0, index + 1);

            int temp = array[index];
            array[index] = array[j];
            array[j] = temp;
        }
    }

    //private MySqlConnection connection;

    public void GetExcel()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //string path = Path.Combine(Application.dataPath, "quizzData.xlsx");
        string path = Path.Combine(Application.streamingAssetsPath, "quizzData.xlsx");
        FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
        {
            FallbackEncoding = Encoding.GetEncoding(1252),
            Password = "password",
            AutodetectSeparators = new char[] { ',', ';', '\t', '|', '#' },
            LeaveOpen = false,
            AnalyzeInitialCsvRows = 0,
        });
        DataSet result = reader.AsDataSet();
        sheetIndex = int.Parse(FindObjectOfType<ExcelIndex>().GetComponent<TextMeshProUGUI>().text);
        table = new DataTable();
        table = result.Tables[sheetIndex];

        //arrNumberQuestion = new int[table.Rows.Count];
        //Debug.Log(table.Rows.Count);
        //for (int i = 0; i < table.Rows.Count; ++i)
        //{
        //    int index = i;
        //    arrNumberQuestion[index] = index + 1;
        //}

        arrNumberQuestion = new int[table.Rows.Count - 1];

        for (int i = 0; i < arrNumberQuestion.Length; i++)
        {
            int index = i;
            arrNumberQuestion[i] = index + 1;
        }


        Shuffle(arrNumberQuestion);


        reader.Close();
        stream.Close();
    }

    public void SetQuestionTotal()
    {
        FindObjectOfType<QuestionTotal>().GetComponent<TextMeshProUGUI>().text = (countQuestion + 1).ToString() + " / " + (table.Rows.Count - 1).ToString();
    }

    public void GetRow()
    {
        DataRow row = table.Rows[arrNumberQuestion[countQuestion]];
        if (row == null)
            return;

        int[] arr = { 0, 1, 2, 3 };
        Shuffle(arr);

        textMeshProUGUIs[arr[0]].text = row[0].ToString();
        textMeshProUGUIs[arr[1]].text = row[1].ToString();
        textMeshProUGUIs[arr[2]].text = row[2].ToString();
        textMeshProUGUIs[arr[3]].text = row[3].ToString();
        textMeshProUGUIs[4].text = row[4].ToString();
        textMeshProUGUIs[5].text = row[5].ToString();
        textMeshProUGUIs[6].text = "Question number " + (countQuestion + 1).ToString();

        string imageName = row[7].ToString();
        GameObject goImage = GetComponentInChildren<QuestionImage>(true).gameObject;
        if (imageName.Length > 0)
        {
            goImage.SetActive(true);

            //set image
            switch (sheetIndex)
            {
                case 1:
                    goImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Tequilas/" + imageName);
                    break;
                case 2:
                    goImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Wines/" + imageName);
                    break;
                case 3:
                    goImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Vodkas/" + imageName);
                    break;
            }


        }
        else
        {
            goImage.SetActive(false);
            goImage.GetComponent<Image>().sprite = null;
        }

    }

    public void SetGOResultUI()
    {
        FindObjectOfType<QuizManager>().gameObject.SetActive(false);

        GameObject goUIResult = FindObjectOfType<ResultManager>(true).gameObject;
        goUIResult.SetActive(true);

        GameObject goRow = goUIResult.GetComponentInChildren<RowManager>().gameObject;
        TextMeshProUGUI[] texts0 = goRow.transform.GetChild(0).GetComponentsInChildren<TextMeshProUGUI>();
        texts0[0].text = "Question Number";
        texts0[1].text = arrNumberQuestion.Length.ToString();
        TextMeshProUGUI[] texts1 = goRow.transform.GetChild(1).GetComponentsInChildren<TextMeshProUGUI>();
        texts1[0].text = "Correct Answer";
        texts1[1].text = correctAnswers.ToString();
    }

    public AudioSource audioSourceAnnoucement;
    public AudioClip audioClipCorrect;
    public AudioClip audioClipIncorrect;

    public IEnumerator Select(string answer, string correctAnswer, Button button)
    {
        yield return new WaitForSeconds(1f);

        if (answer.Equals(correctAnswer))
        {
            //audio
            if (audioSourceAnnoucement != null)
                audioSourceAnnoucement.PlayOneShot(audioClipCorrect);

            correctAnswers++;
            countQuestion++;
            GetComponentInChildren<BarFill>().GetComponent<Image>().fillAmount = (countQuestion + 1) * (100f / arrNumberQuestion.Length / 100f);

            if (countQuestion == arrNumberQuestion.Length)
            {
                GameObject goFinish = GetComponentInChildren<AnnoucementResult>(true).gameObject;
                goFinish.SetActive(true);
                goFinish.GetComponentInChildren<Button>().onClick.AddListener(() => SetGOResultUI());
                //Time.timeScale = 0;

                //countQuestion = 0;
                //questionCurrent.text = "0";

                yield return null;
            }
            else
            {
                GameObject go = GetComponentInChildren<DialogAnnoucementCorrect>(true).gameObject;
                go.SetActive(true);

                go.GetComponentInChildren<TextMeshProUGUI>().text = "Correct";

                //set TextCurrentQuestion
                TextMeshProUGUI questionCorrect = GetComponentInChildren<CorrectTotal>().gameObject.GetComponent<TextMeshProUGUI>();
                if (correctAnswers > 1)
                    questionCorrect.text = correctAnswers.ToString() + " corrects";
                else
                    questionCorrect.text = correctAnswers.ToString() + " correct";


                //button - set interactable true
                foreach (Button b in buttons)
                {
                    b.interactable = true;
                }
            }
        }
        else
        {
            //audio
            if (audioSourceAnnoucement != null)
                audioSourceAnnoucement.PlayOneShot(audioClipIncorrect);

            GetComponentInChildren<BarFill>().GetComponent<Image>().fillAmount = (countQuestion + 1) * (100f / arrNumberQuestion.Length / 100f);

            button.interactable = false;

            int isInteractable = 0;
            foreach (Button b in buttons)
            {
                if (b.IsInteractable())
                    isInteractable++;
            }

            if (isInteractable == 1)
            {
                countQuestion++;
                GetComponentInChildren<BarFill>().GetComponent<Image>().fillAmount = (countQuestion + 1) * (100 / arrNumberQuestion.Length / 100);
            }

            if (countQuestion == arrNumberQuestion.Length)
            {
                GameObject goFinish = GetComponentInChildren<AnnoucementResult>(true).gameObject;
                goFinish.SetActive(true);
                goFinish.GetComponentInChildren<Button>().onClick.AddListener(() => SetGOResultUI());
                //Time.timeScale = 0;

                //countQuestion = 0;
                //questionCurrent.text = "0";

                //button - set interactable true
                foreach (Button b in buttons)
                {
                    b.interactable = true;
                }

                yield return null;
            }
            else if (isInteractable == 1)
            {
                GetComponentInChildren<BarFill>().GetComponent<Image>().fillAmount = (countQuestion + 1) * (100 / arrNumberQuestion.Length / 100);

                GameObject goCorrect = GetComponentInChildren<DialogAnnoucementCorrect>(true).gameObject;
                goCorrect.SetActive(true);

                //button - set interactable true
                foreach (Button b in buttons)
                {
                    b.interactable = true;
                }

                goCorrect.GetComponentInChildren<TextMeshProUGUI>().text = "Incorrect";
                goCorrect.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Next Question";

                //set TextCurrentQuestion
                //TextMeshProUGUI questionCurrent = GetComponent<QuizManager>().gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                //questionCurrent.text = countQuestion.ToString();

                yield return new WaitForSeconds(1f);
            }
            else
            {
                GameObject go = GetComponentInChildren<DialogAnnoucementIncorrect>(true).gameObject;
                go.SetActive(true);

                go.GetComponentInChildren<TextMeshProUGUI>().text = "Incorrect";
                GetComponentInChildren<BarFill>().GetComponent<Image>().fillAmount = (countQuestion + 1) * (100f / arrNumberQuestion.Length / 100f);
            }

        }
        GetComponentInChildren<BarFill>().GetComponent<Image>().fillAmount = (countQuestion + 1) * (100f / arrNumberQuestion.Length / 100f);
    }
}
