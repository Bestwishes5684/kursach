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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace Kursovya_rabota.FORMS
{
    public partial class DobINV : Form
    {
        databases database = new databases();
        public DobINV()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();

            var id = textBox5.Text;
            var name = textBox1.Text;
            var vid = textBox3.Text;
            var kol = textBox4.Text;
            int price;

            if(int.TryParse(textBox2.Text,out price))
            {
                var a = $"insert into sport_inv (id,name,price,vid_sporta,[kol-vo]) values('{id}','{name}','{price}','{vid}','{kol}')";

                var command = new SqlCommand(a, database.GetSqlConnection());

                command.ExecuteNonQuery();

                MessageBox.Show("Запись созданна");
            }
            else
            {
                MessageBox.Show("Запись не созданна");
            }
            database.CloseConnection();

            
        }

        private void DobINV_Load(object sender, EventArgs e)
        {

        }
    }
}
