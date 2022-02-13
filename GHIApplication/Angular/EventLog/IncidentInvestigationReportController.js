angular.module('GHIApp', []).controller('IncidentInvestigationReportController', function ($scope, $http) {



    GetDataFn();


    //Get All Data With Data Table
    function GetDataFn() {
        $('#dataTable').DataTable().destroy();
        $http.get('/api/IncidentReportApi/GetInvestigationReportData').then(function (r) {
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



});

