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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReportExporter));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.comboBoxDBs = new System.Windows.Forms.ComboBox();
            this.labelDBNames = new System.Windows.Forms.Label();
            this.labelSPs = new System.Windows.Forms.Label();
            this.comboBoxSPs = new System.Windows.Forms.ComboBox();
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.filterbox = new System.Windows.Forms.GroupBox();
            this.lblExport = new System.Windows.Forms.Label();
            this.btnexportEXCEL = new System.Windows.Forms.Button();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.lblReportName = new System.Windows.Forms.Label();
            this.txtReportName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.filterbox.SuspendLayout();
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
            this.dataGridView.Location = new System.Drawing.Point(38, 366);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(564, 427);
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
            this.groupBoxParameters.AutoSize = true;
            this.groupBoxParameters.Location = new System.Drawing.Point(38, 53);
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.Size = new System.Drawing.Size(567, 169);
            this.groupBoxParameters.TabIndex = 5;
            this.groupBoxParameters.TabStop = false;
            this.groupBoxParameters.Text = "Parameters";
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(15, 22);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(132, 63);
            this.buttonExecute.TabIndex = 8;
            this.buttonExecute.Text = "HentData";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // filterbox
            // 
            this.filterbox.AutoSize = true;
            this.filterbox.Controls.Add(this.lblExport);
            this.filterbox.Controls.Add(this.buttonExecute);
            this.filterbox.Controls.Add(this.btnexportEXCEL);
            this.filterbox.Controls.Add(this.btnExportPDF);
            this.filterbox.Controls.Add(this.lblReportName);
            this.filterbox.Controls.Add(this.txtReportName);
            this.filterbox.Location = new System.Drawing.Point(38, 244);
            this.filterbox.Name = "filterbox";
            this.filterbox.Size = new System.Drawing.Size(570, 116);
            this.filterbox.TabIndex = 6;
            this.filterbox.TabStop = false;
            // 
            // lblExport
            // 
            this.lblExport.AutoSize = true;
            this.lblExport.Location = new System.Drawing.Point(219, 16);
            this.lblExport.Name = "lblExport";
            this.lblExport.Size = new System.Drawing.Size(55, 13);
            this.lblExport.TabIndex = 18;
            this.lblExport.Text = "Eksporter:";
            // 
            // btnexportEXCEL
            // 
            this.btnexportEXCEL.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnexportEXCEL.BackgroundImage")));
            this.btnexportEXCEL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnexportEXCEL.Location = new System.Drawing.Point(260, 33);
            this.btnexportEXCEL.Name = "btnexportEXCEL";
            this.btnexportEXCEL.Size = new System.Drawing.Size(78, 52);
            this.btnexportEXCEL.TabIndex = 17;
            this.btnexportEXCEL.UseVisualStyleBackColor = true;
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExportPDF.BackgroundImage")));
            this.btnExportPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExportPDF.Location = new System.Drawing.Point(176, 33);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(78, 52);
            this.btnExportPDF.TabIndex = 14;
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click_1);
            // 
            // lblReportName
            // 
            this.lblReportName.AutoSize = true;
            this.lblReportName.Location = new System.Drawing.Point(382, 38);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(114, 13);
            this.lblReportName.TabIndex = 16;
            this.lblReportName.Text = "ReportName (ValgFritt)";
            // 
            // txtReportName
            // 
            this.txtReportName.Location = new System.Drawing.Point(371, 65);
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.Size = new System.Drawing.Size(193, 20);
            this.txtReportName.TabIndex = 15;
            // 
            // FormReportExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 818);
            this.Controls.Add(this.filterbox);
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
            this.filterbox.ResumeLayout(false);
            this.filterbox.PerformLayout();
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
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.GroupBox filterbox;
        private System.Windows.Forms.Label lblExport;
        private System.Windows.Forms.Button btnexportEXCEL;
        private System.Windows.Forms.Label lblReportName;
        private System.Windows.Forms.TextBox txtReportName;
        private System.Windows.Forms.Button btnExportPDF;
    }
}

