using ExcelDataReader;
using System.Data;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrophyManager : MonoBehaviour
{
    public DataTable table;
    public int TrophySelected = 0;
    public Button[] buttons;
    private void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        //if (buttons != null)
        //{
        //    for (int i = 0; i < buttons.Length; i++)
        //    {
        //        int index = i;
        //        buttons[index].onClick.AddListener(() => Select(index));
        //    }
        //}
        GetExcel();
        SetTrophy();
    }

    public void Select(int buttonIndex)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            if (buttonIndex != index)
                buttons[index].GetComponentInChildren<Image>().gameObject.SetActive(false);
            else
                buttons[index].GetComponentInChildren<Image>().gameObject.SetActive(true);
        }
    }

    public void SetTrophy()
    {
        for (int i = 0; i < table.Rows.Count - 1; i++)
        {
            int index = i;
            DataRow row = table.Rows[index + 1];
            GameObject goTrophy = GetComponentInChildren<TrophyManager>().gameObject;
            GameObject goButton = goTrophy.transform.GetChild(i).gameObject;
            Button button = goButton.GetComponent<Button>();
            button.onClick.AddListener(() => ToggleTrophy(index));
            Image image = button.GetComponentInChildren<Image>();
            Sprite sprite = Resources.Load<Sprite>("Trophies/" + row[1].ToString());
            image.GetComponent<Image>().sprite = sprite;
        }
    }

    public void ToggleTrophy(int currentIndex)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            if (currentIndex != index)
            {
                buttons[i].transform.GetChild(0).gameObject.SetActive(false);
                FindObjectOfType<ExcelIndex>(true).GetComponent<TextMeshProUGUI>().text = (currentIndex + 1).ToString();
            }
            else
                buttons[i].transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void GetExcel()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

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
        //excel - troyphy
        table = result.Tables[0];


        reader.Close();
        stream.Close();
    }
}
