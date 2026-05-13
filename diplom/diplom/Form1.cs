using System;
using diplom.Classes;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace diplom
{
    public partial class authForm: Form
    {
        public authForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void enterLogBtn_Click(object sender, EventArgs e)
        {
            if (Authorize.CheckUser(loginTxtBox.Text, psswdTxtBox.Text))
            {
                navForn main = new navForn();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
