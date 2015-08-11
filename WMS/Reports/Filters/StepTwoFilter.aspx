<%@ Page Title="" Language="C#" MasterPageFile="~/ReportingEngine.Master" AutoEventWireup="true" CodeBehind="StepTwoFilter.aspx.cs" Inherits="WMS.Reports.Filters.StepTwoFilter" %>
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
                        <asp:LinkButton ID="btnStepOne" runat="server" CssClass="inactive-link" OnClick="btnStepOne_Click" >Step One<p>Company, Locations</p></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnStepTwo" runat="server" CssClass="active-link" OnClick="btnStepTwo_Click" >Step Two<p>Divisions, Shifts</p></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnStepThree" runat="server"  CssClass="inactive-link" OnClick="btnStepThree_Click" >Step Three<p>Departments, Employee Type</p></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnStepFour" runat="server" CssClass="inactive-link" OnClick="btnStepFour_Click" >Step Four<p>Sections, Crew</p></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnStepFive" runat="server"  CssClass="inactive-link" OnClick="btnStepFive_Click" >Step Five<p>Employee</p></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnStepSix" runat="server" CssClass="inactive-link" OnClick="btnStepSix_Click" >Finish<p>Generate Report</p></asp:LinkButton>

                    </li>
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
                            <div class="filterHeader"><span class="FilterNameHeading">Division</span>
                                 <span style="margin-left:10px"><asp:TextBox ID="TextBoxSearchDivision" CssClass="input-field" runat="server" /> <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn-primary" OnClick="ButtonSearchDivision_Click" /></span></div>
                            <section>
                            <asp:GridView ID="GridViewDivision" runat="server" Width="300px" AutoGenerateColumns="False" PagerStyle-CssClass="pgr" CssClass="Grid"                              GridLines="None" AllowPaging="True" AllowSorting="True"                                                OnPageIndexChanging="GridViewDivision_PageIndexChanging" ForeColor="Black" OnRowDataBound="GridViewDivision_RowDataBound" ShowFooter="True"  >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <%--<asp:CheckBox ID="CheckAll" runat="server" />--%>
                                            <input style="margin-left:6px" id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 'GridViewDivision');" 
                                            runat="server" type="checkbox" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox style="margin-left:6px"  ID="CheckOne" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="DivisionID" HeaderText="ID" />
                                        <asp:BoundField DataField="DivisionName" HeaderText="Name" />
                    
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
                        <div class="row">
                             <div class="filterHeader"><span class="FilterNameHeading">Shifts</span>
                                 <span style="margin-left:10px"><asp:TextBox ID="tbSearch_Shift" CssClass="input-field" runat="server" /> <asp:Button ID="Button2" runat="server" Text="Search" CssClass="btn-primary" OnClick="ButtonSearchShift_Click" /></span>
                        </div>
                             <section>
                            <asp:GridView ID="GridViewShift" runat="server" Width="300px" AutoGenerateColumns="False" PagerStyle-CssClass="pgr" CssClass="Grid"                              GridLines="None" AllowPaging="True" AllowSorting="True"                                                OnPageIndexChanging="GridViewShift_PageIndexChanging" BorderColor="#0094FF" BorderStyle="None" OnRowDataBound="GridViewShift_RowDataBound" ShowFooter="True" BorderWidth="1px"  >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <%--<asp:CheckBox ID="CheckAll" runat="server" />--%>
                                            <input style="margin-left:6px" id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 'GridViewShift');" 
                                            runat="server" type="checkbox" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox style="margin-left:6px"  ID="CheckOne" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="ShiftID" HeaderText="ID" />
                                        <asp:BoundField DataField="ShiftName" HeaderText="Name" />
                    
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black" Wrap="False" />
                                <HeaderStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black" BorderColor="#0094FF" BorderStyle="None" BorderWidth="1px" />
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
