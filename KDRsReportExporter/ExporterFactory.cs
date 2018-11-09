using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace KDRsReportExporter
{
    internal class ExporterFactory
    {
        private string dataSource, catalog, selectSpName, SMTP, eUser, ePwd, Subject, filePath, bodyHead, bodyMain, bodySignature, isLocked;
        private List<string> sp_names = new List<string>();
        private int SMTPport, decimalCount = 2;
        private List<string> emailList = new List<string>();

        public Boolean BisLocked { get; set; }
        private Boolean isEmail;
        public SqlConnection conn;

        public string GetCatalog()
        {
            return catalog;
        }

        public List<string> GetSpName()
        {
            return sp_names;
        }

        public string ReportName { get; set; }
        public string ProgTittle { get; set; }

        public ExporterFactory(Boolean isrunning)
        {
            ReadDBSettings();
            ConnectToDB(isrunning);
        }

        public string periodeName { get; set; }

        public string fileName { get; set; }

        public void ReadDBSettings()
        {
            try
            {
                string appPath = System.Windows.Forms.Application.StartupPath;
                string[] lines = System.IO.File.ReadAllLines(appPath + "\\KDRsConfig.txt");
                char demiliter = '=';
                char semicolon = ';';
                //is LOCKED
                isLocked = lines[0].Split(demiliter)[1];
                ReportName = lines[2].Split(demiliter)[1];
                ProgTittle = lines[3].Split(demiliter)[1];
                //DATA
                dataSource = lines[5].Split(demiliter)[1];
                catalog = lines[6].Split(demiliter)[1];
                var lstSpnames = lines[7].Split(demiliter)[1];
                var spltlstSpnames = lstSpnames.Split(semicolon);
                foreach (var sp in spltlstSpnames)
                {
                    sp_names.Add(sp);
                }

                filePath = lines[8].Split(demiliter)[1];
                fileName = lines[9].Split(demiliter)[1];
                decimalCount = int.Parse(lines[10].Split(demiliter)[1]);

                //EMAIL - SETTINGS
                SMTP = lines[12].Split(demiliter)[1];
                eUser = lines[13].Split(demiliter)[1];
                ePwd = lines[14].Split(demiliter)[1];
                SMTPport = int.Parse(lines[15].Split(demiliter)[1]);

                //EMAIL
                Subject = lines[16].Split(demiliter)[1];
                bodyHead = lines[17].Split(demiliter)[1];
                bodyMain = lines[18].Split(demiliter)[1];
                bodySignature = lines[19].Split(demiliter)[1];
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                foreach (string line in lines)
                {
                    if (isEmail)
                    {
                        emailList.Add(line);
                    }
                    else
                    //Read the line and do the task
                    if (line.ToLower().Equals("[email]"))
                    {
                        isEmail = true;
                    }
                }

                if (isLocked.ToLower().Equals("yes"))
                {
                    BisLocked = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error" + e);
            }
        }

        public void ConnectToDB(Boolean isRunning = false)
        {
            conn = new SqlConnection();

            if (isRunning)
            {
                conn.ConnectionString =
           "Data Source=" + dataSource + ";" +

           //"Initial Catalog=" + catalog + ";" +
           "Integrated Security=True;";
            }
            else
            {
                conn.ConnectionString =
            "Data Source=" + dataSource + ";" +
            "Initial Catalog=" + catalog + ";" +
            "Integrated Security=True;";
            }

            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("DataBaseSource i KDRsConfig er feil");
            }
        }

        private DateTime startDate, endDate;

        public void setDate(DateTime _startDate, DateTime _endDate)
        {
            startDate = _startDate;

            endDate = _endDate;
        }

        public System.Data.DataTable GetData()
        {
            //MessageBox.Show("We are getting data");
            System.Data.DataTable dt = new System.Data.DataTable();
            selectSpName = sp_names[0];
            SqlCommand cmd = new SqlCommand(selectSpName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@startdate", startDate));
            cmd.Parameters.Add(new SqlParameter("@enddate", endDate));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);

            return FormatDataTable(dt);
        }

        public System.Data.DataTable FormatDataTable(System.Data.DataTable dt)
        {
            List<string> colNames = new List<string>();
            List<string> DateNames = new List<string>();
            string DepartmentColumn = "";
            System.Data.DataTable dtCloned = dt.Clone();
            foreach (DataColumn c in dt.Columns)
            {
                if (c.DataType == typeof(String))
                {
                    if (c.ColumnName.Equals("Avdelings Navn") || c.ColumnName.Equals("Rabatt Type") || c.ColumnName.Equals("AvdelingsNavn"))
                    {
                        DepartmentColumn = c.ColumnName;
                    }
                }
                else
                if (c.DataType == typeof(Decimal))
                {
                    dtCloned.Columns[c.ColumnName].DataType = typeof(Decimal);

                    colNames.Add(c.ColumnName);
                }
                else if (c.DataType == typeof(int))
                {
                    dtCloned.Columns[c.ColumnName].DataType = typeof(int);
                    colNames.Add(c.ColumnName);
                }
                else if (c.DataType == typeof(float))
                {
                    dtCloned.Columns[c.ColumnName].DataType = typeof(float);
                    colNames.Add(c.ColumnName);
                }
                //if (c.DataType == typeof(DateTime))
                //{
                //    DateNames.Add(c.ColumnName);
                //}
            }
            String lstDepartment = "";
            foreach (DataRow row in dt.Rows)
            {
                if (DepartmentColumn != "")
                {
                    String Departmentname = row.Field<String>(DepartmentColumn);
                    if (!lstDepartment.Equals(Departmentname))
                    {
                        DataRow row2 = dtCloned.NewRow();
                        // probalby set background color here somehow
                        row2.SetField(DepartmentColumn, Departmentname);
                        row.SetField(DepartmentColumn, "");

                        dtCloned.Rows.Add(row2);

                        lstDepartment = Departmentname;
                    }
                    else
                    {
                        row.SetField(DepartmentColumn, "");
                    }
                }
                dtCloned.ImportRow(row);
            }

            foreach (DataRow dr in dtCloned.Rows)
            {
                foreach (DataColumn dc in dtCloned.Columns)
                {
                    if (colNames.Contains(dc.ColumnName))
                    {
                        double value;
                        string whatYouWant = "";
                        double dts = 00;

                        if (double.TryParse((dr[dc]).ToString(), out value))
                        {
                            dts = Convert.ToDouble(dr[dc]);

                            if (decimalCount == 0)
                            {
                                whatYouWant = dts.ToString("#,##0");
                            }
                            else if (decimalCount == 2)
                            {
                                whatYouWant = dts.ToString("#,##0.00");
                            }

                            dr[dc] = whatYouWant;
                        }

                        //if (double.TryParse((dr[dc]).ToString(), out value))
                        //{
                        //    dts = Convert.ToDouble(dr[dc]);

                        //    if (decimalCount == 0)
                        //    {
                        //        whatYouWant = dts.ToString("#,##0");
                        //    }
                        //    else if (decimalCount == 2)
                        //    {
                        //        //whatYouWant = dts.ToString("C2");
                        //        whatYouWant = dts.ToString("#,##0.00");
                        //    }
                        //    if (dr[dc].GetType() == typeof(int))
                        //    {
                        //        int intvalue;
                        //        dr[dc] = int.TryParse(whatYouWant, out intvalue);
                        //    }
                        //    else
                        //    {
                        //        dr[dc] = whatYouWant;
                        //    }
                        //}
                    }
                    //if (DateNames.Contains(dc.ColumnName))
                    //{
                    //    if (!dr[dc].Equals(null))
                    //    {
                    //        DateTime tmpDateTime = Convert.ToDateTime(dr[dc]);
                    //        dr[dc] = Convert.ToDateTime(dr[dc]);
                    //    }
                    //}
                }
            }

            return dtCloned;
        }

        public void SendEmail(String attachmentFile)
        {
            //Sending the email.
            //Now we must create a new Smtp client to send our email.
            //MessageBox.Show("We are sending mail");
            SmtpClient client = new SmtpClient(SMTP, SMTPport);   //smtp.gmail.com // For Gmail
                                                                  //smtp.live.com // Windows live / Hotmail
                                                                  //smtp.mail.yahoo.com // Yahoo
                                                                  //smtp.aim.com // AIM
                                                                  //my.inbox.com // Inbox

            //Authentication.
            //This is where the valid email account comes into play. You must have a valid email account(with password) to give our program a place to send the mail from.

            NetworkCredential cred = new NetworkCredential(eUser, ePwd);

            //To send an email we must first create a new mailMessage(an email) to send.
            MailMessage Msg = new MailMessage();

            // Sender e-mail address.
            Msg.From = new MailAddress(eUser);//Nothing But Above Credentials or your credentials (*******@gmail.com)

            // Recipient e-mail address.
            foreach (String recipient in emailList)
            {
                // Msg.To.Add(recipient);
                Msg.Bcc.Add(recipient); // code for adding each email to Blindcopy copu so they do not see eachother.
            }

            // Assign the subject of our message.
            Msg.Subject = Subject;

            // Create the content(body) of our message.
            Msg.Body = bodyHead + " \r\n" + " \r\n" + bodyMain + " \r\n" + " \r\n" + bodySignature;

            Attachment attach = new Attachment(attachmentFile);

            Msg.Attachments.Add(attach);
            // Send our account login details to the client.
            client.Credentials = cred;

            //Enabling SSL(Secure Sockets Layer, encyription) is reqiured by most email providers to send mail
            client.EnableSsl = true;

            // Send our email.
            client.Send(Msg);
        }

        public string ExportToEXCEL(System.Data.DataTable dt)
        {
            string fileLocation = filePath + fileName + "_" + DateTime.Today.ToShortDateString() + ".xlsx";
            string OlderfileLocation = filePath + fileName + "_" + DateTime.Today.ToShortDateString() + ".xls";

            iTextSharp.text.Font ColFont3 = FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL);
            String ReportTime;
            String PrintDate = ("Utskiftdato: " + DateTime.Today.ToShortDateString());
            if (!startDate.Date.ToShortDateString().Equals("01.01.0001"))
            {
                ReportTime = ReportPeriod();
            }
            else
            {
                ReportTime = ("No Date Is Set ");
            }

            XLWorkbook wb = new XLWorkbook();

            var ws = wb.Worksheets.Add(dt, "Ark1");

            ws.Row(1).InsertRowsAbove(5);
            var LstColumnUsed = ws.LastColumnUsed();

            var rngHeader = ws.Range(1, 1, 5, LstColumnUsed.ColumnNumber());
            rngHeader.Style.Fill.BackgroundColor = XLColor.LightCyan;
            rngHeader.Style.Font.Bold = true;
            ws.Cell(3, 3).Value = ReportTime;
            ws.Cell(5, LstColumnUsed.ColumnNumber()).Value = PrintDate;
            wb.SaveAs(fileLocation);

            //object oMissing = Type.Missing;
            //var excelApp = new Microsoft.Office.Interop.Excel.Application();
            //// Make the object visible.
            ////excelApp.Visible = true;

            //// Create a new, empty workbook and add it to the collection returned
            //// by property Workbooks. The new workbook becomes the active workbook.
            //// Add has an optional parameter for specifying a praticular template.
            //// Because no argument is sent in this example, Add creates a new workbook.
            //var Workbook = excelApp.Workbooks.Add();

            //// This example uses a single workSheet. The explicit type casting is
            //// removed in a later procedure.
            //Microsoft.Office.Interop.Excel._Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.ActiveSheet;
            ////workSheet.Cells(wb);
            //// wbOLD.SaveAs(OlderfileLocation, XlFileFormat.xlExcel8, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            ////excelApp.Quit();
            //Workbook.SaveAs(OlderfileLocation, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //Workbook.Close();
            return fileLocation;
        }

        private string ReportPeriod()
        {
            return ("Periode:" + periodeName + "   " + "Fra : " + (startDate.ToShortDateString()) + "     Til : " + (endDate.ToShortDateString()));
        }

        public string ExportToPDF(System.Data.DataTable dt)
        {
            //Create a dummy GridView
            //MessageBox.Show("We are exporting PDF");
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            string fileLocation = filePath + fileName + "_" + DateTime.Today.ToShortDateString() + ".pdf";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileLocation, FileMode.Create));

            document.Open();
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);

            PdfPTable table = new PdfPTable(dt.Columns.Count);
            PdfPRow row = null;
            //float[] widths = new float[] { 4f, 4f, 4f, 4f, 4f };

            //table.SetWidths(widths);
            PdfPCell tCell;
            iTextSharp.text.Font ColFont = FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.BOLD);
            Chunk chunkCols = new Chunk(fileName, ColFont);
            tCell = new PdfPCell(new Paragraph(chunkCols));
            tCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tCell.Colspan = dt.Columns.Count;
            tCell.Border = 0;
            //cell.PaddingLeft = 10;
            tCell.Padding = 5;
            tCell.PaddingTop = 0;
            table.AddCell(tCell);

            PdfPCell cellH;
            iTextSharp.text.Font ColFont3 = FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL);
            Chunk chunkCols1;
            if (!startDate.Date.ToShortDateString().Equals("01.01.0001"))
            {
                chunkCols1 = new Chunk(ReportPeriod());
            }
            else
            {
                chunkCols1 = new Chunk("No Date Is Set ");
            }

            cellH = new PdfPCell(new Paragraph(chunkCols1))
            {
                Colspan = dt.Columns.Count,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingTop = 10,
                PaddingBottom = 5
            };
            table.AddCell(cellH);

            //Utskrift
            chunkCols1 = new Chunk(("Utskiftdato: " + DateTime.Today.ToShortDateString()), ColFont3);
            cellH = new PdfPCell(new Paragraph(chunkCols1));
            cellH.Colspan = dt.Columns.Count;
            cellH.Border = 0;
            cellH.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellH.PaddingTop = 0;
            cellH.PaddingBottom = 20;
            table.AddCell(cellH);

            table.WidthPercentage = 100;
            int iCol = 0;
            string colname = "";
            PdfPCell cell = new PdfPCell(new Phrase("Products"));
            cell.Colspan = dt.Columns.Count;

            foreach (DataColumn c in dt.Columns)
            {
                PdfPCell hcell = new PdfPCell(new Phrase(c.ColumnName, font5));
                hcell.BackgroundColor = new BaseColor(173, 216, 230);
                table.AddCell(hcell);
            }
            int row_cnt = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        PdfPCell dcell = new PdfPCell(new Phrase(r[c].ToString(), font5));

                        if (row_cnt % 2 == 1)
                        {
                            dcell.BackgroundColor = new BaseColor(220, 220, 220);
                        }
                        //pcell.HorizontalAlignment = Element.ALIGN_RIGHT;

                        table.AddCell(dcell);
                    }
                    //table.AddCell(new Phrase(r[0].ToString(), font5));
                    //table.AddCell(new Phrase(r[1].ToString(), font5));
                    //table.AddCell(new Phrase(r[2].ToString(), font5));
                    //table.AddCell(new Phrase(r[3].ToString(), font5));
                    //table.AddCell(new Phrase(r[4].ToString(), font5));
                }
                row_cnt += 1;
            }

            document.Add(table);
            document.Close();
            return fileLocation;
        }
    }
}