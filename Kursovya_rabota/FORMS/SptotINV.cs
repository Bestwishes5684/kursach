﻿using Kursach_zadumin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.Data.OleDb;
using System.Security.Cryptography;

namespace Kursovya_rabota.FORMS
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted,
    }
    public partial class SptotINV : Form
    {
        string dostup;
        int selectrow;
        databases database = new databases();
        public SptotINV()
        {
            InitializeComponent();
        }
        public void Dostup(string user)
        {
            dostup = user;
        }


        
        private void CreatColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("name", "Название");
            dataGridView1.Columns.Add("price", "Цена");
            dataGridView1.Columns.Add("Vid_sporta", "Вид спорта");
            dataGridView1.Columns.Add("kol-vo", "Количество");
            dataGridView1.Columns.Add("Istnew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dataGrid, IDataRecord record)
        {
            dataGrid.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetString(3), record.GetInt32(4), RowState.ModifiedNew);
        }

        private void RefreshDatagrid(DataGridView dwg)
        {
            dataGridView1.Rows.Clear();
            string query = $"select * from sport_inv";
            SqlCommand cmd = new SqlCommand(query, database.GetSqlConnection());
            database.openConnection();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dwg, reader);
            }
            reader.Close();


        }
        private void ClearField()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SptotINV_Load(object sender, EventArgs e)
        {
            CreatColumns();
            RefreshDatagrid(dataGridView1);


            if (dostup == "Пользователь")
            {
                Dob.Enabled = false;
                panel1.Enabled = false;
                DEl.Enabled=false;
                RED.Enabled=false;
                sav.Enabled =false;
                button5.Enabled =false;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectrow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectrow];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
            }
        }

        private void Dob_Click(object sender, EventArgs e)
        {
            DobINV dobINV = new DobINV();
            dobINV.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            search(dataGridView1);

        }

        private void search(DataGridView dwg)
        {
            dataGridView1.Rows.Clear();

            string search = $"select * from sport_inv where concat (id, name, price, vid_sporta, [kol-vo]) like '%" + textBox6.Text + "%'";
            SqlCommand com = new SqlCommand(search, database.GetSqlConnection());
            database.openConnection();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dwg, reader);

            }

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Такого товара нету");
            }


            reader.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void DEl_Click(object sender, EventArgs e)
        {
            string sure = "Вы уверенны?";
            string title = "Вы уверенны? ";

            var result = MessageBox.Show(sure, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                deliterow();
            }
        }

        private void deliterow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;
                return;
            }

            dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearField();
            RefreshDatagrid(dataGridView1);
        }

        private void RED_Click(object sender, EventArgs e)
        {
            change();
        }


        private void update()
        {
            database.openConnection();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var rowState = (RowState)dataGridView1.Rows[i].Cells[5].Value;

                if (rowState == RowState.Existed)
                {

                    continue;
                }

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);

                    var deletequery = $"delete from sport_inv where id = {id}";

                    var command = new SqlCommand(deletequery, database.GetSqlConnection());

                    command.ExecuteNonQuery();

                }

                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    var name = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    var price = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    var vid = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    var kolvo = dataGridView1.Rows[i].Cells[4].Value.ToString();


                    string query = $"update sport_inv set name='{name}', price='{price}', vid_sporta='{vid}', [kol-vo]='{kolvo}' where id='{id}'";

                    var command = new SqlCommand(query, database.GetSqlConnection());

                    command.ExecuteNonQuery();
                }
            }



        }
        private void change()
        {
            var selectedrowsindex = dataGridView1.CurrentCell.RowIndex;

            var id = textBox1.Text;
            var name = textBox2.Text;

            var vid = textBox4.Text;
            var kol = textBox5.Text;

            int price;

            if (dataGridView1.Rows[selectedrowsindex].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBox3.Text, out price))
                {
                    dataGridView1.Rows[selectedrowsindex].SetValues(id, name, price, vid, kol);
                    dataGridView1.Rows[selectedrowsindex].Cells[5].Value = RowState.Modified;
                }
            }
            else
            {
                MessageBox.Show("Цена должна иметь числовой формат!");
            }
        }

        private void sav_Click(object sender, EventArgs e)
        {
            update();
        }

        private void IMP_Click(object sender, EventArgs e)
        {
            int i, j;

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга.
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            ExcelApp.Cells[1, 1] = "id";
            ExcelApp.Cells[1, 2] = "Название";
            ExcelApp.Cells[1, 3] = "Цена";
            ExcelApp.Cells[1, 4] = "Вид спорта";
            ExcelApp.Cells[1, 5] = "Количество";


            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (j = 0; j < dataGridView1.ColumnCount; j++)
                {

                    ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;

                }
            }
            //Вызываем нашу созданную эксельку.
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
                
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Klient klient = new Klient();
            klient.Show();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sortASC(dataGridView1);
        }

        private void button6_Click(object sender, EventArgs e)
        {

            sortDESC(dataGridView1);
        }

        private void sortDESC(DataGridView dwg)
        {
            
                dataGridView1.Rows.Clear();
            string sort = $"SELECT* FROM sport_inv ORDER BY price DESC";
            SqlCommand cmd = new SqlCommand(sort, database.GetSqlConnection());
                database.openConnection();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingleRow(dwg, reader);
                }
                reader.Close();


            
        }

        private void sortASC(DataGridView dwg)
        {

            dataGridView1.Rows.Clear();
            string sort = $"SELECT* FROM sport_inv ORDER BY price ASC";
            SqlCommand cmd = new SqlCommand(sort, database.GetSqlConnection());
            database.openConnection();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dwg, reader);
            }
            reader.Close();



        }
    }
}

