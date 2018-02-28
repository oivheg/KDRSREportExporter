namespace KDRsReportExporter
{
    partial class FormReportExporter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.comboBoxDBs = new System.Windows.Forms.ComboBox();
            this.labelDBNames = new System.Windows.Forms.Label();
            this.labelSPs = new System.Windows.Forms.Label();
            this.comboBoxSPs = new System.Windows.Forms.ComboBox();
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.txtReportName = new System.Windows.Forms.TextBox();
            this.lblReportName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(38, 297);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(515, 267);
            this.dataGridView.TabIndex = 0;
            // 
            // comboBoxDBs
            // 
            this.comboBoxDBs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDBs.FormattingEnabled = true;
            this.comboBoxDBs.Location = new System.Drawing.Point(38, 26);
            this.comboBoxDBs.Name = "comboBoxDBs";
            this.comboBoxDBs.Size = new System.Drawing.Size(234, 21);
            this.comboBoxDBs.TabIndex = 1;
            this.comboBoxDBs.SelectedIndexChanged += new System.EventHandler(this.comboBoxDBs_SelectedIndexChanged);
            // 
            // labelDBNames
            // 
            this.labelDBNames.AutoSize = true;
            this.labelDBNames.Location = new System.Drawing.Point(35, 10);
            this.labelDBNames.Name = "labelDBNames";
            this.labelDBNames.Size = new System.Drawing.Size(56, 13);
            this.labelDBNames.TabIndex = 2;
            this.labelDBNames.Text = "Databaser";
            // 
            // labelSPs
            // 
            this.labelSPs.AutoSize = true;
            this.labelSPs.Location = new System.Drawing.Point(305, 10);
            this.labelSPs.Name = "labelSPs";
            this.labelSPs.Size = new System.Drawing.Size(94, 13);
            this.labelSPs.TabIndex = 4;
            this.labelSPs.Text = "Stored procedures";
          
            // 
            // comboBoxSPs
            // 
            this.comboBoxSPs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSPs.FormattingEnabled = true;
            this.comboBoxSPs.Location = new System.Drawing.Point(308, 26);
            this.comboBoxSPs.Name = "comboBoxSPs";
            this.comboBoxSPs.Size = new System.Drawing.Size(245, 21);
            this.comboBoxSPs.TabIndex = 3;
            this.comboBoxSPs.SelectedIndexChanged += new System.EventHandler(this.comboBoxSPs_SelectedIndexChanged);
            this.comboBoxSPs.SelectionChangeCommitted += new System.EventHandler(this.comboBoxSPs_SelectionChangeCommitted);
            // 
            // groupBoxParameters
            // 
            this.groupBoxParameters.Location = new System.Drawing.Point(38, 53);
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.Size = new System.Drawing.Size(515, 169);
            this.groupBoxParameters.TabIndex = 5;
            this.groupBoxParameters.TabStop = false;
            this.groupBoxParameters.Text = "Parameters";
           
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(194, 239);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(132, 31);
            this.btnExportPDF.TabIndex = 9;
            this.btnExportPDF.Text = "Exporter Til PDF";
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(38, 239);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(132, 31);
            this.buttonExecute.TabIndex = 8;
            this.buttonExecute.Text = "HentData";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // txtReportName
            // 
            this.txtReportName.Location = new System.Drawing.Point(344, 250);
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.Size = new System.Drawing.Size(209, 20);
            this.txtReportName.TabIndex = 10;
            this.txtReportName.TextChanged += new System.EventHandler(this.txtReportName_TextChanged);
            // 
            // lblReportName
            // 
            this.lblReportName.AutoSize = true;
            this.lblReportName.Location = new System.Drawing.Point(341, 234);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(67, 13);
            this.lblReportName.TabIndex = 11;
            this.lblReportName.Text = "ReportName";
            this.lblReportName.Click += new System.EventHandler(this.lblReportName_Click);
            // 
            // FormReportExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 589);
            this.Controls.Add(this.lblReportName);
            this.Controls.Add(this.txtReportName);
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.groupBoxParameters);
            this.Controls.Add(this.labelSPs);
            this.Controls.Add(this.comboBoxSPs);
            this.Controls.Add(this.labelDBNames);
            this.Controls.Add(this.comboBoxDBs);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormReportExporter";
            this.Text = "FormReportExporter";
            this.Load += new System.EventHandler(this.FormReportExporter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox comboBoxDBs;
        private System.Windows.Forms.Label labelDBNames;
        private System.Windows.Forms.Label labelSPs;
        private System.Windows.Forms.ComboBox comboBoxSPs;
        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.TextBox txtReportName;
        private System.Windows.Forms.Label lblReportName;
    }
}

