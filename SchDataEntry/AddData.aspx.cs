using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchDataEntry
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInitialRow();
            }
        }

        protected void grdDataEntry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddlCountries = null;
            DropDownList ddlStates = null;
            DropDownList ddlCities = null;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddlCountries = (e.Row.FindControl("ddlCountries") as DropDownList);
                ddlStates = (e.Row.FindControl("ddlState") as DropDownList);
             
                BindDropDownList(ddlCountries, "1", "Select Country");
                 BindDropDownList(ddlStates, "2", "Select Country", Convert.ToInt32( ddlCountries.SelectedValue));
               
            }

        }

        protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.Parent.Parent;
            int idx = row.RowIndex;
            // TextBox txtECustCode = (TextBox)row.Cells[0].FindControl("txtECustCode");


            //foreach (GridViewRow row in grdDataEntry.Rows)
            //{
                DropDownList drpstate = ((DropDownList)row.FindControl("ddlState")) as DropDownList;
                DropDownList ddlCountries = ((DropDownList)row.FindControl("ddlCountries")) as DropDownList;
                //DropDownList drpstate = row.FindControl("drpstate") as DropDownList;
                BindDropDownList(drpstate, "2", "Select state",Convert.ToInt32(ddlCountries.SelectedValue));
           // }
          
        }

     
        private void BindDropDownList(DropDownList ddl, string mode, string defaultText,int CountryID=0)
        {

            CommonData objData = new CommonData();
            var datta = objData.GetCommonFilters(mode, CountryID);

            ddl.DataSource = datta;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(defaultText, "0"));
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;
            dt.Rows.Add(dr);
            //Store the DataTable in ViewState  
            ViewState["CurrentTable"] = dt;
            grdDataEntry.DataSource = dt;
            grdDataEntry.DataBind();
        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values  
                        DropDownList box1 = (DropDownList)grdDataEntry.Rows[rowIndex].Cells[1].FindControl("ddlCountries");
                        DropDownList box2 = (DropDownList)grdDataEntry.Rows[rowIndex].Cells[2].FindControl("ddlState");
                        TextBox box3 = (TextBox)grdDataEntry.Rows[rowIndex].Cells[3].FindControl("txtName");
                        TextBox box4 = (TextBox)grdDataEntry.Rows[rowIndex].Cells[3].FindControl("txtRemarks");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Column1"] = box1.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Column2"] = box2.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdDataEntry.DataSource = dtCurrentTable;
                    grdDataEntry.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //Set Previous Data on Postbacks  
          SetPreviousData();
        }
        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList box1 = (DropDownList)grdDataEntry.Rows[rowIndex].Cells[1].FindControl("ddlCountries");
                        DropDownList box2 = (DropDownList)grdDataEntry.Rows[rowIndex].Cells[2].FindControl("ddlState");
                        TextBox box3 = (TextBox)grdDataEntry.Rows[rowIndex].Cells[3].FindControl("txtName");
                        TextBox box4 = (TextBox)grdDataEntry.Rows[rowIndex].Cells[3].FindControl("txtRemarks");
                        box1.SelectedValue = dt.Rows[i]["Column1"].ToString();
                        box2.SelectedValue = dt.Rows[i]["Column2"].ToString();
                        box3.Text = dt.Rows[i]["Column3"].ToString();
                        box4.Text = dt.Rows[i]["Column4"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }


        public void SaveData()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDate.Text))
                {
                    DataSet DSApproveReject = new DataSet("dsAR");
                    DataTable DTApproveReject = new DataTable("dtAR");
                    DTApproveReject.Columns.Add(new DataColumn("EntryDate", typeof(string)));
                    DTApproveReject.Columns.Add(new DataColumn("CountryID", typeof(int)));
                    DTApproveReject.Columns.Add(new DataColumn("StateID", typeof(string)));
                    DTApproveReject.Columns.Add(new DataColumn("Name", typeof(string)));
                    DTApproveReject.Columns.Add(new DataColumn("Remarks", typeof(string)));


                    foreach (GridViewRow row in grdDataEntry.Rows)
                    {
                        DropDownList ddlCountries = (DropDownList)row.FindControl("ddlCountries");
                        DropDownList ddlState = (DropDownList)row.FindControl("ddlState");
                        TextBox txtName = (TextBox)row.FindControl("txtName");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

                        if (ddlCountries.SelectedValue != "0" && ddlState.SelectedValue != "0")
                        {
                            DTApproveReject.Rows.Add(txtDate.Text,
                                ddlCountries.SelectedValue, ddlState.SelectedValue, txtName.Text, txtRemarks.Text);
                        }

                    }
                    DSApproveReject.Tables.Add(DTApproveReject);
                    StringWriter strwtr = new StringWriter();
                    DSApproveReject.WriteXml(strwtr);
                    var data = strwtr.ToString();

                    CommonData objdata = new CommonData();
                    objdata.SaveData(data, txtDate.Text);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Date');", true);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnSaveData_Click(object sender, EventArgs e)
        {
            SaveData();
        }
    }
}