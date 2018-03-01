using System;
using System.Data;
using System.Windows.Forms;

namespace KDRsReportExporter
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // MessageBox.Show(args[0]);
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormReportExporter());
            }
            else if (args[0].ToLower().Equals("day"))
            {
                //MessageBox.Show("This was Day");
                //This takes the current day and send it as an argunment to the SQL stored procedure
                ExporterFactory exp_Factory = new ExporterFactory(false);
                exp_Factory.setDate(DateTime.Now.AddDays(-1).Date, DateTime.Now.AddDays(-1).Date);
                DataTable dt = exp_Factory.GetData();
                exp_Factory.periodeName = "Day";
                string fileName = exp_Factory.ExportToPDF(dt);
                exp_Factory.SendEmail(fileName);
            }
            else if (args[0].ToLower().Equals("week"))
            {
                //This takes the current day and send it as an argunment to the SQL stored procedure
                ExporterFactory exp_Factory = new ExporterFactory(false);
                exp_Factory.setDate(DateTime.Now.AddDays(-8).Date, DateTime.Now.AddDays(-1).Date);

                DataTable dt = exp_Factory.GetData();
                exp_Factory.periodeName = "Week";
                string fileName = exp_Factory.ExportToPDF(dt);
                exp_Factory.SendEmail(fileName);
            }
            else if ((args[0].ToLower().Equals("month")))
            {
                ExporterFactory exp_Factory = new ExporterFactory(false);
                exp_Factory.setDate(DateTime.Now.AddMonths(-1).Date, DateTime.Now.AddDays(-1).Date);

                DataTable dt = exp_Factory.GetData();
                exp_Factory.periodeName = "Month";
                string fileName = exp_Factory.ExportToPDF(dt);
                exp_Factory.SendEmail(fileName);
            }
        }
    }
}