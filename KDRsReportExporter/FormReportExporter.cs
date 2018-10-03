using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
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
        private DataTable dept;

        private void FormReportExporter_Load(object sender, EventArgs e)
        {
            //PasswordForm frm = new PasswordForm();
            //if (frm.ShowDialog() != DialogResult.OK)
            //{
            //    // The user canceled.
            //    this.Close();
            //}

            btnExportPDF.Enabled = false;
            btnexportEXCEL.Enabled = false;
            exp_Factory = new ExporterFactory(true);

            if (exp_Factory.BisLocked)
            {
                this.Text = exp_Factory.ProgTittle;
                txtReportName.Text = exp_Factory.ReportName;

                comboBoxDBs.Items.Add(exp_Factory.GetCatalog());
                comboBoxSPs.Items.Add(exp_Factory.GetSpName());
                comboBoxDBs.SelectedIndex = 0;
                comboBoxSPs.SelectedIndex = 0;
                comboBoxDBs.Enabled = false;
                comboBoxSPs.Enabled = false;
                comboBoxDBs.Visible = false;
                comboBoxSPs.Visible = false;
                labelSPs.Visible = false;
                labelDBNames.Visible = false;
            }
            else
            {
                comboBoxDBs.DataSource = GetDBNames();
            }

            //exp_Factory.setDate(DateTime.Now.AddDays(-1), DateTime.Now);
            //DataTable dt = exp_Factory.GetData();
            //dataGridView.DataSource = dt;
            //string fileName = exp_Factory.ExportToPDF(dt);
            //exp_Factory.SendEmail(fileName);
            // Display the password form.
        }

        public void GetDepartments(CheckedListBox chcklstcdep)
        {
            string[] ColumnNames = { "ID", "Name" };
            List<string[]> departments = RunSelectStatementDEPT("use " + DBName + "; " +
                 "select * " +
                 "from dbo.Department"
                , ColumnNames);
            chcklstcdep
                .Items
                .Clear();
            chcklstcdep.ItemCheck += Selected;
            chcklstcdep.Items.Add("Velg alle");
            foreach (var item in departments)
            {
                chcklstcdep.Items.Add(item[1] + "_" + item[0]);
            }
        }

        private bool suppressCheckedChanged;

        private void Selected(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox item = (CheckedListBox)sender;

            if (suppressCheckedChanged) return;

            if (e.NewValue == CheckState.Checked && item.Text.Equals("Velg alle"))
            {
                suppressCheckedChanged = true;
                SelectDeselectAll(true);
            }
            else if (e.NewValue == CheckState.Unchecked && item.Text.Equals("Velg alle"))
            {
                suppressCheckedChanged = true;
                SelectDeselectAll(false);
            }
            suppressCheckedChanged = false;
        }

        public List<String[]> RunSelectStatementDEPT(string Query, string[] ColumnNames)
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
                    string[] tmpstr2 = { Convert.ToString(reader[ColumnNames[0]]), Convert.ToString(reader[ColumnNames[1]]) };
                    result.Add(tmpstr2);
                }
            }
            return result;
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

        private CheckedListBox deptListbox = new CheckedListBox();

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
                else if (spParam[0].Equals("@Departmentlist", StringComparison.OrdinalIgnoreCase))
                {
                    deptListbox.Name = spParam[0];

                    deptListbox.CheckOnClick = true;
                    GetDepartments(deptListbox);
                    deptListbox.Location = new Point(xPos + lblText.Width, yPos);
                    groupBoxParameters.Controls.Add(deptListbox);
                }
                else if (spParam[0].Equals("@akkumulert", StringComparison.OrdinalIgnoreCase))
                {
                    CheckBox chcbox = new CheckBox();

                    chcbox.Name = spParam[0];
                    chcbox.Location = new Point(xPos + lblText.Width, yPos);
                    groupBoxParameters.Controls.Add(chcbox);
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

        public DataTable ExecuteSP(List<SqlParameter> paramList)
        {
            //MessageBox.Show("We are getting data");
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand(SpName, exp_Factory.conn);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter parameters in paramList)
            {
                cmd.Parameters.Add(parameters);
            }

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            return exp_Factory.FormatDataTable(dt);
        }

        private List<SqlParameter> GetParameters()
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            foreach (object paramets in groupBoxParameters.Controls)
            {
                if (paramets is System.Windows.Forms.TextBox)
                {
                    System.Windows.Forms.TextBox tmpTextBox = paramets as System.Windows.Forms.TextBox;

                    paramList.Add(new SqlParameter(tmpTextBox.Name.ToString(), tmpTextBox.Text));
                }
                else if (paramets is DateTimePicker)
                {
                    DateTimePicker tmpdatePicker = paramets as DateTimePicker;
                    string _tmpDate;
                    _tmpDate = tmpdatePicker.Value.ToString("yyyyMMdd");
                    paramList.Add(new SqlParameter(tmpdatePicker.Name.ToString(), _tmpDate));

                    if (tmpdatePicker.Name.ToLower().Equals("@startdate"))
                    {
                        startDate = tmpdatePicker.Value;
                    }
                    else if (tmpdatePicker.Name.ToLower().Equals("@enddate"))
                    {
                        endDate = tmpdatePicker.Value;
                    }
                }
                else if (paramets is CheckedListBox)
                {
                    System.Windows.Forms.CheckedListBox tmpchecklistbox = paramets as System.Windows.Forms.CheckedListBox;

                    List<String[]> checkedlist = new List<string[]>();
                    foreach (object Item in tmpchecklistbox.CheckedItems)
                    {
                        if (Item.ToString().Equals("Velg alle"))
                        {
                            continue;
                        }
                        String[] chcSplit = Item.ToString().Split('_');
                        checkedlist.Add(chcSplit);
                    }

                    paramList.Add(new SqlParameter(tmpchecklistbox.Name, CreateSqlDataRecords(checkedlist)));
                }
                else if (paramets is CheckBox)
                {
                    CheckBox tmpcheckbox = paramets as CheckBox;

                    paramList.Add(new SqlParameter(tmpcheckbox.Name, tmpcheckbox.Checked));
                }
            }
            exp_Factory.setDate(startDate, endDate);
            return paramList;
        }

        private DataTable CreateSqlDataRecords(List<String[]> checkedlist)
        {
            DataTable table = new DataTable();
            table.Columns.Add("UserID", typeof(int));
            table.Columns.Add("DepName", typeof(String));
            foreach (string[] item in checkedlist)
            {
                table.Rows.Add(int.Parse(item[1]), item[0]);
            }

            return table;
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
            if (!exp_Factory.BisLocked)
            {
                FillStoredProcedures();
            }

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

        private void chcklistCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void SelectDeselectAll(bool Selected)
        {
            for (int i = 0; i < deptListbox.Items.Count; i++) // loop to set all items checked or unchecked
            {
                deptListbox.SetItemChecked(i, Selected);
            }
        }
    }
}