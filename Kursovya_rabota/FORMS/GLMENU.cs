using Kursovya_rabota.FORMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovya_rabota
{
    public partial class GLMENU : Form
    {
        public GLMENU()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SptotINV iNV = new SptotINV();
            iNV.Dostup("Пользователь");
            iNV.ShowDialog();
        }
    }
}
