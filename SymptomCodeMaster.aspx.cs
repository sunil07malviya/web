using System;
using System.Web.UI;
using ISPL.CSC.Model.Masters;

namespace ISPL.CSC.Web.Masters
{
    public partial class SymptomCodeMaster : BasePage
    {
        private const string TRAN_ID_KEY = "ID";
        private const string STATUS_KEY = "Status";

        private SymptomCodeInfo mySymptomCodeInfo = null;
        private void Page_Load(object sender, EventArgs e)
        {
            
            this.LOVSymptomGroup.LOVAfterClick += new EventHandler(LOVSymptomGroup_LOVAfterClick);
            this.LOVModel.LOVAfterClick +=new EventHandler(LOVModel_LOVAfterClick);
            btnSymptomCode.Status = "";

            if (!IsPostBack)
            {
                pDispHeading();
                pSetUserControls();
                mySymptomCodeInfo = SQLServerDAL.Masters.SymptomCode.GetSymptomCodeInfo(Convert.ToInt32(Request[TRAN_ID_KEY].ToString()));

                if (mySymptomCodeInfo != null)
                {
                    ViewState[TRAN_ID_KEY] = mySymptomCodeInfo;
                    ViewState[STATUS_KEY] = "View";
                    pLockControls();
                    pBindControls();
                }
                else
                {
                    ViewState[TRAN_ID_KEY] = new SymptomCodeInfo();
                    ViewState[STATUS_KEY] = "Add";
                    pClearControls();
                }
                btnSymptomCode.MenuID = MenuID;
                btnSymptomCode.ButtonClicked = ViewState[STATUS_KEY].ToString();
            }

        }
        private void pSetUserControls()
        {

            LOVSymptomGroup.ClearAll();
            LOVSymptomGroup.Query = "select [Group] [ShortName],[GroupName] [Description],[SlNo] from SymptomGroups";

            //LOVSymptomGroup.TextWidth = 100;

            LOVFailureType.ClearAll();
            LOVFailureType.Query = "select FailureType [ShortName], CodeName[Description],slno from FailureType";
            //LOVFailureType.TextWidth = 100;

            LOVBillingType.ClearAll();
            LOVBillingType.Query = "select BillingType [ShortName], CodeName [Description],slno from BillingType";
            //LOVBillingType.TextWidth = 100;

            LOVModel.ClearAll();
            LOVModel.Query = "select Model_Code [ShortName], Model_Description[Description],Model_Slno from model";
            //LOVModel.TextWidth = 75;
        } 
        private void pDispHeading()
        {
            lblHeading.InnerText = WebComponents.Misc.GetPageCaption(MenuID);
        }
        private void pBindControls()
        {
            mySymptomCodeInfo = (Model.Masters.SymptomCodeInfo)ViewState[TRAN_ID_KEY];
            try
            {
                SymptomGroupInfo mySymptomGroupInfo = SQLServerDAL.Masters.SymptomGroup.GetSymptomGroupInfo(mySymptomCodeInfo.MySymptomGroupInfo.SLNO);
                LOVModel.strFirstColumn = mySymptomCodeInfo.Modelcode;
                LOVModel.strLastColumn = "0";
                txtModelDesc.Text = mySymptomCodeInfo.Modeldesc;
                LOVSymptomGroup.strFirstColumn = mySymptomGroupInfo.GroupSName;
                LOVSymptomGroup.strLastColumn = Convert.ToString(mySymptomCodeInfo.MySymptomGroupInfo.SLNO);
                txtSymptomGroupDesc.Text = mySymptomCodeInfo.MySymptomGroupInfo.GroupName;
                txtSymptomCode.Text = mySymptomCodeInfo.Symptomcode;
                txtSymptomDesc.Text = mySymptomCodeInfo.SymptomCodedesc;
                LOVFailureType.strFirstColumn = mySymptomCodeInfo.Failuretypeinfo;
                LOVFailureType.strLastColumn = "0";
                LOVBillingType.strFirstColumn = mySymptomCodeInfo.Billingtypeinfo;
                LOVBillingType.strLastColumn = "0";
                txtBillAllwd.Text = mySymptomCodeInfo.OverrideBillAllowd;
                txtSpfcOvrdVal.Text = mySymptomCodeInfo.Specificoverridevalue;
                txtTextKey.Text = mySymptomCodeInfo.TextKey_faultGroup;
                txtCharacteristicValue.Text = Convert.ToString(mySymptomCodeInfo.Characteristicvalue);
                txtTextKey_FaultCode.Text = mySymptomCodeInfo.Textkey_faultcode;
                txtRPMUseBillingtypedefault.Text = mySymptomCodeInfo.RPMUsebillingtypedefault;
                txtChargeable.Text = mySymptomCodeInfo.Chargeable;
                txtADHConsumable.Text = mySymptomCodeInfo.ADHconsumable;
                txtTampering.Text = mySymptomCodeInfo.Tampering;
                txtBBEPPNotApplicable.Text = mySymptomCodeInfo.BBEPPnotapplicable;
                txtBER.Text = mySymptomCodeInfo.BER;
                txtF19.Text = mySymptomCodeInfo.F19;
                txtNFF.Text = mySymptomCodeInfo.NFF;
            }
            catch
            {
                throw;
            }
        }
        private void pMapControls()
        {
            mySymptomCodeInfo = (SymptomCodeInfo)ViewState[TRAN_ID_KEY];

            try
            {
                if(LOVSymptomGroup.strLastColumn.Length == 0)
                    mySymptomCodeInfo.MySymptomGroupInfo = null;
                else
                {
                    SymptomGroupInfo mySymptomGroupInfo = SQLServerDAL.Masters.SymptomGroup.GetSymptomGroupInfo(Convert.ToInt32(LOVSymptomGroup.strLastColumn));
                    mySymptomCodeInfo.MySymptomGroupInfo = mySymptomGroupInfo;
                }
                if (LOVFailureType.strFirstColumn.Length == 0)
                    mySymptomCodeInfo.Failuretypeinfo = "";
                else
                {
                    //Modified by chandrashekar s date: 23/oct/2014
                    //Lov Manual Typed selection Making to selct only first column value :Start
                    string strFailureType = LOVFailureType.strFirstColumn;
                    int intPipelinepos = strFailureType.IndexOf('|');
                    string strFailureTypeSplit = strFailureType;
                    if (intPipelinepos != -1)
                    {
                        strFailureTypeSplit = strFailureType.Substring(0, intPipelinepos);
                        strFailureTypeSplit = strFailureTypeSplit.Replace("\t", String.Empty);
                    }

                    mySymptomCodeInfo.Failuretypeinfo = strFailureTypeSplit;

                    //Selecting 2nd column value also, and Same inserting into Database                   
                    //mySymptomCodeInfo.Failuretypeinfo = Convert.ToString(LOVFailureType.strFirstColumn);
                    //:End
                }
                if (LOVBillingType.strFirstColumn.Length == 0)
                    mySymptomCodeInfo.Billingtypeinfo = "";
                else
                {
                    //Modified by chandrashekar s date: 23/oct/2014
                    //Lov Manual Typed selection Making to selct only first column value :Start
                    string strBillingType = LOVBillingType.strFirstColumn;
                    int intPipelinepos = strBillingType.IndexOf('|');
                    string strBillingTypeSplit = strBillingType;
                    if (intPipelinepos != -1)
                    {
                        strBillingTypeSplit = strBillingType.Substring(0, intPipelinepos);
                        strBillingTypeSplit = strBillingTypeSplit.Replace("\t", String.Empty);
                    }

                    mySymptomCodeInfo.Billingtypeinfo = strBillingTypeSplit;

                    //Selecting 2nd column value also, and Same inserting into Database                                    
                    //   mySymptomCodeInfo.Billingtypeinfo = Convert.ToString(LOVBillingType.strFirstColumn);
                    //:End
                }
                if (LOVModel.strFirstColumn.Length == 0)
                    mySymptomCodeInfo.Modelcode = "";
                else
                {
                    //Modified by chandrashekar s date: 23/oct/2014
                    //Lov Manual Typed selection Making to selct only first column value :Start
                  
                    mySymptomCodeInfo.Modelcode = LOVModel.strFirstColumn.Split('|')[0].Trim().ToString();

                    //Selecting 2nd column value also, and Same inserting into Database                                                        
                    //mySymptomCodeInfo.Modelcode = Convert.ToString(LOVModel.strFirstColumn);
                    //:End                    
                }
                //mySymptomCodeInfo.Modelcode = WebComponents.CleanString.InputText(txtModel.Text, txtModel.MaxLength);
                mySymptomCodeInfo.Modeldesc = WebComponents.CleanString.InputText(txtModelDesc.Text, txtModelDesc.MaxLength);
                
                mySymptomCodeInfo.Symptomcode = WebComponents.CleanString.InputText(txtSymptomCode.Text,txtSymptomCode.MaxLength);
                //mySymptomCodeInfo.SymptomCodedesc = WebComponents.CleanString.InputText(txtSymptomDesc.Text, txtSymptomDesc.MaxLength);
               //mySymptomCodeInfo.Billingtypeinfo = WebComponents.CleanString.InputText(LOVBillingType.strFirstColumn, LOVBillingType.strFirstColumn.Length);
                mySymptomCodeInfo.OverrideBillAllowd = WebComponents.CleanString.InputText(txtBillAllwd.Text, txtBillAllwd.MaxLength);
                mySymptomCodeInfo.Specificoverridevalue = WebComponents.CleanString.InputText(txtSpfcOvrdVal.Text, txtSpfcOvrdVal.MaxLength);
                mySymptomCodeInfo.TextKey_faultGroup = WebComponents.CleanString.InputText(txtTextKey.Text, txtTextKey.MaxLength);
                mySymptomCodeInfo.Characteristicvalue = Convert.ToDouble(WebComponents.CleanString.InputText(txtCharacteristicValue.Text, txtCharacteristicValue.MaxLength));
                mySymptomCodeInfo.Textkey_faultcode = WebComponents.CleanString.InputText(txtTextKey_FaultCode.Text, txtTextKey_FaultCode.MaxLength);
                mySymptomCodeInfo.RPMUsebillingtypedefault = WebComponents.CleanString.InputText(txtRPMUseBillingtypedefault.Text, txtRPMUseBillingtypedefault.MaxLength);
                mySymptomCodeInfo.Chargeable = WebComponents.CleanString.InputText(txtChargeable.Text, txtChargeable.MaxLength);
                mySymptomCodeInfo.ADHconsumable = WebComponents.CleanString.InputText(txtADHConsumable.Text, txtADHConsumable.MaxLength);
                mySymptomCodeInfo.Tampering = WebComponents.CleanString.InputText(txtTampering.Text, txtTampering.MaxLength);
                mySymptomCodeInfo.BBEPPnotapplicable = WebComponents.CleanString.InputText(txtBBEPPNotApplicable.Text, txtBBEPPNotApplicable.MaxLength);
                mySymptomCodeInfo.BER = WebComponents.CleanString.InputText(txtBER.Text, txtBER.MaxLength);
                mySymptomCodeInfo.F19 = WebComponents.CleanString.InputText(txtF19.Text, txtF19.MaxLength);
                mySymptomCodeInfo.NFF = WebComponents.CleanString.InputText(txtNFF.Text, txtNFF.MaxLength);
                ViewState[TRAN_ID_KEY] = mySymptomCodeInfo;
            }
            catch
            {
                throw;
            }
        }
        private void pClearControls()
        {
            //txtModel.Text = "";
            LOVModel.ClearAll();
            txtModelDesc.Text = "";
            LOVSymptomGroup.ClearAll();
            txtSymptomGroupDesc.Text = "";
            txtSymptomCode.Text = "";
            txtSymptomDesc.Text = "";
            LOVFailureType.ClearAll();
            LOVBillingType.ClearAll();
            txtBillAllwd.Text = "";
            txtSpfcOvrdVal.Text = "";
            txtTextKey.Text = "";
            txtCharacteristicValue.Text = "";
            txtTextKey_FaultCode.Text = "";
            txtRPMUseBillingtypedefault.Text = "";
            txtChargeable.Text = "";
            txtADHConsumable.Text = "";
            txtTampering.Text = "";
            txtBBEPPNotApplicable.Text = "";
            txtBER.Text = "";
            txtF19.Text = "";
            txtNFF.Text = "";

        }
        private void pBacktoGrid()
        {
            string url = "../DetailsView.aspx?MenuId=" + MenuID;
            Response.Redirect(url, true);
        }
        private void pUnLockControls()
        {
            LOVModel.ReadOnly = false;
            //txtModelDesc.ReadOnly = false;
            LOVSymptomGroup.ReadOnly = false;
            //txtSymptomGroupDesc.ReadOnly = false;
            txtSymptomCode.ReadOnly = false;
            txtSymptomDesc.ReadOnly = false;
            LOVFailureType.ReadOnly = false;
            LOVBillingType.ReadOnly = false;
            txtBillAllwd.ReadOnly = false;
            txtSpfcOvrdVal.ReadOnly = false;
            txtTextKey.ReadOnly = false;
            txtCharacteristicValue.ReadOnly = false;
            txtTextKey_FaultCode.ReadOnly = false;
            txtRPMUseBillingtypedefault.ReadOnly = false;
            txtChargeable.ReadOnly = false;
            txtADHConsumable.ReadOnly = false;
            txtTampering.ReadOnly = false;
            txtBBEPPNotApplicable.ReadOnly = false;
            txtBER.ReadOnly = false;
            txtF19.ReadOnly = false;
            txtNFF.ReadOnly = false;


        }
        private void pLockControls()
        {
            LOVModel.ReadOnly = true;
            //txtModelDesc.ReadOnly = true;
            LOVSymptomGroup.ReadOnly = true;
            //txtSymptomGroupDesc.ReadOnly = true;
            txtSymptomCode.ReadOnly = true;
            txtSymptomDesc.ReadOnly = true;
            LOVFailureType.ReadOnly = true;
            LOVBillingType.ReadOnly = true;
            txtBillAllwd.ReadOnly = true;
            txtSpfcOvrdVal.ReadOnly = true;
            txtTextKey.ReadOnly = true;
            txtCharacteristicValue.ReadOnly = true;
            txtTextKey_FaultCode.ReadOnly = true;
            txtRPMUseBillingtypedefault.ReadOnly = true;
            txtChargeable.ReadOnly = true;
            txtADHConsumable.ReadOnly = true;
            txtTampering.ReadOnly = true;
            txtBBEPPNotApplicable.ReadOnly = true;
            txtBER.ReadOnly = true;
            txtF19.ReadOnly = true;
            txtNFF.ReadOnly = true;

        }
        protected void Page_EditButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Modify";
            pUnLockControls();
            
        }
        protected void Page_DeleteButton(object sender, EventArgs e)
        {
            ViewState[STATUS_KEY] = "Delete";
            pLockControls();
        }
        protected void Page_CancelButton(object sender, EventArgs e)
        {
            pBacktoGrid();
        }
        protected void Page_SubmitButton(object sender, EventArgs e)
        {
            string lstrStatus = ViewState[STATUS_KEY].ToString();

            pMapControls();

            if (lstrStatus.Equals("Delete"))
            {
                if (fblnValidDelete())
                {
                    pDelete();
                    pBacktoGrid();
                    return;
                }
                else
                {
                    btnSymptomCode.Status = "Deletion not possible...!";
                    return;
                }
            }
            if (fblnValidEntry())
            {
                if ((lstrStatus.Equals("New") || lstrStatus.Equals("Add")))
                    pSave();

                if ((lstrStatus.Equals("Edit") || lstrStatus.Equals("Modify")))
                    pUpdate();

                pBacktoGrid();
            }
        }
        private void pSave()
        {
            try
            {
                pMapControls();

                mySymptomCodeInfo = (SymptomCodeInfo)ViewState[TRAN_ID_KEY];


                SQLServerDAL.Masters.SymptomCode.Insert(mySymptomCodeInfo);
            }
            catch
            {
                throw;
            }
        }
        private void pUpdate()
        {
            try
            {
                pMapControls();

                mySymptomCodeInfo = (SymptomCodeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.SymptomCode.Update(mySymptomCodeInfo);
            }
            catch
            {
                throw;
            }
        }
        private void pDelete()
        {
            try
            {
                mySymptomCodeInfo = (SymptomCodeInfo)ViewState[TRAN_ID_KEY];

                SQLServerDAL.Masters.SymptomCode.Delete(mySymptomCodeInfo.SLNO);
            }
            catch
            {
                throw;
            }
        }
        private bool fblnValidDelete()
        {
            mySymptomCodeInfo = (SymptomCodeInfo)ViewState[TRAN_ID_KEY];

            if (SQLServerDAL.Masters.SymptomCode.blnCheckDelete(mySymptomCodeInfo.Symptomcode))
                return true;
            else
                return false;
        }
        private bool fblnValidEntry()
        {
            bool lblnReturnValue = true;

            if (Page.IsValid)
            {
                if (LOVModel.strLastColumn.Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Model is required!";
                    lblnReturnValue = false;
                }
                if (txtModelDesc.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Model Desc is required!";
                    lblnReturnValue = false;
                }
                if (LOVSymptomGroup.strFirstColumn.Split('|')[0].Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Symptom Group Definition is required!";
                    lblnReturnValue = false;
                }
                if (txtSymptomGroupDesc.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Symptom Group Desc is required!";
                    lblnReturnValue = false;
                }
                if (txtSymptomCode.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Symptom Code is required!";
                    lblnReturnValue = false;
                }
                if (txtSymptomDesc.Text.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Symptom Desc is required!";
                    lblnReturnValue = false;
                }
                if (LOVFailureType.strLastColumn.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Default Failure Type is required!";
                    lblnReturnValue = false;
                }
                if (LOVBillingType.strLastColumn.Trim().Length == 0 && lblnReturnValue)
                {
                    lblMessage.Text = "Default Billing Type is required!";
                    lblnReturnValue = false;
                }
                //if (ddlBB10Ex.SelectedValue == "" && lblnReturnValue)
                //{
                //    lblMessage.Text = "BB10Exclusive is required!";
                //    lblnReturnValue = false;
                //}
                if (lblnReturnValue)
                {
                    mySymptomCodeInfo = (SymptomCodeInfo)ViewState[TRAN_ID_KEY];

                    if (ViewState[STATUS_KEY].Equals("Modify") && mySymptomCodeInfo.SLNO == 0)
                    {
                        lblMessage.Text = "SymptomCodeInfo not found...!";
                        lblnReturnValue = false;
                    }
                    if (SQLServerDAL.Masters.SymptomCode.blnCheckSymptomCodeInfo(mySymptomCodeInfo))
                        lblnReturnValue = true;
                    else
                    {
                        lblMessage.Text = "Duplicate Entry...!";
                        lblnReturnValue = false;
                    }
                }
            }
            else
            {
                btnSymptomCode.Status = "Check ur entries...!";
                lblnReturnValue = false;
            }
            return lblnReturnValue;
        }
        private void LOVSymptomGroup_LOVAfterClick(object sender, EventArgs e)
        {
            if (LOVSymptomGroup.strLastColumn.Length > 0)
            {
                Model.Masters.SymptomGroupInfo mySymptomGroupInfo = SQLServerDAL.Masters.SymptomGroup.GetSymptomGroupInfo(Convert.ToInt32(LOVSymptomGroup.strLastColumn));

                txtSymptomGroupDesc.Text = mySymptomGroupInfo.GroupName;
            }
            else
                txtSymptomDesc.Text = "";
        }
        private void LOVModel_LOVAfterClick(object sender, EventArgs e)
        {
            if (LOVModel.strLastColumn.Length > 0)
            {
                Model.Masters.ModelInfo myModelInfo = SQLServerDAL.Masters.ModelMaster.GetModelInfo(Convert.ToInt32(LOVModel.strLastColumn));

                txtModelDesc.Text = myModelInfo.Desc;
                LOVModel.strFirstColumn=LOVModel.strFirstColumn.Split('|')[0].Trim();
            }
            else
                txtModelDesc.Text = "";
        }
    }
}
