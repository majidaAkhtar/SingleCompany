using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WMSLibrary
{
    public class Filters
    {

        public static List<int> SyncDepartmentIDs(GridView gv, List<int> DepartmentIDs)
        {
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                bool isChecked = ((CheckBox)gv.Rows[i].FindControl("CheckOne")).Checked;
                if (isChecked)  // Add to filter list
                {
                    int DepartmentID = int.Parse(gv.Rows[i].Cells[1].Text);
                    DepartmentIDs.Add(DepartmentID);
                }
                else    // Remove from filter list
                {
                    int DepartmentID = int.Parse(gv.Rows[i].Cells[1].Text);
                    for (int k = 0; k < DepartmentIDs.Count; k++)
                    {
                        if (DepartmentIDs[k] == DepartmentID)
                        {
                            DepartmentIDs.RemoveAt(k);
                        }
                    }
                }
            }
            return DepartmentIDs;
        }


        public static FiltersModel SyncGridViewIDs(GridView gv, FiltersModel filtersModel, string filterName)
        {

            if (filtersModel == null)
                return new FiltersModel();
            if (filtersModel.DepartmentFilter == null)
                return new FiltersModel();
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                bool isChecked = ((CheckBox)gv.Rows[i].FindControl("CheckOne")).Checked;
                if (isChecked)  // Add to filter list
                {
                    string ItemID = gv.Rows[i].Cells[1].Text;
                    string ItemName = gv.Rows[i].Cells[2].Text;

                    AddObjectToList(filtersModel, filterName, ItemID, ItemName);

                }
                else    // Remove from filter list
                {
                    string ItemID = gv.Rows[i].Cells[1].Text;

                    RemoveObjectFromList(filtersModel, filterName, ItemID);

                }
            }
            return filtersModel;
        }

        private static void RemoveObjectFromList(FiltersModel filtersModel, string filterName, string ItemID)
        {
            switch (filterName)
            {
                case "Company":
                    for (int k = 0; k < filtersModel.CompanyFilter.Count; k++)
                    {
                        if (filtersModel.CompanyFilter[k].ID == ItemID)
                            filtersModel.CompanyFilter.RemoveAt(k);
                    }
                    break;
                case "Department":
                    for (int k = 0; k < filtersModel.DepartmentFilter.Count; k++)
                    {
                        if (filtersModel.DepartmentFilter[k].ID == ItemID)
                            filtersModel.DepartmentFilter.RemoveAt(k);
                    }
                    break;
                case "Section":
                    for (int k = 0; k < filtersModel.SectionFilter.Count; k++)
                    {
                        if (filtersModel.SectionFilter[k].ID == ItemID)
                            filtersModel.SectionFilter.RemoveAt(k);
                    }
                    break;
                case "Location":
                    for (int k = 0; k < filtersModel.LocationFilter.Count; k++)
                    {
                        if (filtersModel.LocationFilter[k].ID == ItemID)
                            filtersModel.LocationFilter.RemoveAt(k);
                    }
                    break;
                case "Shift":
                    for (int k = 0; k < filtersModel.SectionFilter.Count; k++)
                    {
                        if (filtersModel.SectionFilter[k].ID == ItemID)
                            filtersModel.SectionFilter.RemoveAt(k);
                    }
                    break;
                case "Crew":
                    for (int k = 0; k < filtersModel.SectionFilter.Count; k++)
                    {
                        if (filtersModel.SectionFilter[k].ID == ItemID)
                            filtersModel.SectionFilter.RemoveAt(k);
                    }
                    break;
                case "Type":
                    for (int k = 0; k < filtersModel.SectionFilter.Count; k++)
                    {
                        if (filtersModel.SectionFilter[k].ID == ItemID)
                            filtersModel.SectionFilter.RemoveAt(k);
                    }
                    break;
                case "Employee":
                    for (int k = 0; k < filtersModel.SectionFilter.Count; k++)
                    {
                        if (filtersModel.SectionFilter[k].ID == ItemID)
                            filtersModel.SectionFilter.RemoveAt(k);
                    }
                    break;
            }
        }

        private static void AddObjectToList(FiltersModel filtersModel, string filterName, string ItemID, string ItemName)
        {
            switch (filterName)
            {
                case "Company":
                    if (filtersModel.CompanyFilter.Where(aa => aa.ID == ItemID).Count() == 0)
                        filtersModel.CompanyFilter.Add(new FiltersAttributes() { ID = ItemID, FilterName = ItemName });
                    break;
                case "Division":
                    if (filtersModel.DivisionFilter.Where(aa => aa.ID == ItemID).Count() == 0)
                        filtersModel.DivisionFilter.Add(new FiltersAttributes() { ID = ItemID, FilterName = ItemName });
                    break;
                case "Department":
                    if (filtersModel.DepartmentFilter.Where(aa => aa.ID == ItemID).Count() == 0)
                        filtersModel.DepartmentFilter.Add(new FiltersAttributes() { ID = ItemID, FilterName = ItemName });
                    break;
                case "Section":
                    if (filtersModel.SectionFilter.Where(aa => aa.ID == ItemID).Count() == 0)
                        filtersModel.SectionFilter.Add(new FiltersAttributes() { ID = ItemID, FilterName = ItemName });
                    break;
                case "Location":
                    if (filtersModel.LocationFilter.Where(aa => aa.ID == ItemID).Count() == 0)
                        filtersModel.LocationFilter.Add(new FiltersAttributes() { ID = ItemID, FilterName = ItemName });
                    break;
                case "Shift":
                    if (filtersModel.ShiftFilter.Where(aa => aa.ID == ItemID).Count() == 0)
                        filtersModel.ShiftFilter.Add(new FiltersAttributes() { ID = ItemID, FilterName = ItemName });
                    break;
                case "Type":
                    if (filtersModel.TypeFilter.Where(aa => aa.ID == ItemID).Count() == 0)
                        filtersModel.TypeFilter.Add(new FiltersAttributes() { ID = ItemID, FilterName = ItemName });
                    break;
                case "Employee":
                    if (filtersModel.EmployeeFilter.Where(aa => aa.ID == ItemID).Count() == 0)
                        filtersModel.EmployeeFilter.Add(new FiltersAttributes() { ID = ItemID, FilterName = ItemName });
                    break;
                case "Crew":
                    if (filtersModel.CrewFilter.Where(aa => aa.ID == ItemID).Count() == 0)
                        filtersModel.CrewFilter.Add(new FiltersAttributes() { ID = ItemID, FilterName = ItemName });
                    break;

            }
        }

        public static void SetGridViewCheckState(GridView gv, FiltersModel filtersModel, string filterName)
        {
            if (filtersModel == null)
                return;
            switch (filterName)
            {
                case "Department":
                    SetGridViewCheckStateChild(gv, filtersModel, filtersModel.DepartmentFilter);
                    break;
                case "Section":
                    SetGridViewCheckStateChild(gv, filtersModel, filtersModel.SectionFilter);
                    break;
                case "Company":
                    SetGridViewCheckStateChild(gv, filtersModel, filtersModel.CompanyFilter);
                    break;
                case "Location":
                    SetGridViewCheckStateChild(gv, filtersModel, filtersModel.LocationFilter);
                    break;
                case "Shift":
                    SetGridViewCheckStateChild(gv, filtersModel, filtersModel.ShiftFilter);
                    break;
                case "Division":
                    SetGridViewCheckStateChild(gv, filtersModel, filtersModel.DivisionFilter);
                    break;
                case "Employee":
                    SetGridViewCheckStateChild(gv, filtersModel, filtersModel.EmployeeFilter);
                    break;
                case "Crew":
                    SetGridViewCheckStateChild(gv, filtersModel, filtersModel.CrewFilter);
                    break;
                case "Type":
                    SetGridViewCheckStateChild(gv, filtersModel, filtersModel.TypeFilter);
                    break;
            }

        }

        private static void SetGridViewCheckStateChild(GridView gv, FiltersModel filtersModel, List<FiltersAttributes> list)
        {
            if (list == null)
                return;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < gv.Rows.Count; j++)
                {
                    if (list[i].ID == gv.Rows[j].Cells[1].Text)
                    {
                        ((CheckBox)gv.Rows[j].FindControl("CheckOne")).Checked = true;
                    }
                }
            }
        }

    }


    public class FiltersAttributes
    {
        public string FilterName;
        public string ID;
    }

    public class FiltersModel
    {
        public List<FiltersAttributes> CompanyFilter = new List<FiltersAttributes>();
        public List<FiltersAttributes> DivisionFilter = new List<FiltersAttributes>();
        public List<FiltersAttributes> DepartmentFilter = new List<FiltersAttributes>();
        public List<FiltersAttributes> SectionFilter = new List<FiltersAttributes>();
        public List<FiltersAttributes> LocationFilter = new List<FiltersAttributes>();
        public List<FiltersAttributes> ShiftFilter = new List<FiltersAttributes>();
        public List<FiltersAttributes> RegionFilter = new List<FiltersAttributes>();
        public List<FiltersAttributes> CityFilter = new List<FiltersAttributes>();
        public List<FiltersAttributes> CrewFilter = new List<FiltersAttributes>();
        public List<FiltersAttributes> EmployeeFilter = new List<FiltersAttributes>();
        public List<FiltersAttributes> TypeFilter = new List<FiltersAttributes>();
    }
}
