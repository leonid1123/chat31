using MySqlConnector;
using System;
using System.Windows.Forms;

namespace chat31
{
    public partial class Form1 : Form
    {
        static string login = "";
        string password = "";
        bool goOnline = false;
        static int id;

        public int GetMyId()
        {
            return id;
        }
        public Form1()
        {
            InitializeComponent();
        }
        public string GetLogin()
        {
            return login;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            login = LoginInput.Text.Trim();
            password = PassInput.Text.Trim();
            if (login.Length != 0 & password.Length != 0)
            {
                //MySqlConnection connection = new MySqlConnection("Server=localhost;User ID=pk31;Password=123456;Database=pk31chat");
                //connection.Open();

                ErrorLabel.Text = DataBaseConnection.GetConnection().State.ToString();
                MySqlCommand command = new MySqlCommand("SELECT password, id FROM users WHERE login =@param;", DataBaseConnection.GetConnection());
                command.Parameters.AddWithValue("param", login);
                command.ExecuteNonQuery();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    ErrorLabel.Text += " ,пользователь есть,";
                    reader.Read();
                    if (password.Equals(reader.GetString(0)))
                    {
                        ErrorLabel.Text += "пароль совпадает";
                        goOnline = true;
                        id = reader.GetInt32(1);
                    }
                    else
                    {
                        ErrorLabel.Text += "пароль не совпадает";
                    }
                }
                else
                {
                    ErrorLabel.Text += " ,такого пользователя нет.";
                }
                //подключиться к БД+
                //сделать запрос по имени пользователя+
                //если такое имя пользователя есть,
                //то проверить пароль на совпадение,+
                //если совпадает, то поменять статус online на true
                //после перейти в окно чата
                DataBaseConnection.CloseConnection();
                if (goOnline)
                {

                    MySqlCommand loginCommand = 
                        new MySqlCommand("UPDATE `users` SET `online` = true WHERE `users`.`login` = @param1;", DataBaseConnection.GetConnection());
                    loginCommand.Parameters.AddWithValue("param1", login);
                    
                    loginCommand.ExecuteNonQuery();
                    DataBaseConnection.CloseConnection();
                    Form2 form2 = new Form2();
                    form2.Show();
                    //this.Close();
                    
                }
            }
            else
            {
                ErrorLabel.Text = "Логин или пароль не введены!";
            }
        }
    }
}
