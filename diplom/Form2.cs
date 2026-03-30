using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using diplom.Classes;
using System.Windows.Forms;

namespace diplom
{
    public partial class navForn: Form
    {
        public navForn()
        {
            InitializeComponent();

            userNameLabel.Text = "Пользователь: " + CurrentUser.FullName;

            ConfigureAccess();
        }

        private void ConfigureAccess()
        {
            if (CurrentUser.RoleId == 2 || CurrentUser.RoleId == 3)
            {
                crateReportBtn.Visible = false;
                usersListBtn.Visible = false;
                directoriesBtn.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void reportListBtn_Click(object sender, EventArgs e)
        {
            reportForm reports = new reportForm();
            reports.Show();
        }
    }
}
