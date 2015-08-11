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
    public partial class StepTwoFilter : System.Web.UI.Page
    {
        private TAS2013Entities da = new TAS2013Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind Grid View According to Filters
                BindGridViewDivision("");
                BindGridViewShift("");
                List<string> list = Session["ReportSession"] as List<string>;
                dateFrom.Value = list[0];
                dateTo.Value = list[1];
            }
            if (Session["FiltersModel"] != null)
            {
                // Check and Uncheck Items in grid view according to Session Filters Model
                WMSLibrary.Filters.SetGridViewCheckState(GridViewDivision, Session["FiltersModel"] as FiltersModel, "Division");
                WMSLibrary.Filters.SetGridViewCheckState(GridViewShift, Session["FiltersModel"] as FiltersModel, "Shift");
            }
        }
        protected void ButtonSearchShift_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveShiftIDs();
            BindGridViewShift(tbSearch_Shift.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewShift, Session["FiltersModel"] as FiltersModel, "Shift");
        }
        protected void ButtonSearchDivision_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDivisionIDs();
            BindGridViewDivision(TextBoxSearchDivision.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewDivision, Session["FiltersModel"] as FiltersModel, "Division");
        }
        protected void GridViewDivision_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDivisionIDs();

            //change page index
            GridViewDivision.PageIndex = e.NewPageIndex;
            BindGridViewDivision("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewDivision, Session["FiltersModel"] as FiltersModel, "Division");
        }

        protected void GridViewShift_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveShiftIDs();

            //change page index
            GridViewShift.PageIndex = e.NewPageIndex;
            BindGridViewShift("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewShift, Session["FiltersModel"] as FiltersModel, "Shift");
        }

        private void SaveShiftIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewShift, Session["FiltersModel"] as FiltersModel, "Shift");
            Session["FiltersModel"] = FM;
        }
        private void SaveDivisionIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewDivision, Session["FiltersModel"] as FiltersModel, "Division");
            Session["FiltersModel"] = FM;
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
        private void BindGridViewDivision(string search)
        {
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            List<Division> _View = new List<Division>();
            List<Division> _TempView = new List<Division>();
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForCompanyFilters(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from Division " + query);
            _View = dt.ToList<Division>();
            if (fm.CompanyFilter.Count > 0)
            {
                foreach (var comp in fm.CompanyFilter)
                {
                    short _compID = Convert.ToInt16(comp.ID);
                    _TempView.AddRange(_View.Where(aa => aa.CompanyID == _compID).ToList());
                }
                _View = _TempView;
            }
            GridViewDivision.DataSource = _View.Where(aa => aa.DivisionName.Contains(search)).ToList();
            GridViewDivision.DataBind();
        }

        private void SaveDateSession()
        {
            List<string> list = Session["ReportSession"] as List<string>;
            list[0] = DateFrom.ToString("yyyy-MM-dd");
            list[1] = DateTo.ToString("yyyy-MM-dd");
            Session["ReportSession"] = list;
        }

        private void BindGridViewShift(string search)
        {
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            List<Shift> _View = new List<Shift>();
            List<Shift> _TempView = new List<Shift>();
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForLocationSegeration(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from Shift " + query);
            _View = dt.ToList<Shift>();
            if (fm.LocationFilter.Count > 0)
            {
                foreach (var loc in fm.LocationFilter)
                {
                    short _locID = Convert.ToInt16(loc.ID);
                    _TempView.AddRange(_View.Where(aa => aa.LocationID == _locID).ToList());
                }
                _View = _TempView;
            }
            GridViewShift.DataSource = _View.Where(aa => aa.ShiftName.Contains(search)).ToList();
            GridViewShift.DataBind();
        }

        #region Navigation Buttons
        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveShiftIDs();
            SaveDivisionIDs();
            SaveDateSession();
            // Go to the next page
            string url = "~/Reports/Filters/StepThreeFilter.aspx";
            Response.Redirect(url);
        }
        protected void ButtonSkip_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveShiftIDs();
            // Go to the next page
            string url = "~/Filters/DeptFilter.aspx";
            Response.Redirect(url);
        }
        protected void ButtonFinish_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveShiftIDs();
            SaveDivisionIDs();
            // Go to the next page
            string url = "~/Reports/ReportContainer.aspx";
            Response.Redirect(url);
        }
        #endregion

        protected void GridViewDivision_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewDivision.PageIndex + 1) + " of " + GridViewDivision.PageCount;
            }
        }

        protected void GridViewShift_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewShift.PageIndex + 1) + " of " + GridViewShift.PageCount;
            }
        }
    }
}