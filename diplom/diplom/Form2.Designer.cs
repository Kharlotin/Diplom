namespace diplom
{
    partial class navForn
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
            this.crateReportBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.reportListBtn = new System.Windows.Forms.Button();
            this.printReportBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // crateReportBtn
            // 
            this.crateReportBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.crateReportBtn.Location = new System.Drawing.Point(307, 244);
            this.crateReportBtn.Name = "crateReportBtn";
            this.crateReportBtn.Size = new System.Drawing.Size(215, 40);
            this.crateReportBtn.TabIndex = 0;
            this.crateReportBtn.Text = "Создать отчёт";
            this.crateReportBtn.UseVisualStyleBackColor = true;
            this.crateReportBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exitBtn.Location = new System.Drawing.Point(307, 290);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(215, 42);
            this.exitBtn.TabIndex = 1;
            this.exitBtn.Text = "Выход";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.userNameLabel.Location = new System.Drawing.Point(12, 18);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(185, 29);
            this.userNameLabel.TabIndex = 2;
            this.userNameLabel.Text = "Пользователь:";
            // 
            // reportListBtn
            // 
            this.reportListBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.reportListBtn.Location = new System.Drawing.Point(307, 152);
            this.reportListBtn.Name = "reportListBtn";
            this.reportListBtn.Size = new System.Drawing.Size(215, 40);
            this.reportListBtn.TabIndex = 5;
            this.reportListBtn.Text = "Отчёты";
            this.reportListBtn.UseVisualStyleBackColor = true;
            this.reportListBtn.Click += new System.EventHandler(this.reportListBtn_Click);
            // 
            // printReportBtn
            // 
            this.printReportBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.printReportBtn.Location = new System.Drawing.Point(307, 198);
            this.printReportBtn.Name = "printReportBtn";
            this.printReportBtn.Size = new System.Drawing.Size(215, 40);
            this.printReportBtn.TabIndex = 6;
            this.printReportBtn.Text = "Печать/Экспорт";
            this.printReportBtn.UseVisualStyleBackColor = true;
            this.printReportBtn.Click += new System.EventHandler(this.printReportBtn_Click);
            // 
            // navForn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.printReportBtn);
            this.Controls.Add(this.reportListBtn);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.crateReportBtn);
            this.Name = "navForn";
            this.Text = "Навигация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button crateReportBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Button reportListBtn;
        private System.Windows.Forms.Button printReportBtn;
    }
}