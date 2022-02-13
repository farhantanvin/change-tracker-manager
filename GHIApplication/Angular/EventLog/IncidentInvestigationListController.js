angular.module('GHIApp', []).controller('IncidentInvestigationListController', function ($scope, $http) {

  

    GetDataFn();
  
   
    //Get All Data With Data Table
    function GetDataFn() {
        $('#dataTable').DataTable().destroy();
        $http.get('/api/IncidentLogApi/GetInvestigationData').then(function (r) {
            $scope.result = r.data;
            angular.element(document).ready(function () {
                $('#dataTable').DataTable({
                    "lengthMenu": [[5, 10, 20, -1], [5, 10, 20, "All"]],
                    "language":
                    {
                        "searchPlaceholder": "Type your search key"
                    },
                    "columnDefs": [
                        { orderable: false, targets: -1 }
                    ],
                    dom: 'Blfrtip',
                    responsive: true
                });
            });
        });
    }



    //View Form Function
    $scope.ViewFn = function (x, y) {
        $scope.selectedRow = y;
        $http.post('/api/IncidentLogApi/PostInvestigationData/', x).then(function (d) {
            $scope.newData = d.data;
        });
    }

    //Get Card No Data
    $http.get('/api/DropDownApi/GetCardNoData').then(function (r) {
        $scope.CardNoData = [];
        if (r.data.length > 0)
            for (var i = 0; i < r.data.length; i++) {
                $scope.CardNoData.push(r.data[i]);
            }
    });

    //Edit Form Function
    $scope.EditFn = function (x, y) {
        $scope.selectedRow = y;
        $http.post('/api/IncidentLogApi/PostInvestigationData/', x).then(function (d) {
            $scope.editData = d.data;
            $scope.editData.InvestigationDate = new Date(d.data.InvestigationDate);
        });
    }

    //Update Form Data Function
    $scope.UpdateInvestigation = function (x) {

        var factorVal = '';
        var incidentTypeVal = '';

        $('[name="whatFactor"]').each(function (i, e) {
            if ($(e).is(':checked')) {
                var comma = factorVal.length === 0 ? '' : ',';
                factorVal += (comma + e.value);
            }
        });

        $('[name="incidentType"]').each(function (i, e) {
            if ($(e).is(':checked')) {
                var comma = incidentTypeVal.length === 0 ? '' : ',';
                incidentTypeVal += (comma + e.value);
            }
        });



        $scope.editData.whatFactor = factorVal;
        $scope.editData.incidentType = incidentTypeVal;

        $http.post('/api/IncidentLogApi/PostInvestigationUpdateData/', x).then(function (d) {
            $('.closeModal').click();
            GetDataFn();
            messageLoad(d.data);
        });
    }


    //Get Initial Report Variable Function
    function GetInitialReportFn() {
        $scope.newReportData = {
            Id: 0,
            IncidentDate: new Date(), 
            ImpactBusiness: '',
            ImpactCost: '',
            DiscoverDate: new Date(),
            RootCauseIndcident: '',
            RecoveryCost: '',
            DetailsCorrectiveAction: '',
            CorrectiveActionTakenBy: '',
            CorrectiveActionTakenByDate: new Date(),
            DetailsPreventiveAction: '',
            PreventiveActionTakenBy: '',
            PreventiveActionByDate: new Date(),
            LearningFromIncident: '',
            FollowUpConcernPerson: '',
            FollowUpDate: new Date(),
            IsmrComments: '',
            IsmrName: '',
            IsmrDate: new Date()
        };
    }

    GetInitialReportFn();




    //Investigation Report Form Function
    $scope.InvestigationFn = function (x, y) {
        $scope.selectedRow = y;
        $scope.newData.IncidentId = x.Id;
    }

    //Get Card No Data
    $http.get('/api/DropDownApi/GetCardNoData').then(function (r) {
        $scope.CardNoData = [];
        if (r.data.length > 0)
            for (var i = 0; i < r.data.length; i++) {
                $scope.CardNoData.push(r.data[i]);
            }
    });




    //Clear Report Function
    $scope.ClearReportFn = function () {
        GetInitialReportFn();
    }


    //View Rport Form Function
    $scope.ViewReportFn = function (x, y) {
        $scope.selectedRow = y;
        $http.post('/api/IncidentLogApi/PostInvestigationData/', x).then(function (d) {
            $scope.newReportDataFromDB = d.data;
            $scope.newReportDataFromDB.PlaceOfIncident = d.data.Location;
            $scope.newReportDataFromDB.IncidentType = d.data.incidentType;
        });

    }


    //Save Report Function
    $scope.SaveReportFn = function (x, y) {
        var allData = {
            Id: 0,
            IncidentInvestigationReport1: x,
            IncidentInvestigationReport2: y
        };

        if (x.ImpactBusiness != "" && x.ImpactCost != " ") {
            $http.post('/api/IncidentReportApi/PostInvestigationReportSave', allData).then(function (r) {
                $('.closeModal').click();
                messageLoad(r.data);
                GetInitialReportFn();
                GetDataFn();
            });
        }
    }


});

