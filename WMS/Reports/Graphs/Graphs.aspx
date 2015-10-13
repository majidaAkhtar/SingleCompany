﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ReportingEngine.Master" AutoEventWireup="true" CodeBehind="Graphs.aspx.cs" Inherits="WMS.Reports.Graphs.Graphs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style>
    .selected {
        background-color:black;
        color:white;
        font-weight:bold;
    }
</style>
    <div ng-controller="DashboardController" ng-Init="InitFunction()" class="jumbotron">
        <div class="row" style="margin-bottom:20px">
            <div class="col-md-4">
                <form name="myForm">
                    <label for="repeatSelect"> Criteria: </label>
                    <select name="repeatSelect" id="repeatSelect" ng-model="Criteria.repeatSelect" placeholder="Criteria...">
                      <option ng-repeat="option in Criteria.availableOptions" value="{{option.id}}">{{option.name}}</option>
                    </select>
                </form>
            </div>
            <div class="col-md-4">
                <form name="myForm">
                    <label for="ValueSelect"> Value: </label>
                    <select name="ValueSelect" id="ValueSelect" ng-model="Value.repeatSelect">
                        <option ng-repeat="option in Value.availableOptions" value="{{option.id}}">{{option.name}}</option>
                    </select>
                </form>
            </div>
            <div class="col-md-4">
                Select All:<input type="checkbox" ng-model="MultipleSelect">
                DateFrom : <input id="dateFrom" ng-model="DateFrom"  class="input-sm"  type="date" />
                 DateTo : <input id="date1" ng-model="DateTo"  class="input-sm"  type="date" />
                <asp:button ng-click="RenderGraph()" OnClientClick="false" Class="btn btn-success btn-sm">Fetch Summary</asp:button>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <div class="row">
                    <div style="font-size:16px; margin-bottom:20px">Select Graph Type</div>
                    <table class="table"> 
                      <tr ng-repeat="x in names" ng-class="{'selected':$index == selectedRow}" ng-click="setClickedRow($index)">
                        <td>{{ x }}</td>
   
                      </tr>
                    </table>
                </div>
               
                <div class="row">
                    <asp:button ng-click="GetBestCriteria()" OnClientClick="false">Evaluation for the past 20 days</asp:button>
                </div>
            </div>
            
            <div class="col-md-10" >
                <div class ="row"> 
                    <div class="col-md-6" >
                        <highchart id="chart1" config="highchartsNG"></highchart>
                    </div> 
                    <div class="col-md-6" >
                        <highchart id="HighchartforSameDate" config="highchartsBG"></highchart>
                    </div> 
                </div> 
            </div>
        </div>
    </div>
</asp:Content>
