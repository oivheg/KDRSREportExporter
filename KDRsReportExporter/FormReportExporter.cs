using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KDRsReportExporter
{
    public partial class FormReportExporter : Form
    {
        public FormReportExporter()
        {
            InitializeComponent();
        }

        private ExporterFactory exp_Factory;
        private String DBName;
        private String SpName;
        private DataTable dt;

        private void FormReportExporter_Load(object sender, EventArgs e)
        {
            PasswordForm frm = new PasswordForm();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                // The user canceled.
                this.Close();
            }

            btnExportPDF.Enabled = false;
            btnexportEXCEL.Enabled = false;
            exp_Factory = new ExporterFactory(true);
            comboBoxDBs.DataSource = GetDBNames();
            //exp_Factory.setDate(DateTime.Now.AddDays(-1), DateTime.Now);
            //DataTable dt = exp_Factory.GetData();
            //dataGridView.DataSource = dt;
            //string fileName = exp_Factory.ExportToPDF(dt);
            //exp_Factory.SendEmail(fileName);
            // Display the password form.
        }

        public List<String[]> RunSelectStatement(string Query, string[] ColumnNames)
        {
            List<string[]> result = new List<string[]>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = Query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = exp_Factory.conn;
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    decimal number;
                    if (Decimal.TryParse(Convert.ToString(reader[ColumnNames[0]]), out number))
                    {
                        decimal d = Convert.ToDecimal(reader[ColumnNames[0]]);
                    }
                    // It's a decimal
                    else
                    {
                        if (ColumnNames.Length >= 2)
                        {
                            string[] tmpstring = { Convert.ToString(reader[ColumnNames[0]]), Convert.ToString(reader[ColumnNames[1]]) };
                            result.Add(tmpstring);
                        }
                        else
                        {
                            string[] tmpstr2 = { Convert.ToString(reader[ColumnNames[0]]) };
                            result.Add(tmpstr2);
                        }
                    }
                }
            }
            return result;
        }

        public List<String> GetDBNames()
        {
            string[] ColumnNames = { "name" };
            List<String[]> tmpList = RunSelectStatement("SELECT name FROM master.dbo.sysdatabases " +
                "WHERE name NOT IN('master', 'tempdb', 'model', 'msdb') ORDER By name; ", ColumnNames);

            return TempLsit(tmpList);
        }

        private static List<string> TempLsit(List<string[]> tmpList)
        {
            List<String> tempListReturn = new List<string>();
            foreach (String[] Values in tmpList)
            {
                tempListReturn.Add(Values[0]);
            }

            return tempListReturn;
        }

        public List<String> GetSPNames()
        {
            string[] ColumnNames = { "specific_name" };
            List<String[]> tmpList = RunSelectStatement("select specific_name from " + DBName + ".information_schema.routines " +
                "where routine_type = 'PROCEDURE' and specific_name like 'exp[_]%' ORDER BY 1", ColumnNames);
            return TempLsit(tmpList);
        }

        public void FillStoredProcedures()
        {
            comboBoxSPs.DataSource = null;
            comboBoxSPs.DataSource = GetSPNames();
        }

        public void ParametersFactory()
        {
            string[] ColumnNames = { "name", "Type" };
            List<string[]> parameters = RunSelectStatement("use " + DBName + "; " +
                 "select name, 'Type' = type_name(user_type_id) " +
                 "from sys.parameters where object_id = object_id('dbo." + SpName + "') " +
                 "ORDER BY parameter_id", ColumnNames);

            groupBoxParameters.Controls.Clear();
            int paramsCount = 0;
            int xPosCol1 = 5;
            int xPosCol2 = 275;
            int yPos = 20;
            foreach (string[] spParam in parameters)
            {
                paramsCount++;

                System.Windows.Forms.Label lblText = new System.Windows.Forms.Label();
                int xPos;
                if (paramsCount % 2 == 0)
                {
                    xPos = xPosCol2;
                }
                else
                {
                    xPos = xPosCol1;
                }

                lblText.Text = spParam[0];

                lblText.Location = new Point(xPos, yPos);
                lblText.Width = 115;

                groupBoxParameters.Controls.Add(lblText);

                if (spParam[1].ToLower().Equals("datetime"))
                {
                    DateTimePicker TimePicker = new DateTimePicker();
                    TimePicker.Name = spParam[0];
                    TimePicker.Format = DateTimePickerFormat.Custom;
                    TimePicker.CustomFormat = "dd/MM/yyyy";
                    TimePicker.Width = 100;
                    TimePicker.Location = new Point(xPos + lblText.Width, yPos);
                    groupBoxParameters.Controls.Add(TimePicker);
                }
                else
                {
                    System.Windows.Forms.TextBox TxtParam = new System.Windows.Forms.TextBox();
                    TxtParam.Name = spParam[0];
                    TxtParam.Location = new Point(xPos + lblText.Width, yPos);
                    groupBoxParameters.Controls.Add(TxtParam);
                }

                if (paramsCount % 2 == 0)
                {
                    yPos += 30;
                }
            }
        }

        public DataTable ExecuteSP(List<string[]> paramList)
        {
            //MessageBox.Show("We are getting data");
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand(SpName, exp_Factory.conn);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (string[] parameters in paramList)
            {
                cmd.Parameters.Add(new SqlParameter(parameters[0], parameters[1]));
            }

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            return exp_Factory.FormatDataTable(dt);
        }

        private List<string[]> GetParameters()
        {
            List<string[]> paramList = new List<string[]>();
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            foreach (object paramets in groupBoxParameters.Controls)
            {
                if (paramets is System.Windows.Forms.TextBox)
                {
                    System.Windows.Forms.TextBox tmpTextBox = paramets as System.Windows.Forms.TextBox;

                    paramList.Add(new string[] { tmpTextBox.Name.ToString(), tmpTextBox.Text });
                }
                else if (paramets is DateTimePicker)
                {
                    DateTimePicker tmpdatePicker = paramets as DateTimePicker;
                    string _tmpDate;
                    _tmpDate = tmpdatePicker.Value.ToString("yyyyMMdd");
                    paramList.Add(new string[] { tmpdatePicker.Name.ToString(), _tmpDate });

                    if (tmpdatePicker.Name.ToLower().Equals("@startdate"))
                    {
                        startDate = tmpdatePicker.Value;
                    }
                    else if (tmpdatePicker.Name.ToLower().Equals("@enddate"))
                    {
                        endDate = tmpdatePicker.Value;
                    }
                }
            }
            exp_Factory.setDate(startDate, endDate);
            return paramList;
        }

        //public void ChangeInitialCatalog(string newDBName)
        //{
        //    SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder(exp_Factory.conn.ConnectionString)
        //    { ConnectTimeout = 5, InitialCatalog = newDBName }; // you can add other parameters.
        //    exp_Factory.conn.ConnectionString = conn.ConnectionString;
        //    exp_Factory.conn.Open();
        //}

        private void comboBoxDBs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //here
            DBName = (sender as ComboBox).Text;
            FillStoredProcedures();
            //ChangeInitialCatalog((newDBName));
        }

        private void comboBoxSPs_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void comboBoxSPs_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpName = (sender as ComboBox).Text;
            ParametersFactory();
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            dt = ExecuteSP(GetParameters());
            dataGridView.DataSource = dt;

            if (dt.Rows.Count != 0)
            {
                btnExportPDF.Enabled = true;
                btnexportEXCEL.Enabled = true;
            }
            else
            {
                btnExportPDF.Enabled = false;
                btnexportEXCEL.Enabled = false;
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            exp_Factory.fileName = txtReportName.Text;

            if (exp_Factory.fileName.Equals(""))
            {
                exp_Factory.fileName = Regex.Replace(SpName, "exp_", "", RegexOptions.IgnoreCase);
            }
            string fileName = exp_Factory.ExportToPDF(dt);
            MessageBox.Show("PDF: Exported Succsefully to :\n" + fileName);
        }

        private void btnexportEXCEL_Click(object sender, EventArgs e)
        {
            exp_Factory.fileName = txtReportName.Text;

            if (exp_Factory.fileName.Equals(""))
            {
                exp_Factory.fileName = Regex.Replace(SpName, "exp_", "", RegexOptions.IgnoreCase);
            }
            string fileName = exp_Factory.ExportToEXCEL(dt);
            MessageBox.Show("Excel: Exported Succsefully to :\n" + fileName);
        }

        private void txtReportName_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblReportName_Click(object sender, EventArgs e)
        {
        }
    }
}