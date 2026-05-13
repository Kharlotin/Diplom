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
    public partial class PrintExportForm: Form
    {

        private int reportId;
        public PrintExportForm(int id)
        {
            InitializeComponent();
            reportId = id;
            LoadReportData();
        }

        private void PrintExportForm_Load(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void LoadReportData()
        {
            try
            {
                Helper.OpenConnection();

                // Заголовок отчёта
                string headerQuery = @"
                    SELECT R.ReportName, R.ReportDate, R.Description, U.FullName 
                    FROM Reports R
                    JOIN Users U ON R.CreatedBy = U.Id
                    WHERE R.Id = @Id";

                SqlCommand cmdHeader = new SqlCommand(headerQuery, Helper.Connection);
                cmdHeader.Parameters.AddWithValue("@Id", reportId);
                SqlDataReader reader = cmdHeader.ExecuteReader();

                if (reader.Read())
                {
                    lblReportName.Text = reader["ReportName"].ToString();
                    lblReportDate.Text = Convert.ToDateTime(reader["ReportDate"]).ToString("dd.MM.yyyy");
                    lblCreatedBy.Text = reader["FullName"].ToString();
                    lblDescription.Text = reader["Description"].ToString();
                }
                reader.Close();

                // Таблица закупок
                string purchasesQuery = @"
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY P.Id) AS [№],
                        OV.Code AS [ОКВЭД],
                        OP.Code AS [ОКПД2],
                        P.ContractSubject AS [Предмет договора],
                        OK.Code AS [ОКЕИ],
                        P.Quantity AS [Количество],
                        P.MaxPrice AS [НМЦК, руб],
                        FORMAT(P.NoticeDate, 'dd.MM.yyyy') AS [Дата извещения],
                        FORMAT(P.ContractEndDate, 'dd.MM.yyyy') AS [Срок исполнения],
                        PM.Name AS [Способ закупки],
                        CASE WHEN P.IsElectronic = 1 THEN 'Да' ELSE 'Нет' END AS [Электронная],
                        P.ApplicationsCount AS [Кол-во заявок],
                        P.ContractSum AS [Сумма договора, руб],
                        P.ContractNumber AS [№ договора]
                    FROM Purchases P
                    JOIN Okved OV ON P.OkvedId = OV.Id
                    JOIN Okpd2 OP ON P.Okpd2Id = OP.Id
                    JOIN Okei OK ON P.OkeiId = OK.Id
                    JOIN PurchaseMethods PM ON P.PurchaseMethodId = PM.Id
                    WHERE P.ReportId = @ReportId
                    ORDER BY P.Id";

                SqlDataAdapter adapter = new SqlDataAdapter(purchasesQuery, Helper.Connection);
                adapter.SelectCommand.Parameters.AddWithValue("@ReportId", reportId);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dgvPrint.DataSource = table;
                dgvPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvPrint.ReadOnly = true;
            }
            finally
            {
                Helper.CloseConnection();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void btnExportWord_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Word документ|*.doc";
            saveDialog.Title = "Экспорт отчёта в Word";
            saveDialog.FileName = $"Отчёт_{lblReportName.Text}_{DateTime.Now:yyyyMMdd}";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToWord(saveDialog.FileName);
            }
        }

        private void ExportToWord(string filePath)
        {
            // Простой экспорт через HTML-разметку
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<head><meta charset='UTF-8'><title>Отчёт</title></head>");
            sb.AppendLine("<body>");
            sb.AppendLine($"<h1>{lblReportName.Text}</h1>");
            sb.AppendLine($"<p><b>Дата создания:</b> {lblReportDate.Text}</p>");
            sb.AppendLine($"<p><b>Создал:</b> {lblCreatedBy.Text}</p>");
            sb.AppendLine($"<p><b>Описание:</b> {lblDescription.Text}</p>");
            sb.AppendLine("<table border='1' cellpadding='5' cellspacing='0'>");

            // Заголовки таблицы
            sb.AppendLine("<tr>");
            foreach (DataGridViewColumn col in dgvPrint.Columns)
            {
                sb.AppendLine($"<th>{col.HeaderText}</th>");
            }
            sb.AppendLine("</tr>");

            // Данные
            foreach (DataGridViewRow row in dgvPrint.Rows)
            {
                if (row.IsNewRow) continue;
                sb.AppendLine("<tr>");
                foreach (DataGridViewCell cell in row.Cells)
                {
                    sb.AppendLine($"<td>{cell.Value}</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body></html>");

            System.IO.File.WriteAllText(filePath, sb.ToString(), System.Text.Encoding.UTF8);
            MessageBox.Show("Отчёт экспортирован в Word.");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
