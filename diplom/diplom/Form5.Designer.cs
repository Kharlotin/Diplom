namespace diplom
{
    partial class PrintExportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblReportName = new System.Windows.Forms.Label();
            this.lblReportDate = new System.Windows.Forms.Label();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.dgvPrint = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExportWord = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.CloseBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReportName
            // 
            this.lblReportName.AutoSize = true;
            this.lblReportName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblReportName.Location = new System.Drawing.Point(12, 9);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(109, 25);
            this.lblReportName.TabIndex = 0;
            this.lblReportName.Text = "Название";
            // 
            // lblReportDate
            // 
            this.lblReportDate.AutoSize = true;
            this.lblReportDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblReportDate.Location = new System.Drawing.Point(12, 34);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Size = new System.Drawing.Size(62, 25);
            this.lblReportDate.TabIndex = 1;
            this.lblReportDate.Text = "Дата";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCreatedBy.Location = new System.Drawing.Point(12, 59);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(98, 25);
            this.lblCreatedBy.TabIndex = 2;
            this.lblCreatedBy.Text = "Создано";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDescription.Location = new System.Drawing.Point(251, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(111, 25);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Описание";
            // 
            // dgvPrint
            // 
            this.dgvPrint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrint.Location = new System.Drawing.Point(13, 159);
            this.dgvPrint.Name = "dgvPrint";
            this.dgvPrint.Size = new System.Drawing.Size(1176, 532);
            this.dgvPrint.TabIndex = 4;
            this.dgvPrint.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrint.Location = new System.Drawing.Point(913, 22);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(135, 48);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "Печать";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExportWord
            // 
            this.btnExportWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExportWord.Location = new System.Drawing.Point(1054, 22);
            this.btnExportWord.Name = "btnExportWord";
            this.btnExportWord.Size = new System.Drawing.Size(135, 48);
            this.btnExportWord.TabIndex = 6;
            this.btnExportWord.Text = "Экспорт";
            this.btnExportWord.UseVisualStyleBackColor = true;
            this.btnExportWord.Click += new System.EventHandler(this.btnExportWord_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CloseBtn.Location = new System.Drawing.Point(1054, 76);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(135, 48);
            this.CloseBtn.TabIndex = 7;
            this.CloseBtn.Text = "Выход";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // PrintExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 727);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.btnExportWord);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgvPrint);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCreatedBy);
            this.Controls.Add(this.lblReportDate);
            this.Controls.Add(this.lblReportName);
            this.Name = "PrintExportForm";
            this.Text = "Печать/Экспорт";
            this.Load += new System.EventHandler(this.PrintExportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReportName;
        private System.Windows.Forms.Label lblReportDate;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.DataGridView dgvPrint;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExportWord;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button CloseBtn;
    }
}