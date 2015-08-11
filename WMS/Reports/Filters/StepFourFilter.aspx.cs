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
    public partial class StepFourFilter : System.Web.UI.Page
    {
        private TAS2013Entities da = new TAS2013Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind Grid View According to Filters
                BindGridViewSection("");
                BindGridViewCrew("");
                //dateFrom.Value = "2015-08-09";
            }
            if (Session["FiltersModel"] != null)
            {
                // Check and Uncheck Items in grid view according to Session Filters Model
                WMSLibrary.Filters.SetGridViewCheckState(GridViewSection, Session["FiltersModel"] as FiltersModel, "Section");
                WMSLibrary.Filters.SetGridViewCheckState(GridViewCrew, Session["FiltersModel"] as FiltersModel, "Crew");
            }
        }
        protected void ButtonSearchSection_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveSectionIDs();
            BindGridViewSection(tbSearch_Section.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewSection, Session["FiltersModel"] as FiltersModel, "Section");
        }
        protected void ButtonSearchCrew_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveCrewIDs();
            BindGridViewCrew(tbSearch_Crew.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewCrew, Session["FiltersModel"] as FiltersModel, "Crew");
        }
        protected void GridViewSection_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveSectionIDs();

            //change page index
            GridViewSection.PageIndex = e.NewPageIndex;
            BindGridViewSection("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewSection, Session["FiltersModel"] as FiltersModel, "Section");
        }

        protected void GridViewCrew_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveCrewIDs();

            //change page index
            GridViewCrew.PageIndex = e.NewPageIndex;
            BindGridViewCrew("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewCrew, Session["FiltersModel"] as FiltersModel, "Crew");
        }

        private void SaveSectionIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewSection, Session["FiltersModel"] as FiltersModel, "Section");
            Session["FiltersModel"] = FM;
        }
        private void SaveCrewIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewCrew, Session["FiltersModel"] as FiltersModel, "Crew");
            Session["FiltersModel"] = FM;
        }

        private void BindGridViewSection(string search)
        {
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            List<Section> _View = new List<Section>();
            List<Section> _TempView = new List<Section>();
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForCompanyFilters(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from Section " + query);
            _View = dt.ToList<Section>();
            if (fm.DepartmentFilter.Count > 0)
            {
                foreach (var comp in fm.DepartmentFilter)
                {
                    short _compID = Convert.ToInt16(comp.ID);
                    _TempView.AddRange(_View.Where(aa => aa.DeptID == _compID).ToList());
                }
                _View = _TempView;
            }
            GridViewSection.DataSource = _View.Where(aa => aa.SectionName.Contains(search)).ToList();
            GridViewSection.DataBind();
        }

        private void BindGridViewCrew(string search)
        {
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            List<Crew> _View = new List<Crew>();
            List<Crew> _TempView = new List<Crew>();
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForCompanySegeration(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from Crew " + query);
            _View = dt.ToList<Crew>();
            if (fm.CompanyFilter.Count > 0)
            {
                foreach (var comp in fm.CompanyFilter)
                {
                    short _compID = Convert.ToInt16(comp.ID);
                    _TempView.AddRange(_View.Where(aa => aa.CompanyID == _compID).ToList());
                }
                _View = _TempView;
            }
            GridViewCrew.DataSource = _View.Where(aa => aa.CrewName.Contains(search)).ToList();
            GridViewCrew.DataBind();
        }

        #region Navigation Buttons
        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveSectionIDs();
            SaveCrewIDs();
            // Go to the next page
            string url = "~/Reports/Filters/StepFiveFilter.aspx";
            Response.Redirect(url);
        }
        protected void ButtonSkip_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveSectionIDs();
            // Go to the next page
            string url = "~/Filters/DeptFilter.aspx";
            Response.Redirect(url);
        }
        protected void ButtonFinish_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveSectionIDs();
            SaveCrewIDs();
            // Go to the next page
            string url = "~/Reports/ReportContainer.aspx";
            Response.Redirect(url);
        }
        #endregion

        protected void GridViewSection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewSection.PageIndex + 1) + " of " + GridViewSection.PageCount;
            }
        }

        protected void GridViewCrew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewCrew.PageIndex + 1) + " of " + GridViewCrew.PageCount;
            }
        }
    }
}