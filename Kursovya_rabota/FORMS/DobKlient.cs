using Kursach_zadumin;
using Microsoft.Office.Interop.Excel;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kursovya_rabota.FORMS
{
    public partial class DobKlient : Form
    {
        public int tv2 = 0;

        public List<int> tv=new List<int>();

        databases database = new databases();
        public DobKlient()
        {
            InitializeComponent();
        }

        private void DobKlient_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kurs1DataSet.sport_inv". При необходимости она может быть перемещена или удалена.
            this.sport_invTableAdapter.Fill(this.kurs1DataSet.sport_inv);

            var queryString = $"SELECT id,name FROM sport_inv";
            var command = new SqlCommand(queryString, database.GetSqlConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            comboBox1.Items.Clear();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[1].ToString());
                tv.Add(int.Parse(reader[0].ToString()));
            }
            reader.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();

            var id = textBox1.Text;
            var name = textBox2.Text;
            var kol = textBox4.Text;
            var time= textBox5.Text;
           
           
                var a = $"insert into [dbo].[Klient] (id,name,Thing,Kol_vo,time) values('{id}','{name}','{tv2}','{kol}','{time}')";

                var command = new SqlCommand(a, database.GetSqlConnection());

                command.ExecuteNonQuery();

                MessageBox.Show("Запись созданна");
            
           
            database.CloseConnection();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tv2 = tv[comboBox1.SelectedIndex];
        }
    }
}
