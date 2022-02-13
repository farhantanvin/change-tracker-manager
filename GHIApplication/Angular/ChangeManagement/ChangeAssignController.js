angular.module('GHIApp', []).controller('ChangeAssignController', function ($scope, $http) {


    //Get Session Value
    var sessionValue = $('#sessionValue').val();


    //Master Initial Load Function
    function GetMasterInitialFn() {
        $scope.newData = { Id: 0, ApprovedDate: new Date(), ApprovedComments: "" };
    }

    GetMasterInitialFn();

    //Get Card No Data
    $http.get('/api/DropDownApi/GetCardNoData').then(function (r) {
        if (r.data.length > 0)
            $scope.CardNoData = r.data;
    });

    //Get All Data With Data Table
    GetDataFn();
    function GetDataFn() {
        $('#dataTable').DataTable().destroy();
        $http.get('/api/ChangeApi/GetAssignPendingList').then(function (r) {
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

    //Risk Assessment  Form Function
    $scope.RiskAssessmentForm = function (x, y) {
        $scope.newData.Id = x.Id;
        $scope.selectedRow = y;
        if (x.Id > 0) {
            $http.post('/api/ChangeApi/PostRiskAssessmentData', x).then(function (r) {
                if (r.data.length > 0) {
                    $scope.RiskRowData = r.data;
                }
            });
        }

    }


    //Submit Assign Person Function
    $scope.SubmitAssignPerson = function (x) {
        if (x != null) {
            $http.post('/api/ChangeApi/PostAssignPerson', x).then(function (r) {
                messageLoad(r.data);
                GetMasterInitialFn();
                GetDataFn();
                $("#assesmentModal .close").click();
            });
        }
    }


});

