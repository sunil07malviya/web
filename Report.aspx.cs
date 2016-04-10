using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalDecisions.Shared;

namespace ISPL.CSC.Web
{
    public partial class Report : BasePage
    {
        private const string DB_KEY = "DB_KEY";

        CrystalDecisions.CrystalReports.Engine.ReportDocument objReportDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        CrystalDecisions.CrystalReports.Engine.SubreportObject objSubReport;
        CrystalDecisions.CrystalReports.Engine.ReportDocument objSubReportDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            rptViewer.DisplayToolbar = true;
            if (!IsPostBack)
            {
                ViewState["ID"] = Request["ID"].ToString();
                ViewState["SELFORMULA"] = Request["SELFORMULA"].ToString();
                ViewState["PARAMETER"] = Request["PARAMETER"].ToString();
                ViewState["FORMULA"] = Request["FORMULA"].ToString();
                ViewState["ISPDF"] = Request["IsPDF"] != null ? Request["IsPDF"].ToString() : "0";
            }
            Model.ReportInfo myReport = SQLServerDAL.Report.GetReportInfo(Convert.ToInt32(ViewState["ID"].ToString()));

            this.Title = myReport.ReportName;

            string sReportName = Server.MapPath(".") + "\\Reports\\" + myReport.RPTFileName;
            string sSelectionFormula = myReport.SelectionFormula;
            string param = myReport.Parameter;
            string formula = myReport.Formula;
            this.Title = myReport.ReportName;

            if (ViewState["SELFORMULA"].ToString() != "")
            {
                string str1 = ViewState["SELFORMULA"].ToString();
                str1 = str1.Replace("{", "").Replace("}", "");

                string[] strArray = str1.Split(',');
                for (int i = 0; i < strArray.Length; i++)
                {
                    sSelectionFormula = sSelectionFormula.Replace("?" + i.ToString(), strArray[i].ToString());
                }
            }
            else
                sSelectionFormula = "1 = 1";

            if (ViewState["PARAMETER"].ToString() != "")
            {
                string str1 = ViewState["PARAMETER"].ToString();
                str1 = str1.Replace("{", "").Replace("}", "");

                string[] strArray = str1.Split(',');
                for (int i = 0; i < strArray.Length; i++)
                {
                    param = param.Replace("?" + i.ToString(), strArray[i].ToString());
                }
            }

            if (ViewState["FORMULA"].ToString() != "" || myReport.Formula.Length > 0)
            {
                string str1 = ViewState["FORMULA"].ToString();
                str1 = str1.Replace("{", "").Replace("}", "");

                string[] strArray = str1.Split(',');
                for (int i = 0; i < strArray.Length; i++)
                {
                    formula = formula.Replace("?" + i.ToString(), strArray[i].ToString());
                }
            }

            // Direct printing
            if ((!IsPostBack) && (divPrint.Visible == true))
            {
                try
                {
                    ddlPrinter.DataSource = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
                    ddlPrinter.DataBind();
                }
                catch
                {
                    divPrint.Visible = false;
                }
            }

