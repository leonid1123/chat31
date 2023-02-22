using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace chat31
{
    public partial class Form1 : Form
    {
        string login = "";
        string password = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login = LoginInput.Text.Trim(); 
            password = PassInput.Text.Trim();
            if (login.Length != 0 & password.Length!=0) 
            {
                MySqlConnection connection = new MySqlConnection("Server=localhost;User ID=pk31;Password=123456;Database=pk31chat");
                connection.Open();
                ErrorLabel.Text = connection.State.ToString();
                MySqlCommand command = new MySqlCommand("SELECT password FROM users WHERE login =@param;", connection);
                command.Parameters.AddWithValue("param", login);
                command.ExecuteNonQuery();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    ErrorLabel.Text += " ,пользователь есть.";
                }
                //подключиться к БД+
                //сделать запрос по имени пользователя+
                //если такое имя пользователя есть,
                //то проверить пароль на совпадение,
                //если совпадает, то поменять статус online на true
                //после перейти в окно чата
            } else
            {
                ErrorLabel.Text = "Логин или пароль не введены!";
            }
        }
    }
}
