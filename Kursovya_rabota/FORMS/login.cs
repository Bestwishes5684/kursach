using Kursach_zadumin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Kursovya_rabota.FORMS;

namespace Kursovya_rabota
{
    public partial class login : Form
    {
        databases database = new databases();
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string query = $"select id_user,login_user,password_user from register where  login_user='{login}'  and password_user='{password}'";
            SqlCommand command = new SqlCommand(query, database.GetSqlConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно авторизировались");

                login login1 = new login();
                SptotINV iNV = new SptotINV();
                this.Hide();
                iNV.ShowDialog();
                this.Show();

            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
