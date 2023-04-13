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
            
            MySqlCommand command = new MySqlCommand("SELECT `nik` FROM `users` WHERE `login`= @param", DataBaseConnection.GetConnection());
            command.Parameters.AddWithValue("param", userLogin);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            //SELECT `nik` FROM `users` WHERE `login`= "user"
            label1.Text = "Добро пожаловать: " + reader.GetString(0);
            //SELECT `nik` FROM `users` WHERE `online`= true
            DataBaseConnection.CloseConnection();

            MySqlCommand onlineUsers = new MySqlCommand("SELECT `nik` FROM `users` WHERE `online`= true", DataBaseConnection.GetConnection());
            MySqlDataReader onlineUsersReader = onlineUsers.ExecuteReader();
            listBox2.Items.Clear();
            while(onlineUsersReader.Read())
            {
                listBox2.Items.Add(onlineUsersReader.GetString(0));
            }
            DataBaseConnection.CloseConnection();

            MySqlCommand getAllMsg = new MySqlCommand("SELECT `users`.`nik`, `msg`.`time`," +
                "`msg`.`text` FROM `msg` JOIN `users` ON `msg`.`nik`=`users`.`id` " +
                "ORDER BY `msg`.`time` DESC", DataBaseConnection.GetConnection());
            MySqlDataReader getAllMsgReader = getAllMsg.ExecuteReader();
            listBox1.Items.Clear();
            while(getAllMsgReader.Read())
            {
                listBox1.Items.Add(getAllMsgReader.GetString(0) + ":" + getAllMsgReader.GetDateTime(1));
                listBox1.Items.Add(getAllMsgReader.GetString(2));
            }
            //SELECT `users`.`nik`, `msg`.`time`,`msg`.`text` FROM `msg` JOIN `users` ON `msg`.`nik`=`users`.`id` ORDER BY `msg`.`time` DESC
            DataBaseConnection.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)//кнопка отправить сообщение
        {
            //INSERT INTO `msg`( `text`, `whom`, `nik`) VALUES("Еще одно сообщение", 0, 1);
            MySqlCommand sendMessage = new MySqlCommand("INSERT INTO `msg`( `text`, `whom`, `nik`) VALUES(@myMsg, 0, @myId);", DataBaseConnection.GetConnection());
            sendMessage.Parameters.AddWithValue("@myMsg",textBox1.Text.Trim());
            //sendMessage.Parameters.AddWithValue("@myId",); найти свой ID!!!
        }
    }
}
