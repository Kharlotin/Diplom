using System;
using System.Data;
using System.Data.SqlClient;
using diplom.Classes;
using System.Windows.Forms;

namespace diplom
{
    public partial class reportForm: Form
    {
        public reportForm()
        {
            InitializeComponent();
            ConfigureAccess();
        }

        private void ConfigureAccess()
        {
            if (CurrentUser.RoleId == 2 || CurrentUser.RoleId == 3)
            {
                btnAdd.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void LoadReports()
        {
            Helper.OpenConnection();

            string query = @"SELECT 
                             Id,
                             ContractSubject,
                             Quantity,
                             MaxPrice,
                             ContractNumber
                             FROM Purchases";

            SqlDataAdapter adapter = new SqlDataAdapter(query, Helper.Connection);

            DataTable table = new DataTable();

            adapter.Fill(table);

            dgvReports.DataSource = table;

            dgvReports.Columns["Id"].Visible = false;

            dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReports.ReadOnly = true;
            dgvReports.AllowUserToAddRows = false;

            Helper.CloseConnection();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            LoadReports();
        }

        private void crateForm_Load(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReports();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            reportForm form = new reportForm();
            form.ShowDialog();

            LoadReports();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvReports.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvReports.SelectedRows[0].Cells["Id"].Value);

                Helper.OpenConnection();

                string query = "DELETE FROM Purchases WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, Helper.Connection);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                Helper.CloseConnection();

                LoadReports();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления");
            }
        }
    }
}
