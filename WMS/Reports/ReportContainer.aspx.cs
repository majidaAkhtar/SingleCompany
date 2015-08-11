using Microsoft.Reporting.WebForms;
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

namespace WMS.Reports
{
    public partial class ReportContainer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             String reportName = Request.QueryString["reportname"];
            if (!Page.IsPostBack)
            {
                List<string> list = Session["ReportSession"] as List<string>;
                FiltersModel fm = Session["FiltersModel"] as FiltersModel;

                List<ViewAbsent> _TempViewList = new List<ViewAbsent>();
                User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
                QueryBuilder qb = new QueryBuilder();
                string query = qb.MakeCustomizeQuery(LoggedInUser);
                string _dateFrom = list[0];
                string _dateTo = list[1];
                DataTable dt = qb.GetValuesfromDB("select * from ViewAbsent " + query + " and (AttDate >= " + "'" + _dateFrom + "'" + " and AttDate <= " +"'"
               + _dateTo + "'" + " )" + " and StatusAB=1 ");
                List<ViewAbsent> _ViewList = dt.ToList<ViewAbsent>();

                
                string PathString = "";
                if (GlobalVariables.DeploymentType == false)
                {
                    PathString = "/Reports/RDLC/DRAbsent.rdlc";
                }
                else
                    PathString = "/WMS/Reports/RDLC/DRAbsent.rdlc";
                LoadReport(PathString, _ViewList, _dateFrom+" TO "+_dateTo);
                
            }
        }

        public void ReportsFilterImplementation(FiltersModel fm, List<ViewAbsent> _TempViewList, List<ViewAbsent> _ViewList)
        {
            //for company
            if (fm.CompanyFilter.Count > 0)
            {
                foreach (var comp in fm.CompanyFilter)
                {
                    short _compID = Convert.ToInt16(comp.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.CompanyID == _compID).ToList());
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
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.LocID == _locID).ToList());
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
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.ShiftID == _shiftID).ToList());
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
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.TypeID == _typeID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for crews
            if (fm.CrewFilter.Count > 0)
            {
                foreach (var cre in fm.CrewFilter)
                {
                    short _crewID = Convert.ToInt16(cre.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.CrewID == _crewID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();





            //for division
            if (fm.DivisionFilter.Count > 0)
            {
                foreach (var div in fm.DivisionFilter)
                {
                    short _divID = Convert.ToInt16(div.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.DivID == _divID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for department
            if (fm.DepartmentFilter.Count > 0)
            {
                foreach (var dept in fm.DepartmentFilter)
                {
                    short _deptID = Convert.ToInt16(dept.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.DeptID == _deptID).ToList());
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
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.SecID == _secID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //Employee
            if (fm.EmployeeFilter.Count > 0)
            {
                foreach (var emp in fm.EmployeeFilter)
                {
                    int _empID = Convert.ToInt32(emp.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.EmpID == _empID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();
        }
        private void LoadReport(string path, List<ViewAbsent> _Employee, string date)
        {
            string _Header = "Absent Report";
            this.ReportViewer1.LocalReport.DisplayName = "Absent Report";
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
            System.Security.PermissionSet sec = new System.Security.PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
            ReportViewer1.LocalReport.SetBasePermissionsForSandboxAppDomain(sec);
            IEnumerable<ViewAbsent> ie;
            ie = _Employee.AsQueryable();
            ReportDataSource datasource1 = new ReportDataSource("DataSet1", ie);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource1);
            ReportParameter rp = new ReportParameter("Date", date, false);
            ReportParameter rp1 = new ReportParameter("Header", _Header, false);
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp, rp1 });
            ReportViewer1.LocalReport.Refresh();
        }
    }
}