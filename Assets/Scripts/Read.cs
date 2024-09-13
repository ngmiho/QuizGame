using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class Read : MonoBehaviour
{
    private string connectionString;

    string query;

    private MySqlConnection MS_Connection;

    private MySqlCommand MS_Command;

    private MySqlDataReader MS_Reader;

    public Text textCanvas;

    private void Start()
    {
        viewInfo();
    }

    public void viewInfo()
    {

        query = "SELECT * FROM Quizz";


        connectionString = "Server=127.0.0.1;Database=db;User Id=root;Password=NguMiHo98;Charset=utf8";

        MS_Connection = new MySqlConnection(connectionString);

        MS_Connection.Open();

        MS_Command = new MySqlCommand(query, MS_Connection);

        MS_Reader = MS_Command.ExecuteReader();

        while (MS_Reader.Read())
        {
            textCanvas.text += MS_Reader[0];
        }

        MS_Reader.Close();
    }

}