using diplom.Classes;
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

namespace diplom
{
    public partial class EditReportForm: Form
    {
        private int? reportId = null;

        public EditReportForm()
        {
            InitializeComponent();
        }

        public EditReportForm(int id)
        {
            InitializeComponent();
            reportId = id;
        }

        private void ConfigureAccess()
        {
            bool readOnly = CurrentUser.RoleId == 2 || CurrentUser.RoleId == 3;

            txtReportName.ReadOnly = readOnly;
            txtDescription.ReadOnly = readOnly;
            dtpReportDate.Enabled = !readOnly;

            dgvPurchases.ReadOnly = readOnly;
            dgvPurchases.AllowUserToAddRows = false;
            dgvPurchases.AllowUserToDeleteRows = !readOnly;

            btnAddRow.Visible = !readOnly;
            btnDeleteRow.Visible = !readOnly;
            btnSave.Visible = !readOnly;
            btnPrintExport.Visible = reportId.HasValue;
        }

        private void DgvPurchases_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPurchases.IsCurrentCellDirty)
            {
                dgvPurchases.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DgvPurchases_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (dgvPurchases.Columns[e.ColumnIndex].Name == "MaxPrice" || dgvPurchases.Columns[e.ColumnIndex].Name == "ContractSum"))
            {
                DataGridViewRow row = dgvPurchases.Rows[e.RowIndex];
                decimal maxPrice = 0, contractSum = 0;

                if (row.Cells["MaxPrice"].Value != null && row.Cells["MaxPrice"].Value != DBNull.Value)
                    decimal.TryParse(row.Cells["MaxPrice"].Value.ToString(), out maxPrice);

                if (row.Cells["ContractSum"].Value != null && row.Cells["ContractSum"].Value != DBNull.Value)
                    decimal.TryParse(row.Cells["ContractSum"].Value.ToString(), out contractSum);

                decimal savings = maxPrice - contractSum;
                if (savings < 0) savings = 0;

                row.Cells["Savings"].Value = savings;
            }
        }

        private DataTable GetDataTable(string query)
        {
            DataTable table = new DataTable();
            try
            {
                Helper.OpenConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(query, Helper.Connection);
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки справочника:\n" + ex.Message);
            }
            finally
            {
                Helper.CloseConnection();
            }
            return table;
        }

        private void EditReportForm_Load(object sender, EventArgs e)
        {
            dtpReportDate.Value = DateTime.Today;
            ConfigureAccess();
            SetupGridView();

            if (reportId.HasValue)
            {
                LoadReport();
                LoadPurchases();
            }
            else
            {
                // Для нового отчёта создаю пустую таблицу
                CreateEmptyPurchasesTable();
            }
        }

        private void LoadReport()
        {
            try
            {
                Helper.OpenConnection();
                string query = "SELECT ReportName, ReportDate, Description FROM Reports WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, Helper.Connection);
                cmd.Parameters.AddWithValue("@Id", reportId.Value);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtReportName.Text = reader["ReportName"].ToString();
                    dtpReportDate.Value = Convert.ToDateTime(reader["ReportDate"]);
                    txtDescription.Text = reader["Description"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки отчёта:\n" + ex.Message);
            }
            finally
            {
                Helper.CloseConnection();
            }
        }

        private void LoadPurchases()
        {
            try
            {
                Helper.OpenConnection();
                string query = @"
                    SELECT 
                        Id,
                        OkvedId,
                        Okpd2Id,
                        ContractSubject,
                        OkeiId,
                        Quantity,
                        MaxPrice,
                        NoticeDate,
                        ContractEndDate,
                        PurchaseMethodId,
                        IsElectronic,
                        ApplicationsCount,
                        ContractSum,
                        ContractNumber
                    FROM Purchases 
                    WHERE ReportId = @ReportId
                    ORDER BY Id";

                SqlCommand cmd = new SqlCommand(query, Helper.Connection);
                cmd.Parameters.AddWithValue("@ReportId", reportId.Value);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dgvPurchases.DataSource = table;

                SetupGridView();
            }
            finally
            {
                Helper.CloseConnection();
            }
        }

        private void CreateEmptyPurchasesTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("OkvedId", typeof(int));
            table.Columns.Add("Okpd2Id", typeof(int));
            table.Columns.Add("ContractSubject", typeof(string));
            table.Columns.Add("OkeiId", typeof(int));
            table.Columns.Add("Quantity", typeof(decimal));
            table.Columns.Add("MaxPrice", typeof(decimal));
            table.Columns.Add("NoticeDate", typeof(DateTime));
            table.Columns.Add("ContractEndDate", typeof(DateTime));
            table.Columns.Add("PurchaseMethodId", typeof(int));
            table.Columns.Add("IsElectronic", typeof(bool));
            table.Columns.Add("ApplicationsCount", typeof(int));
            table.Columns.Add("ContractSum", typeof(decimal));
            table.Columns.Add("ContractNumber", typeof(string));

            dgvPurchases.DataSource = table;
        }

        private void SetupGridView()
        {
            
            dgvPurchases.AutoGenerateColumns = false;
            dgvPurchases.Columns.Clear();

           
            var idCol = new DataGridViewTextBoxColumn();
            idCol.Name = "Id";
            idCol.DataPropertyName = "Id";
            idCol.Visible = false;
            dgvPurchases.Columns.Add(idCol);

          
            var okvedCol = new DataGridViewComboBoxColumn();
            okvedCol.Name = "OkvedId";
            okvedCol.HeaderText = "Код по ОКВЭД";
            okvedCol.DataPropertyName = "OkvedId";
            okvedCol.DataSource = GetDataTable("SELECT Id, Code + ' - ' + Name AS Display FROM Okved ORDER BY Code");
            okvedCol.DisplayMember = "Display";
            okvedCol.ValueMember = "Id";
            okvedCol.Width = 150;
            dgvPurchases.Columns.Add(okvedCol);

            var okpd2Col = new DataGridViewComboBoxColumn();
            okpd2Col.Name = "Okpd2Id";
            okpd2Col.HeaderText = "Код по ОКПД 2";
            okpd2Col.DataPropertyName = "Okpd2Id";
            okpd2Col.DataSource = GetDataTable("SELECT Id, Code + ' - ' + Name AS Display FROM Okpd2 ORDER BY Code");
            okpd2Col.DisplayMember = "Display";
            okpd2Col.ValueMember = "Id";
            okpd2Col.Width = 150;
            dgvPurchases.Columns.Add(okpd2Col);

            var subjectCol = new DataGridViewTextBoxColumn();
            subjectCol.Name = "ContractSubject";
            subjectCol.HeaderText = "Предмет договора";
            subjectCol.DataPropertyName = "ContractSubject";
            subjectCol.Width = 200;
            dgvPurchases.Columns.Add(subjectCol);

            var okeiCol = new DataGridViewComboBoxColumn();
            okeiCol.Name = "OkeiId";
            okeiCol.HeaderText = "Ед. измерения";
            okeiCol.DataPropertyName = "OkeiId";
            okeiCol.DataSource = GetDataTable("SELECT Id, Code + ' - ' + Name AS Display FROM Okei ORDER BY Code");
            okeiCol.DisplayMember = "Display";
            okeiCol.ValueMember = "Id";
            okeiCol.Width = 120;
            dgvPurchases.Columns.Add(okeiCol);

            var quantityCol = new DataGridViewTextBoxColumn();
            quantityCol.Name = "Quantity";
            quantityCol.HeaderText = "Количество";
            quantityCol.DataPropertyName = "Quantity";
            quantityCol.DefaultCellStyle.Format = "N3";
            quantityCol.Width = 100;
            dgvPurchases.Columns.Add(quantityCol);

            var maxPriceCol = new DataGridViewTextBoxColumn();
            maxPriceCol.Name = "MaxPrice";
            maxPriceCol.HeaderText = "НМЦК, руб";
            maxPriceCol.DataPropertyName = "MaxPrice";
            maxPriceCol.DefaultCellStyle.Format = "N2";
            maxPriceCol.Width = 120;
            dgvPurchases.Columns.Add(maxPriceCol);

            var noticeCol = new DataGridViewTextBoxColumn();
            noticeCol.Name = "NoticeDate";
            noticeCol.HeaderText = "Дата извещения";
            noticeCol.DataPropertyName = "NoticeDate";
            noticeCol.DefaultCellStyle.Format = "dd.MM.yyyy";
            noticeCol.Width = 100;
            dgvPurchases.Columns.Add(noticeCol);

            var endCol = new DataGridViewTextBoxColumn();
            endCol.Name = "ContractEndDate";
            endCol.HeaderText = "Срок исполнения";
            endCol.DataPropertyName = "ContractEndDate";
            endCol.DefaultCellStyle.Format = "dd.MM.yyyy";
            endCol.Width = 100;
            dgvPurchases.Columns.Add(endCol);

            var methodCol = new DataGridViewComboBoxColumn();
            methodCol.Name = "PurchaseMethodId";
            methodCol.HeaderText = "Способ закупки";
            methodCol.DataPropertyName = "PurchaseMethodId";
            methodCol.DataSource = GetDataTable("SELECT Id, Name FROM PurchaseMethods ORDER BY Name");
            methodCol.DisplayMember = "Name";
            methodCol.ValueMember = "Id";
            methodCol.Width = 150;
            dgvPurchases.Columns.Add(methodCol);

            var electronicCol = new DataGridViewCheckBoxColumn();
            electronicCol.Name = "IsElectronic";
            electronicCol.HeaderText = "Электронная";
            electronicCol.DataPropertyName = "IsElectronic";
            electronicCol.Width = 80;
            dgvPurchases.Columns.Add(electronicCol);

            var appsCol = new DataGridViewTextBoxColumn();
            appsCol.Name = "ApplicationsCount";
            appsCol.HeaderText = "Кол-во заявок";
            appsCol.DataPropertyName = "ApplicationsCount";
            appsCol.Width = 80;
            dgvPurchases.Columns.Add(appsCol);

            var sumCol = new DataGridViewTextBoxColumn();
            sumCol.Name = "ContractSum";
            sumCol.HeaderText = "Сумма договора, руб";
            sumCol.DataPropertyName = "ContractSum";
            sumCol.DefaultCellStyle.Format = "N2";
            sumCol.Width = 120;
            dgvPurchases.Columns.Add(sumCol);

            var numberCol = new DataGridViewTextBoxColumn();
            numberCol.Name = "ContractNumber";
            numberCol.HeaderText = "№ договора";
            numberCol.DataPropertyName = "ContractNumber";
            numberCol.Width = 120;
            dgvPurchases.Columns.Add(numberCol);

            dgvPurchases.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvPurchases.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPurchases.AllowUserToAddRows = false;
            dgvPurchases.RowHeadersVisible = false;
        }

        private void txtReportName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReportName.Text))
            {
                MessageBox.Show("Введите название отчёта.");
                return;
            }

            try
            {
                Helper.OpenConnection();
                SqlTransaction transaction = Helper.Connection.BeginTransaction();

                try
                {
                    // Сохранение шапки
                    if (!reportId.HasValue)
                    {
                        string insertReport = @"
                            INSERT INTO Reports (ReportName, ReportDate, CreatedBy, Description)
                            VALUES (@ReportName, @ReportDate, @CreatedBy, @Description);
                            SELECT SCOPE_IDENTITY();";

                        SqlCommand cmd = new SqlCommand(insertReport, Helper.Connection, transaction);
                        cmd.Parameters.AddWithValue("@ReportName", txtReportName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ReportDate", dtpReportDate.Value.Date);
                        cmd.Parameters.AddWithValue("@CreatedBy", CurrentUser.Id);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(txtDescription.Text) ? (object)DBNull.Value : txtDescription.Text.Trim());
                        reportId = Convert.ToInt32(cmd.ExecuteScalar());
                        btnPrintExport.Visible = true;
                    }
                    else
                    {
                        string updateReport = @"
                            UPDATE Reports 
                            SET ReportName = @ReportName, ReportDate = @ReportDate, Description = @Description
                            WHERE Id = @Id";

                        SqlCommand cmd = new SqlCommand(updateReport, Helper.Connection, transaction);
                        cmd.Parameters.AddWithValue("@Id", reportId.Value);
                        cmd.Parameters.AddWithValue("@ReportName", txtReportName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ReportDate", dtpReportDate.Value.Date);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(txtDescription.Text) ? (object)DBNull.Value : txtDescription.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }

                    // Удаление старых позиций
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM Purchases WHERE ReportId = @ReportId", Helper.Connection, transaction);
                    deleteCmd.Parameters.AddWithValue("@ReportId", reportId.Value);
                    deleteCmd.ExecuteNonQuery();

                    // Вставка новых позиций
                    DataTable table = dgvPurchases.DataSource as DataTable;
                    if (table != null && table.Rows.Count > 0)
                    {
                        string insertPurchase = @"
                            INSERT INTO Purchases 
                            (ReportId, OkvedId, Okpd2Id, ContractSubject, OkeiId, Quantity, MaxPrice, 
                             NoticeDate, ContractEndDate, PurchaseMethodId, IsElectronic, 
                             ApplicationsCount, ContractSum, ContractNumber, CreatedBy)
                            VALUES 
                            (@ReportId, @OkvedId, @Okpd2Id, @ContractSubject, @OkeiId, @Quantity, @MaxPrice,
                             @NoticeDate, @ContractEndDate, @PurchaseMethodId, @IsElectronic,
                             @ApplicationsCount, @ContractSum, @ContractNumber, @CreatedBy)";

                        foreach (DataRow row in table.Rows)
                        {
                            // Проверяем, что предмет договора не пустой
                            string subject = row["ContractSubject"]?.ToString();
                            if (string.IsNullOrWhiteSpace(subject)) continue;

                            decimal maxPrice = 0, contractSum = 0;
                            if (row["MaxPrice"] != null && row["MaxPrice"] != DBNull.Value)
                                decimal.TryParse(row["MaxPrice"].ToString(), out maxPrice);
                            if (row["ContractSum"] != null && row["ContractSum"] != DBNull.Value)
                                decimal.TryParse(row["ContractSum"].ToString(), out contractSum);

                            decimal savings = maxPrice - contractSum;
                            if (savings < 0) savings = 0;

                            SqlCommand insertCmd = new SqlCommand(insertPurchase, Helper.Connection, transaction);
                            insertCmd.Parameters.AddWithValue("@ReportId", reportId.Value);
                            insertCmd.Parameters.AddWithValue("@OkvedId", row["OkvedId"] ?? DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@Okpd2Id", row["Okpd2Id"] ?? DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@ContractSubject", subject);
                            insertCmd.Parameters.AddWithValue("@OkeiId", row["OkeiId"] ?? DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@Quantity", row["Quantity"] ?? DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@MaxPrice", maxPrice);
                            insertCmd.Parameters.AddWithValue("@NoticeDate", row["NoticeDate"] ?? DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@ContractEndDate", row["ContractEndDate"] ?? DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@PurchaseMethodId", row["PurchaseMethodId"] ?? DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@IsElectronic", row["IsElectronic"] ?? false);
                            insertCmd.Parameters.AddWithValue("@ApplicationsCount", row["ApplicationsCount"] ?? DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@ContractSum", contractSum);
                            insertCmd.Parameters.AddWithValue("@ContractNumber", row["ContractNumber"] ?? DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@CreatedBy", CurrentUser.Id);
                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show("Отчёт успешно сохранён.");
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ошибка при сохранении:\n" + ex.Message);
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения:\n" + ex.Message);
            }
            finally
            {
                Helper.CloseConnection();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            DataTable table = dgvPurchases.DataSource as DataTable;
            if (table == null)
            {
                CreateEmptyPurchasesTable();
                table = dgvPurchases.DataSource as DataTable;
            }

            DataRow newRow = table.NewRow();

            // Устанавливаем значения по умолчанию
            newRow["IsElectronic"] = false;
            newRow["NoticeDate"] = DateTime.Today;
            newRow["ContractEndDate"] = DateTime.Today.AddMonths(1);

            table.Rows.Add(newRow);
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (dgvPurchases.SelectedRows.Count > 0)
            {
                DataTable table = dgvPurchases.DataSource as DataTable;
                if (table != null)
                {
                    foreach (DataGridViewRow row in dgvPurchases.SelectedRows)
                    {
                        if (!row.IsNewRow && row.DataBoundItem != null)
                        {
                            DataRowView rowView = row.DataBoundItem as DataRowView;
                            if (rowView != null)
                            {
                                table.Rows.Remove(rowView.Row);
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (reportId.HasValue)
            {
                MessageBox.Show("Печать и экспорт отчёта ID: " + reportId.Value);
                PrintExportForm printForm = new PrintExportForm(reportId.Value);
                printForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Сначала сохраните отчёт.");
            }
        }
    }
}
