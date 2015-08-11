using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS.CustomClass;
using WMS.HelperClass;
using WMS.Models;
using WMSLibrary;

namespace WMS.Reports.Filters
{
    public partial class StepFiveFilter : System.Web.UI.Page
    {
        private TAS2013Entities da = new TAS2013Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind Grid View According to Filters
                BindGridViewEmployee("");
                List<string> list = Session["ReportSession"] as List<string>;
                dateFrom.Value = list[0];
                dateTo.Value = list[1];
                //dateFrom.Value = "2015-08-09";
            }
            if (Session["FiltersModel"] != null)
            {
                // Check and Uncheck Items in grid view according to Session Filters Model
                WMSLibrary.Filters.SetGridViewCheckState(GridViewEmployee, Session["FiltersModel"] as FiltersModel, "Employee");
            }
        }
        protected void ButtonSearchEmployee_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveEmployeeIDs();
            BindGridViewEmployee(tbSearch_Employee.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewEmployee, Session["FiltersModel"] as FiltersModel, "Employee");
        }
        protected void GridViewEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveEmployeeIDs();

            //change page index
            GridViewEmployee.PageIndex = e.NewPageIndex;
            BindGridViewEmployee("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewEmployee, Session["FiltersModel"] as FiltersModel, "Employee");
        }


        private void SaveEmployeeIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewEmployee, Session["FiltersModel"] as FiltersModel, "Employee");
            Session["FiltersModel"] = FM;
        }

        private void BindGridViewEmployee(string search)
        {
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            List<Emp> _View = new List<Emp>();
            List<Emp> _TempView = new List<Emp>();
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForCompanyFilters(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from Emp " + query);
            _View = dt.ToList<Emp>();
            if (fm.CompanyFilter.Count > 0)
            {
                foreach (var comp in fm.CompanyFilter)
                {
                    short _compID = Convert.ToInt16(comp.ID);
                    _TempView.AddRange(_View.Where(aa => aa.CompanyID == _compID).ToList());
                }
                _View = _TempView;
            }
            GridViewEmployee.DataSource = _View.Where(aa => aa.EmpName.Contains(search)).ToList();
            GridViewEmployee.DataBind();
        }


        protected void GridViewEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewEmployee.PageIndex + 1) + " of " + GridViewEmployee.PageCount;
            }
        }

        private void SaveDateSession()
        {
            List<string> list = Session["ReportSession"] as List<string>;
            list[0] = DateFrom.ToString("yyyy-MM-dd");
            list[1] = DateTo.ToString("yyyy-MM-dd");
            Session["ReportSession"] = list;
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
        private void NavigationCommonCalls(string path)
        {
            SaveDateSession();
            SaveEmployeeIDs();
            Response.Redirect(path);
        }
        protected void btnStepOne_Click(object sender, EventArgs e)
        {
            NavigationCommonCalls("~/Reports/Filters/StepOneFilter.aspx");
        }

        protected void btnStepTwo_Click(object sender, EventArgs e)
        {
            NavigationCommonCalls("~/Reports/Filters/StepTwoFilter.aspx");
        }

        protected void btnStepThree_Click(object sender, EventArgs e)
        {
            NavigationCommonCalls("~/Reports/Filters/StepThreeFilter.aspx");
        }

        protected void btnStepFour_Click(object sender, EventArgs e)
        {
            NavigationCommonCalls("~/Reports/Filters/StepFourFilter.aspx");
        }

        protected void btnStepFive_Click(object sender, EventArgs e)
        {
            NavigationCommonCalls("~/Reports/Filters/StepFiveFilter.aspx");
        }

        protected void btnStepSix_Click(object sender, EventArgs e)
        {
            SaveDateSession();
            SaveEmployeeIDs();
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            if (MyHelper.UserHasValuesInSession(fm))
            {
                Response.Redirect("~/Reports/Filters/StepSixFilter.aspx");
            }
        }


        #endregion
    }
}