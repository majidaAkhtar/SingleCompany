using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS.CustomClass;
using WMS.Models;
using WMSLibrary;

namespace WMS.Reports
{
    public partial class SummaryReports : System.Web.UI.Page
    {
        String title = "";
        string _dateFrom = "";
        List<EmpPhoto> companyimage = new List<EmpPhoto>();

        TAS2013Entities contex = new TAS2013Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String reportName = Request.QueryString["reportname"];
                List<string> list = Session["ReportSession"] as List<string>;
                FiltersModel fm = Session["FiltersModel"] as FiltersModel;
                User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
                QueryBuilder qb = new QueryBuilder();
                string query = qb.MakeCustomizeQuery(LoggedInUser);
                _dateFrom = list[0];
                string _dateTo = list[1];
                DateTime DateFrom = Convert.ToDateTime(list[0]);
                DateTime DateTo = Convert.ToDateTime(list[1]);
                string PathString = "";
                string consolidatedMonth = "";
                switch (reportName)
                {
                    case "Strength":
                        List<DailySummary> _BadliList = contex.DailySummaries.Where(aa => aa.Date >= DateFrom && aa.Date <= DateTo).ToList();
                        List<DailySummary> _TempBadliList = new List<DailySummary>();
                        title = "Badli Report";
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _TempBadliList, _BadliList),_dateFrom+" TO "+_dateTo);
                        break;
                    case "WorkTime":
                        _BadliList = contex.DailySummaries.Where(aa => aa.Date >= DateFrom && aa.Date <= DateTo).ToList();
                        _TempBadliList = new List<DailySummary>();
                        title = "Badli Report";
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSWorkSummary.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSWorkSummary.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _TempBadliList, _BadliList), _dateFrom + " TO " + _dateTo);
                        break;
                    case "Consolidated":
                        _BadliList = contex.DailySummaries.Where(aa => aa.Date >= DateFrom && aa.Date <= DateTo).ToList();
                        _TempBadliList = new List<DailySummary>();
                        title = "Badli Report";
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSConsolidated.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSConsolidated.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _TempBadliList, _BadliList), _dateFrom + " TO " + _dateTo);
                        break;
                }
            }
        }

        private void LoadReport(string PathString, List<DailySummary> list, string p)
        {
            string _Header = title;
            this.ReportViewer1.LocalReport.DisplayName = title;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(PathString);
            System.Security.PermissionSet sec = new System.Security.PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
            ReportViewer1.LocalReport.SetBasePermissionsForSandboxAppDomain(sec);
            IEnumerable<DailySummary> ie;
            ie = list.AsQueryable();
            IEnumerable<DailySummary> companyImage;
            ReportDataSource datasource1 = new ReportDataSource("DataSet1", ie);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.DataSources.Add(datasource1);
            ReportParameter rp = new ReportParameter("Date", p, false);
            ReportParameter rp1 = new ReportParameter("Header", _Header, false);
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp });
            ReportViewer1.LocalReport.Refresh();
        }

        private List<DailySummary> ReportsFilterImplementation(FiltersModel fm, List<DailySummary> _TempViewList, List<DailySummary> _ViewList)
        {
            //for company
            if (fm.CompanyFilter.Count > 0)
            {
                foreach (var comp in fm.CompanyFilter)
                {
                    short _compID = Convert.ToInt16(comp.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "C" && aa.Criteria==comp.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();



            //for location
            if (fm.LocationFilter.Count > 0)
            {
                foreach (var loc in fm.LocationFilter)
                {
                    short _locID = Convert.ToInt16(loc.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "L" && aa.Criteria==loc.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for shifts
            if (fm.ShiftFilter.Count > 0)
            {
                foreach (var shift in fm.ShiftFilter)
                {
                    short _shiftID = Convert.ToInt16(shift.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "S" && aa.Criteria == shift.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();


            _TempViewList.Clear();

            //for type
            if (fm.TypeFilter.Count > 0)
            {
                foreach (var type in fm.TypeFilter)
                {
                    short _typeID = Convert.ToInt16(type.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "T" && aa.Criteria == type.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            ////for crews
            //if (fm.CrewFilter.Count > 0)
            //{
            //    foreach (var cre in fm.CrewFilter)
            //    {
            //        short _crewID = Convert.ToInt16(cre.ID);
            //        _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "C" && aa.Criteria == loc.ID).ToList());
            //    }
            //    _ViewList = _TempViewList.ToList();
            //}
            //else
            //    _TempViewList = _ViewList.ToList();
            //_TempViewList.Clear();





            ////for division
            //if (fm.DivisionFilter.Count > 0)
            //{
            //    foreach (var div in fm.DivisionFilter)
            //    {
            //        //baldi doesnt have the division id so using division name in
            //        _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "L" && aa.Criteria == loc.ID).ToList());
            //    }
            //    _ViewList = _TempViewList.ToList();
            //}
            //else
            //    _TempViewList = _ViewList.ToList();
            //_TempViewList.Clear();

            //for department
            if (fm.DepartmentFilter.Count > 0)
            {
                foreach (var dept in fm.DepartmentFilter)
                {
                    short _deptID = Convert.ToInt16(dept.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "D" && aa.Criteria == dept.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for sections
            if (fm.SectionFilter.Count > 0)
            {
                foreach (var sec in fm.SectionFilter)
                {
                    short _secID = Convert.ToInt16(sec.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "E" && aa.Criteria == sec.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            ////Employee
            //if (fm.EmployeeFilter.Count > 0)
            //{
            //    foreach (var emp in fm.EmployeeFilter)
            //    {
            //        int _empID = Convert.ToInt32(emp.ID);
            //        _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "L" && aa.Criteria == loc.ID).ToList());
            //    }
            //    _ViewList = _TempViewList.ToList();
            //}
            //else
            //    _TempViewList = _ViewList.ToList();
            //_TempViewList.Clear();


            return _ViewList;
        }

    }
}