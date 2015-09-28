app.controller('DashboardController', function ($scope, $http) {
    //for popluating the Date input with the current date
    var today = new Date();
    $scope.DateFrom = today;
    
    //array with the criteria names
    $scope.selectedRow = 0;  // initialize our variable to null
    $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
        $scope.selectedRow = index;
        switch (index)
        {
            case 0: ReRenderGraphInfo($scope.GraphData);
                break;
            case 1: ReRenderGraphInfoInOutTime($scope.GraphData); break;
            case 2: ReRenderGraphInfoExpectedTime($scope.GraphData); break;

        }
       
    }
    $scope.names = [
        'Daily Attendance','Daily In/Out Times','Daily Expected Mins'
    ];
    $scope.Criteria = {
        repeatSelect: null,
        availableOptions: [
          { id: 'C', name: 'Company' },
          { id: 'L', name: 'Location' },
          { id: 'D', name: 'Department' },
          { id: 'E', name: 'Section' },
          { id: 'S', name: 'Shift' },
          { id: 'A', name: 'Category' }
        ]
    };
    //array with the values once the criteria is selected
    $scope.Value = {
        repeatSelect: null,
        availableOptions:[]
    
    };
    //This function calls to the database to give the values stored in it 
    //by making a SUmmaryDataCriteria in the frontend
    $scope.RenderGraph = function () {

       
        $scope.finalCriteriaForDB = ('' + $scope.DateFrom.getFullYear()).slice(-2) + ('0' + ($scope.DateFrom.getMonth() + 1)).slice(-2) + ('0' + $scope.DateFrom.getDate()).slice(-2) + $scope.Criteria.repeatSelect + $scope.Value.repeatSelect;
        
        $http({ method: 'POST', url: '/Home/GetGraphValues', data: JSON.stringify({ CriteriaValue: $scope.finalCriteriaForDB }) }).
   then(function (response) {
       $scope.GraphData = response.data;
       console.log($scope.GraphData);
       switch ($scope.selectedRow) {
           case 0: ReRenderGraphInfo($scope.GraphData);
               break;
           case 1: ReRenderGraphInfoInOutTime($scope.GraphData); break;
           case 2: ReRenderGraphInfoExpectedTime($scope.GraphData);break;

       }
       
       
   }, function (response) {
       // called asynchronously if an error occurs
       // or server returns response with an error status.
   });


    };
    var ReRenderGraphInfoExpectedTime = function (graphdata)
    {
        $scope.highchartsNG.series = [{
            name: "Attendence",
            colorByPoint: true,
            data: [{
                name: "Absent Employees",
                y: graphdata.AbsentEmps
            }, {
                name: "Present Employees",
                y: graphdata.PresentEmps,
                sliced: true,
                selected: true
            }, {
                name: "On Leave",
                y: graphdata.LvEmps + graphdata.ShortLvEmps + graphdata.HalfLvEmps
            }, {
                name: "Day Off",
                y: graphdata.DayOffEmps
            }]
        }];
        $scope.highchartsNG.loading = false;
    }

    var ReRenderGraphInfoExpectedTime = function (graphdata)
    {
        $scope.highchartsNG.subtitle = (graphdata.ExpectedWorkMins / graphdata.ActualWorkMins) * 100 + "% minutes were utilized";
        $scope.highchartsNG.series = [{
            name: "Minutes",
            colorByPoint: true,
            data: [{
                name: "Expected Work Minutes",
                y: graphdata.ExpectedWorkMins
            }, {
                name: "Actual Work Minutes",
                y: graphdata.ActualWorkMins,
                sliced: true,
                selected: true
            }, {
                name: "Loss Work Minutes",
                y: graphdata.LossWorkMins
            }]
        }];
        $scope.highchartsNG.loading = false;
    }
    var ReRenderGraphInfoInOutTime = function (graphdata)
    {
    
        $scope.highchartsNG.series = [{
            name: "Attendence",
            colorByPoint: true,
            data: [{
                name: "Early In Employees",
                y: graphdata.EIEmps
            }, {
                name: "Early Out Employees",
                y: graphdata.EOEmps,
                sliced: true,
                selected: true
            }, {
                name: "Late In Employees",
                y: graphdata.LIEmps
            }, {
                name: "Late Out Employees",
                y: graphdata.LOEmps
            }]
        }];
        $scope.highchartsNG.loading = false;
    }
    //This function takes the values given back by the database after selecting everything
    //in the front end like criteria, date, value
    var ReRenderGraphInfo = function (graphdata)
    {
        
        $scope.highchartsNG.series = [{
            name: "Attendence",
            colorByPoint: true,
            data: [{
                name: "Absent Employees",
                y: graphdata.AbsentEmps
            }, {
                name: "Present Employees",
                y: graphdata.PresentEmps,
                sliced: true,
                selected: true
            }, {
                name: "On Leave",
                y: graphdata.LvEmps + graphdata.ShortLvEmps + graphdata.HalfLvEmps
            }, {
                name: "Day Off",
                y: graphdata.DayOffEmps
            }]
        }];
        $scope.highchartsNG.loading = false;

    };
    //Once the criteria changes a backend call is made which gets all the unique values of the 
    //selected criteria from the dailysummary table. e.g. if we select C all the  companies will
    //be rendered in the frontend but the  companies wont be duplicated. although in the dailysummary
    //table there are duplicates for the same company the backend will return all the unique company
     //values which are stored in the daily summary
    $scope.$watch('Criteria', function () {
        var GraphClass = { Criteria: $scope.Criteria.repeatSelect };
        
       
        $http({ method: 'POST', url: '/Home/GetCriteriaNames', data: JSON.stringify(GraphClass) }).
   then(function (response) {
       $scope.Value.availableOptions = response.data;
   }, function (response) {
       // called asynchronously if an error occurs
       // or server returns response with an error status.
   });
    },true);
    
    
    $scope.highchartsNG = {
        options: {
            chart: {
                type: 'pie'
            }
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false
                },
                showInLegend: true
            }
        },
        series: [{
            name: "Employees",
            colorByPoint: true,
            data: [{
                name: "Absent Employees",
                y: 56.33
            }, {
                name: "Present Employees",
                y: 24.03,
                sliced: true,
                selected: true
            }, {
                name: "On Leave",
                y: 10.38
            }, {
                name: "Day Off",
                y: 4.77
            }]
        }],
        title: {
            text: 'Daily Summary'
        },
        //once we have fetched the info from the database we will do loading :false
        loading: true
    }
   

});