using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS.CustomClass;
using WMS.Models;
using WMSLibrary;

namespace WMS.Reports.Filters
{
    public partial class StepOneFilter : System.Web.UI.Page
    {
        private TAS2013Entities da = new TAS2013Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadSession();
                // Bind Grid View According to Filters
                BindGridView("");
                BindGridViewLocation("");
                List<string> list = Session["ReportSession"] as List<string>;
                dateFrom.Value = list[0];
                dateTo.Value = list[1];
            }
            if (Session["FiltersModel"] != null)
            {
                // Check and Uncheck Items in grid view according to Session Filters Model
                WMSLibrary.Filters.SetGridViewCheckState(GridViewCompany, Session["FiltersModel"] as FiltersModel, "Company");
                WMSLibrary.Filters.SetGridViewCheckState(GridViewLocation, Session["FiltersModel"] as FiltersModel, "Location");
            }
        }
        private void LoadSession()
        {
            using (TAS2013Entities dc = new TAS2013Entities())
            {
                var v = dc.Users.Where(aa => aa.UserName == "wms.ffl").FirstOrDefault();
                if (v != null)
                {
                    Session["MDevice"] = "0";
                    Session["MHR"] = "0";
                    Session["MDevice"] = "0";
                    Session["MLeave"] = "0";
                    Session["MEditAtt"] = "0";
                    Session["MUser"] = "0";
                    Session["LogedUserFullname"] = "";
                    Session["UserCompany"] = "";
                    Session["MRDailyAtt"] = "0";
                    Session["MRLeave"] = "0";
                    Session["MRMonthly"] = "0";
                    Session["MRAudit"] = "0";
                    Session["MRManualEditAtt"] = "0";
                    Session["MREmployee"] = "0";
                    Session["MRDetail"] = "0";
                    Session["MRSummary"] = "0";
                    Session["LogedUserID"] = v.UserID.ToString();
                    Session["LogedUserFullname"] = v.UserName;
                    Session["LoggedUser"] = v;
                    Session["UserCompany"] = v.CompanyID.ToString();
                    if (v.MHR == true)
                        Session["MHR"] = "1";
                    if (v.MDevice == true)
                        Session["MDevice"] = "1";
                    if (v.MLeave == true)
                        Session["MLeave"] = "1";
                    if (v.MEditAtt == true)
                        Session["MEditAtt"] = "1";
                    if (v.MUser == true)
                        Session["MUser"] = "1";
                    if (v.MRDailyAtt == true)
                        Session["MRDailyAtt"] = "1";
                    if (v.MRLeave == true)
                        Session["MRLeave"] = "1";
                    if (v.MRMonthly == true)
                        Session["MRMonthly"] = "1";
                    if (v.MRAudit == true)
                        Session["MRAudit"] = "1";
                    if (v.MRManualEditAtt == true)
                        Session["MRManualEditAtt"] = "1";
                    if (v.MREmployee == true)
                        Session["MREmployee"] = "1";
                    if (v.MRDetail == true)
                        Session["MRDetail"] = "1";
                    if (v.MRSummary == true)
                        Session["MRSummary"] = "1";
                    if (v.MRoster == true)
                        Session["MRoster"] = "1";
                }
            }
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            string date = dateFrom.Value.ToString();
            // Save selected Company ID and Name in Session
            SaveCompanyIDs();
            BindGridView(TextBoxSearch.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewCompany, Session["FiltersModel"] as FiltersModel, "Company");
        }
        protected void ButtonSearchLoc_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveLocationIDs();
            BindGridViewLocation(tbSearch_Location.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewLocation, Session["FiltersModel"] as FiltersModel, "Location");
        }
        protected void GridViewCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveCompanyIDs();

            //change page index
            GridViewCompany.PageIndex = e.NewPageIndex;
            BindGridView("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewCompany, Session["FiltersModel"] as FiltersModel, "Company");
        }

        protected void GridViewLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveLocationIDs();

            //change page index
            GridViewLocation.PageIndex = e.NewPageIndex;
            BindGridViewLocation("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewLocation, Session["FiltersModel"] as FiltersModel, "Location");
        }

        private void SaveCompanyIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewCompany, Session["FiltersModel"] as FiltersModel, "Company");
            Session["FiltersModel"] = FM;
        }
        private void SaveLocationIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewLocation, Session["FiltersModel"] as FiltersModel, "Location");
            Session["FiltersModel"] = FM;
        }

        private void BindGridView(string search)
        {
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForCompanyView(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from Company " + query);
            List<Company> _View = dt.ToList<Company>();
            GridViewCompany.DataSource = _View.Where(aa => aa.CompName.Contains(search)).ToList();
            GridViewCompany.DataBind();
        }

        private void BindGridViewLocation(string search)
        {
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForLocationTableSegeration(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from Location " +query);
            List<Location> _View = dt.ToList<Location>();
            GridViewLocation.DataSource = _View.Where(aa => aa.LocName.Contains(search)).ToList();
            GridViewLocation.DataBind();
        }
        public DateTime DateFrom
        {
            get
            {
                if (dateFrom.Value == "")
                    return DateTime.Today.Date.AddDays(-1);
                else
                    return DateTime.Parse(dateFrom.Value);
            }
        }
        public DateTime DateTo
        {
            get
            {
                if (dateTo.Value == "")
                    return DateTime.Today.Date.AddDays(-1);
                else
                    return DateTime.Parse(dateTo.Value);
            }
        }
        #region Navigation Buttons
        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDateSession();
            SaveCompanyIDs();
            SaveLocationIDs();
            // Go to the next page
            string url = "~/Reports/Filters/StepTwoFilter.aspx";
            Response.Redirect(url);
        }

        private void SaveDateSession()
        {
            List<string> list = Session["ReportSession"] as List<string>;
            list[0] = DateFrom.ToString("yyyy-MM-dd");
            list[1] = DateTo.ToString("yyyy-MM-dd");
            Session["ReportSession"] = list;
        }
        protected void ButtonSkip_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            //SaveCompanyIDs();
            //SaveLocationIDs();
            // Go to the next page
            string url = "~/Reports/StepTwoFilter.aspx";
            Response.Redirect(url);
        }
        protected void ButtonFinish_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveCompanyIDs();
            SaveLocationIDs();
            List<string> list = Session["ReportSession"] as List<string>;
            string aa = DateFrom.Date.ToString("yyyy-MM-dd");
            string bb = DateTo.Date.ToString("yyyy-MM-dd");
            list.Add(aa);
            list.Add(bb);
            Session["ReportSession"] = list;
            // Go to the next page
            string url = "~/Reports/ReportContainer.aspx";
            Response.Redirect(url);
        }
        #endregion

        protected void GridViewCompany_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewCompany.PageIndex + 1) + " of " + GridViewCompany.PageCount;
            }
        }

        protected void GridViewLocation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewLocation.PageIndex + 1) + " of " + GridViewLocation.PageCount;
            }
        }
    }
}