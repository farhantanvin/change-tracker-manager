angular.module('GHIApp', []).controller('RiskAssessmentController', function ($scope, $http) {


    //Get Session Value
    var sessionValue = $('#sessionValue').val();


    //Master Initial Load Function
    function GetMasterInitialFn() {
        $scope.masterData = { Id: 0, AssessmentBy: sessionValue, AssessmentDate: new Date() };
    }

    GetMasterInitialFn();



    //Get All Data With Data Table
    GetDataFn();
    function GetDataFn() {
        $('#dataTable').DataTable().destroy();
        $http.get('/api/ChangeApi/GetRiskAssessmentPendingList').then(function (r) {
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




    //Initial Load Function
    $scope.RiskRowData = [];
    function GetInitialFn() {
        $scope.RiskRowData.splice({});
        $scope.RiskRowData.push({
            Id: $scope.RiskRowData.length + 1,
            Service: "",
            Vulnerability: "",
            Threat: "",
            Plan: ""
        });
    }
    GetInitialFn();


    //Add Row Function
    $scope.AddRow = function () {
        var data = {
            Id: $scope.RiskRowData.length + 1,
            Service: "",
            Vulnerability: "",
            Threat: "",
            Plan: ""
        };
        $scope.RiskRowData.push(data);
    };

    //Delete Row Function
    $scope.DeleteRow = function (x) {
        $scope.RiskRowData.splice(x, 1);
    };

    //Risk Assessment  Form Function
    $scope.RiskAssessmentForm = function (x, y) {
        $scope.selectedRow = y;
        $scope.masterData.Id = x.Id;
    }


    //Submit Risk Assessment  Function
    $scope.SubmitRiskAssessment = function (x, y) {
        if (x != null) {
            var xData = {
                Id: y.Id,
                AssessmentDate: y.AssessmentDate,
                RowData: x
            };
            $http.post('/api/ChangeApi/PostRiskAssessment', xData).then(function (r) {
                messageLoad(r.data);
                $scope.selectedRow = null;
                GetInitialFn();
                GetDataFn();
                $("#assesmentModal .close").click();
            });
        }
        else {
            messageLoad("Nothing to Submit !");
        }
    }
});

