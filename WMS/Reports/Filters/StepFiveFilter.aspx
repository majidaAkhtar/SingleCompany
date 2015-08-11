<%@ Page Title="" Language="C#" MasterPageFile="~/ReportingEngine.Master" AutoEventWireup="true" CodeBehind="StepFiveFilter.aspx.cs" Inherits="WMS.Reports.Filters.StepFiveFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="container" style="margin-left:0;margin-right:0;">
        <div class="col-sm-3 col-md-3 col-lg-3" >
            <!-- Sidebar -->
            <div id="sidebar-wrapper">
                <ul class="sidebar-nav">
                    <li class="sidebar-brand">
                        <h4>Filters Navigation</h4>
                    </li>
                    <li >
                        <a class="inactive-link" href="StepOneFilter.aspx">Step One<p>Company, Locations</p></a>
                        
                    </li>
                    <li>
                        <a class="inactive-link" href="StepTwoFilter.aspx">Step Two<p>Divisions, Shifts</p></a>
                    </li>
                    <li>
                        <a class="inactive-link" href="StepThreeFilter.aspx">Step Three<p>Departments, Employee Type</p></a>
                    </li>
                    <li>
                        <a class="inactive-link" href="StepFourFilter.aspx">Step Four<p>Sections, Crew</p></a>
                    </li>
                    <li>
                        <a class="active-link" href="StepFiveFilter.aspx">Step Five<p>Employee</p></a>
                    </li>
                    <li>
                        <a class="inactive-link" href="">Finish<p>Generate Report</p></a>
                    </li>
                    <div style=" margin-left:40px; margin-top:20px">
                        <asp:Button ID="ButtonSkip" runat="server"  Text="Skip"  CssClass="btn-warning btn-sm btnCustomMargin" OnClick="ButtonSkip_Click" />
                        <asp:Button ID="ButtonNext" runat="server"  Text="Next" CssClass="btn-info btn-sm"  OnClick="ButtonNext_Click" />
                        <asp:Button ID="ButtonFinish" runat="server"  Text="Finish"  CssClass="btn-success btn-sm" OnClick="ButtonFinish_Click" />
                    </div>
                </ul>
                
            <!-- /#sidebar-wrapper -->
        </div>
        </div>
        <div class="col-sm-9 col-md-9 col-lg-9">
                <div class="row">
                    <div class="col-md-8">
                        <div class="row"> 
                            <div class="col-md-4">
                                <h3>Apply Filters</h3>
                            </div>
                            <div class="col-md-8">
                                
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-6">
                                From : <input id="dateFrom"  class="input-sm"  runat="server" type="date" />
                            </div>
                            <div class="col-md-6">
                                To : <input id="dateTo" class="input-sm"  runat="server" type="date" />
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="filterHeader"><span class="FilterNameHeading">Employee</span>
                                 <span style="margin-left:10px"><asp:TextBox ID="tbSearch_Employee" CssClass="input-field" runat="server" /> <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn-primary" OnClick="ButtonSearchEmployee_Click" /></span></div>
                            <section>
                            <asp:GridView ID="GridViewEmployee" runat="server" Width="300px" AutoGenerateColumns="False" PagerStyle-CssClass="pgr" CssClass="Grid"                              GridLines="None" AllowPaging="True" AllowSorting="True"                                                OnPageIndexChanging="GridViewEmployee_PageIndexChanging" ForeColor="Black" OnRowDataBound="GridViewEmployee_RowDataBound" ShowFooter="True"  >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <%--<asp:CheckBox ID="CheckAll" runat="server" />--%>
                                            <input style="margin-left:6px" id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 'GridViewEmployee');" 
                                            runat="server" type="checkbox" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox style="margin-left:6px"  ID="CheckOne" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="EmpID" HeaderText="ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Name" />
                    
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black" Wrap="False" />
                                <HeaderStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black" />
                                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev" Mode="NextPreviousFirstLast" />
                                <PagerStyle BackColor="White" ForeColor="#0094FF" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </section>
                        </div>
                        <hr />
                    </div>
                    <section class="col-md-4 selected-filters-wrapper">
                    <h2>Selected Filters...</h2><hr />
                    <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).CompanyFilter.Count > 0)
                       { 
                     { Response.Write("<h3>Companies</h3>"); }
                     foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).CompanyFilter)
                        {
                            { Response.Write("<br>" + item.FilterName); }
                        } 
                    }%>
                    <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).LocationFilter.Count > 0)
                       { 
                     { Response.Write("<h3>Locations</h3>"); }
                     foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).LocationFilter)
                        {
                            { Response.Write("<br>" + item.FilterName); }
                        } 
                    }%>
                        <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).DivisionFilter.Count > 0)
                       { 
                     { Response.Write("<h3>Divisions</h3>"); }
                     foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).DivisionFilter)
                        {
                            { Response.Write("<br>" + item.FilterName); }
                        } 
                    }%>
                        <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).ShiftFilter.Count > 0)
                       { 
                     { Response.Write("<h3>Shifts</h3>"); }
                     foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).ShiftFilter)
                        {
                            { Response.Write("<br>" + item.FilterName); }
                        } 
                    }%>
                         <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).DepartmentFilter.Count > 0)
                       { 
                     { Response.Write("<h3>Departments</h3>"); }
                     foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).DepartmentFilter)
                        {
                            { Response.Write("<br>" + item.FilterName); }
                        } 
                    }%>
                         <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).TypeFilter.Count > 0)
                       { 
                     { Response.Write("<h3>Employee Type</h3>"); }
                     foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).TypeFilter)
                        {
                            { Response.Write("<br>" + item.FilterName); }
                        } 
                    }%>
                         <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).SectionFilter.Count > 0)
                       { 
                     { Response.Write("<h3>Section</h3>"); }
                     foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).SectionFilter)
                        {
                            { Response.Write("<br>" + item.FilterName); }
                        } 
                    }%>
                        <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).CrewFilter.Count > 0)
                       { 
                     { Response.Write("<h3>Crew</h3>"); }
                     foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).CrewFilter)
                        {
                            { Response.Write("<br>" + item.FilterName); }
                        } 
                    }%>
                         <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).EmployeeFilter.Count > 0)
                       { 
                     { Response.Write("<h3>Employee</h3>"); }
                     foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).EmployeeFilter)
                        {
                            { Response.Write("<br>" + item.FilterName); }
                        } 
                    }%>
                </section>
                </div>
                <div class="row">
                    
                </div>
        </div>
    </section>
</asp:Content>