            ViewReport(sReportName, sSelectionFormula, param, formula, ViewState["ISPDF"].ToString());
        }
        public void ViewReport(string sReportName, string sSelectionFormula, string param, string formula, string IsPDF)
        {
            int intCounter;
            int intFCounter;
            string[] strParValPair;
            string[] strVal;
            string[] strForValPair;
            string[] strFVal;
            int intCounter1;

            ////Crystal Report's report document object
            //CrystalDecisions.CrystalReports.Engine.ReportDocument objReportDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //object of table Log on info of Crystal report
            TableLogOnInfo ConInfo = new TableLogOnInfo();
            //Parameter value object of crystal report parameters used for adding the value to parameter.
            ParameterDiscreteValue paraValue = new ParameterDiscreteValue();
            //Current parameter value object(collection) of crystal report parameters.
            ParameterValues currValue = new ParameterValues();
            //Formula value object of crystal report formulas used for adding the TEXT to formula.
            //FormulaFieldDefinition formulaField; 

            //Sub report object of crystal report.
            //SubreportObject objSubReport; //=new SubreportObject(); 
            //Sub report document of crystal report.
            //ReportDocument objSubReportDoc = new ReportDocument();

            try
            {
                //Load the report
                objReportDoc.Load(sReportName);
                //GC.Collect();

                //objReportDoc.Load(sReportName, OpenReportMethod.OpenReportByTempCopy);

                //Set the connection information to ConInfo object so that we can apply the 
                //connection information on each table in the report
                string lstrConn = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnString"].ToString();

                if (lstrConn.Length > 0)
                {
                    strParValPair = lstrConn.Split(';');

                    foreach (string index in strParValPair)
                    {
                        if (index.IndexOf("=") > 0)
                        {
                            strVal = index.Split('=');
                            paraValue.Value = strVal[0];

                            switch (strVal[0].ToUpper().Trim())
                            {
                                case "SERVER":
                                    ConInfo.ConnectionInfo.ServerName = strVal[1].Trim();
                                    break;
                                case "USER ID":
                                    ConInfo.ConnectionInfo.UserID = strVal[1].Trim();
                                    break;
                                case "PASSWORD":
                                    ConInfo.ConnectionInfo.Password = strVal[1].Trim();
                                    break;
                                case "DATABASE":
                                    ConInfo.ConnectionInfo.DatabaseName = strVal[1].Trim().Replace("{0}", HttpContext.Current.Session[DB_KEY].ToString());
                                    break;
                            }
                        }
                    }
                }

                for (intCounter = 0; intCounter <= objReportDoc.Database.Tables.Count - 1; intCounter++)
                {
                    objReportDoc.Database.Tables[intCounter].ApplyLogOnInfo(ConInfo);
                    objReportDoc.Database.Tables[intCounter].Location = "[" + ConInfo.ConnectionInfo.DatabaseName + "].[" + "DBO" + "]." + objReportDoc.Database.Tables[intCounter].Location;
                }

                //			Loop through each section on the report then look through each object in the section if the object is			a subreport, then apply logon info on each table of that sub report
                for (int index = 0; index <= objReportDoc.ReportDefinition.Sections.Count - 1; index++)
                {
                    for (intCounter = 0; intCounter <= objReportDoc.ReportDefinition.Sections[index].ReportObjects.Count - 1; intCounter++)
                    {
                        if (objReportDoc.ReportDefinition.Sections[index].ReportObjects[intCounter].Kind == CrystalDecisions.Shared.ReportObjectKind.SubreportObject)
                        {
                            //							objSubReport = CType(objReportDoc.ReportDefinition.Sections[index].ReportObjects[intCounter],CrystalDecisions.CrystalReports.Engine.SubreportObject);

                            // Get the ReportObject by name and cast it as a SubreportObject.
                            objSubReport = objReportDoc.ReportDefinition.Sections[index].ReportObjects[intCounter] as CrystalDecisions.CrystalReports.Engine.SubreportObject;
                            if (objSubReport != null)
                            {
                                //Open the SubReport as Report Document by getting the SubReport Name
                                objSubReportDoc = objSubReport.OpenSubreport(objSubReport.SubreportName);
                                for (intCounter1 = 0; intCounter1 <= objSubReportDoc.Database.Tables.Count - 1; intCounter1++)
                                {
                                    objSubReportDoc.Database.Tables[intCounter1].ApplyLogOnInfo(ConInfo);
                                    if (ConInfo.ConnectionInfo.UserID != "sa")
                                    {
                                        objSubReportDoc.Database.Tables[intCounter1].Location = ConInfo.ConnectionInfo.DatabaseName + "." + ConInfo.ConnectionInfo.UserID + "." + objSubReportDoc.Database.Tables[intCounter1].Location;
                                    }
                                    else 
                                    {
                                        objSubReportDoc.Database.Tables[intCounter1].Location = "[" + ConInfo.ConnectionInfo.DatabaseName + "].[" + "DBO" + "]." + objSubReportDoc.Database.Tables[intCounter1].Location;
                                    }
                                }
                            }
                        }
                    }
                }
                //Check if there are parameters or not in Report.
                intCounter = objReportDoc.DataDefinition.ParameterFields.Count;

                if ((intCounter > 0) && (param.Trim() != ""))
                {
                    string x = param.Trim();
                    strParValPair = param.Split(':');
                    foreach (string index in strParValPair)
                    {
                        if (index.IndexOf("=") > 0)
                        {
                            strVal = index.Split('=');
                            paraValue.Value = strVal[1];
                            currValue = objReportDoc.DataDefinition.ParameterFields[strVal[0].Replace("@", "")].CurrentValues;
                            currValue.Add(paraValue);
                            objReportDoc.DataDefinition.ParameterFields[strVal[0].Replace("@", "")].ApplyCurrentValues(currValue);
                        }
                    }
                }
                //Check if there are Formulas or not in Report.
                intFCounter = objReportDoc.DataDefinition.FormulaFields.Count;

                if ((intFCounter > 0) && (formula.Trim() != ""))
                {
                    strForValPair = formula.Split(':');
                    foreach (string index in strForValPair)
                    {
                        if (index.IndexOf("=") > 0)
                        {
                            strFVal = index.Split('=');
                            objReportDoc.DataDefinition.FormulaFields[strFVal[0]].Text = "'" + strFVal[1] + "'";

                            //							formulaField=objReportDoc.DataDefinition.FormulaFields[strFVal[0].Replace("@","")];
                            //							formulaField.Text ="'" + strFVal[1]+ "'";
                        }
                    }
                }

                //If there is a selection formula passed to this function then use that
                if (sSelectionFormula.Length > 0)
                {
                    objReportDoc.RecordSelectionFormula = sSelectionFormula;
                }
                //Re setting control 
                rptViewer.ReportSource = "";
                lblError.Text = string.Empty;

                if (IsPDF == "0")
                {
                    rptViewer.ReportSource = objReportDoc;
                }
                else
                {

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        string FileName = this.Title;
                        objReportDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, FileName);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                //lblError.Text = "Unable to show report... Errsor : " + ex.InnerException.Message.ToString();
                throw;
            }
            finally
            {
                //objReportDoc.Close();
                //objReportDoc.Dispose();
            }
            //objReportDoc.Close();
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            objSubReportDoc.Close();
            objSubReportDoc.Dispose();
            objReportDoc.Close();
            objReportDoc.Dispose();
        }

        protected void imgPaperSize_Click(object sender, ImageClickEventArgs e)
        {
            // Populate the respective printer's paper size
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            pd.PrinterSettings.PrinterName = ddlPrinter.SelectedItem.Text;
            ddlPaper.Items.Clear();

            for (int i = 0; i < pd.PrinterSettings.PaperSizes.Count; i++)
            {
                ListItem li = new ListItem();
                li.Text = pd.PrinterSettings.PaperSizes[i].PaperName;
                li.Value = pd.PrinterSettings.PaperSizes[i].RawKind.ToString();

                ddlPaper.Items.Add(li);

                // Default paper size
                //if (li.Text == pd.PrinterSettings.DefaultPageSettings.PaperSize.PaperName)
                //li.Selected = true;
            }
        }

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            pSetPrinterSettings();

            objReportDoc.PrintToPrinter(1, false, 0, 0);
        }
        private void pSetPrinterSettings()
        {
            objReportDoc.PrintOptions.PrinterName = ddlPrinter.SelectedItem.Text;

            if (ddlPaper.Items.Count > 0)
                objReportDoc.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)Convert.ToInt32(ddlPaper.SelectedValue);
        }
    }
}
