using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace chat31
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1= new Form1();
            string userLogin = form1.GetLogin();
            MySqlConnection connection = new MySqlConnection("Server=localhost;User ID=pk31;Password=123456;Database=pk31chat");
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT `nik` FROM `users` WHERE `login`= @param", connection);
            command.Parameters.AddWithValue("param", userLogin);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            //SELECT `nik` FROM `users` WHERE `login`= "user"
            label1.Text = "Добро пожаловать: " + reader.GetString(0);
            //SELECT `nik` FROM `users` WHERE `online`= true
            connection.Close();
            connection.Open();
            MySqlCommand onlineUsers = new MySqlCommand("SELECT `nik` FROM `users` WHERE `online`= true", connection);
            MySqlDataReader onlineUsersReader = onlineUsers.ExecuteReader();
            listBox2.Items.Clear();
            while(onlineUsersReader.Read())
            {
                listBox2.Items.Add(onlineUsersReader.GetString(0));
            }
            connection.Close();
            connection.Open();
            MySqlCommand getAllMsg = new MySqlCommand("SELECT `users`.`nik`, `msg`.`time`," +
                "`msg`.`text` FROM `msg` JOIN `users` ON `msg`.`nik`=`users`.`id` " +
                "ORDER BY `msg`.`time` DESC", connection);
            MySqlDataReader getAllMsgReader = getAllMsg.ExecuteReader();
            listBox1.Items.Clear();
            while(getAllMsgReader.Read())
            {
                listBox1.Items.Add(getAllMsgReader.GetString(0) + ":" + getAllMsgReader.GetDateTime(1));
                listBox1.Items.Add(getAllMsgReader.GetString(2));
            }
            //SELECT `users`.`nik`, `msg`.`time`,`msg`.`text` FROM `msg` JOIN `users` ON `msg`.`nik`=`users`.`id` ORDER BY `msg`.`time` DESC
        }
    }
}
