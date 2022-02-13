angular.module('GHIApp', []).controller('ChangeAssignByController', function ($scope, $http) {



    //Get Session Value
    var sessionValue = $('#sessionValue').val();

    //Master Initial Load Function
    function GetMasterInitialFn() {
        $scope.newData = { Id: 0, ChangedBy: sessionValue, ChangedDate: new Date()};
    }

    GetMasterInitialFn();


    //Get All Data With Data Table
    GetDataFn();
    function GetDataFn() {
        $('#dataTable').DataTable().destroy();
        $http.get('/api/ChangeApi/GetAssignByPendingList').then(function (r) {
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
    $scope.SubmitChangeBy = function (x) {
        if (x != null) {
            $http.post('/api/ChangeApi/PostChangeBy', x).then(function (r) {
                messageLoad(r.data);
                GetMasterInitialFn();
                GetDataFn();
                $("#assesmentModal .close").click();
            });
        }
    }

});

