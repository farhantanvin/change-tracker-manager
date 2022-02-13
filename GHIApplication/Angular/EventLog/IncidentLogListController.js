angular.module('GHIApp', []).controller('IncidentLogListController', function ($scope, $http) {

    function GetInitialFn() {
        $scope.newData =
        {
            Id: 0,
            IncidentId: 0,
            InvestigationDate: new Date(),
            Location: '',
            InvestigationTeam: '',
            WhatHappened: '',
            WhatTask: '',
            WhatFactor: 0,
            OtherFactor: '',
            Comments: ''
        };
    }

    GetInitialFn();

    GetDataFn();
  
   
    //Get All Data With Data Table
    function GetDataFn() {
        $('#dataTable').DataTable().destroy();
        $http.get('/api/IncidentLogApi/Get').then(function (r) {
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

    //Investigation Form Function
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

    //Save Investigation Function
    $scope.SaveInvestigation = function (x) {
        if (x != null) {
            $http.post('/api/IncidentLogApi/PostInvestigationSave', x).then(function (r) {
                messageLoad(r.data);
                $(".closeModal").click();
                GetInitialFn();
                GetDataFn();
            });
        }
    }

    $('.saveBtn').on('click', function () {

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

        $scope.newData.WhatFactor   = factorVal;
        $scope.newData.incidentType = incidentTypeVal;


    });

});

