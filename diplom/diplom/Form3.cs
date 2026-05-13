using System;
using System.Data;
using System.Data.SqlClient;
using diplom.Classes;
using System.Windows.Forms;

namespace diplom
{
    public partial class ReportsForm: Form
    {
        public ReportsForm()
        {
            InitializeComponent();
            ConfigureAccess();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            dtpDateFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDateTo.Value = DateTime.Today;

            LoadReports();
        }

        private void ConfigureAccess()
        {
            if (CurrentUser.RoleId == 2 || CurrentUser.RoleId == 3)
            {
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                btnEdit.Visible = false;
                btnOpen.Visible = true;
            }
            else
            {
                btnOpen.Visible = true;
            }
        }

        private int GetSelectedReportId()
        {
            if (dgvReports.SelectedRows.Count == 0)
                return -1;

            return Convert.ToInt32(dgvReports.SelectedRows[0].Cells["Код"].Value);
        }

        private void LoadReports(DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            try
            {
                Helper.OpenConnection();

                string query = @"
            SELECT
                R.Id AS [Код],
                R.ReportName AS [Название отчёта],
                R.ReportDate AS [Дата создания],
                U.FullName AS [Создал],
                R.Description AS [Описание]
            FROM Reports R
            INNER JOIN Users U ON R.CreatedBy = U.Id
            WHERE 1 = 1";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Helper.Connection;

                if (dateFrom.HasValue)
                {
                    query += " AND R.ReportDate >= @DateFrom";
                    cmd.Parameters.AddWithValue("@DateFrom", dateFrom.Value.Date);
                }

                if (dateTo.HasValue)
                {
                    query += " AND R.ReportDate < @DateTo";
                    cmd.Parameters.AddWithValue(
                        "@DateTo",
                        dateTo.Value.Date.AddDays(1));
                }

                query += " ORDER BY R.ReportDate DESC, R.Id DESC";

                cmd.CommandText = query;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dgvReports.DataSource = table;

                if (dgvReports.Columns.Contains("Код"))
                    dgvReports.Columns["Код"].Visible = false;

                dgvReports.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
                dgvReports.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;
                dgvReports.MultiSelect = false;
                dgvReports.ReadOnly = true;
                dgvReports.AllowUserToAddRows = false;
                dgvReports.RowHeadersVisible = false;
            }
            finally
            {
                Helper.CloseConnection();
            }
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
            EditReportForm form = new EditReportForm();
            form.ShowDialog();

            LoadReports();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int reportId = GetSelectedReportId();

            if (reportId == -1)
            {
                MessageBox.Show("Выберите отчёт для удаления.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить выбранный отчёт?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                Helper.OpenConnection();

                string query = "DELETE FROM Reports WHERE Id = @Id";

                SqlCommand cmd =
                    new SqlCommand(query, Helper.Connection);
                cmd.Parameters.AddWithValue("@Id", reportId);

                cmd.ExecuteNonQuery();

                LoadReports();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка удаления:\n" + ex.Message);
            }
            finally
            {
                Helper.CloseConnection();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int reportId = GetSelectedReportId();

            if (reportId == -1)
            {
                MessageBox.Show("Выберите отчёт.");
                return;
            }

            EditReportForm form = new EditReportForm(reportId);
            form.ShowDialog();

            LoadReports();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadReports(dtpDateFrom.Value, dtpDateTo.Value);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadReports();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenSelectedReport();
        }

        private void OpenSelectedReport()
        {
            int reportId = GetSelectedReportId();

            if (reportId == -1)
            {
                MessageBox.Show("Выберите отчёт для открытия.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            EditReportForm form = new EditReportForm(reportId);
            form.ShowDialog();
        }
    }
}
