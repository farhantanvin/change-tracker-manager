angular.module('GHIApp', []).controller('ChangePendingController', function ($scope, $http) {

    //Get Session Value
    var sessionValue = $('#sessionValue').val();

    //Master Initial Load Function
    function GetMasterInitialFn() {
        $scope.cancelData = { Id: 0, CancelBy: sessionValue, CancelComments: "" };
    }

    GetMasterInitialFn();



    //Get All Data With Data Table
    GetDataFn();

    function GetDataFn() {
        $('#dataTable').DataTable().destroy();
        $http.get('/api/ChangeApi/GetChangeRequestPendingList').then(function (r) {
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

    //Change Recommended Function
    $scope.ChangeRecommendedFn = function (x, y) {
        $scope.selectedRow = y;
        if (x.Id > 0) {
            swal({
                title: "Are you sure to approve this record ?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes",
                closeOnConfirm: false
            },
                function () {
                    $http.put('/api/ChangeApi/PutChangeRecommended/' + x.Id, x).then(function (r) {
                        swal({
                            title: r.data,
                            type: "success",
                            showCancelButton: true,
                            showConfirmButton: false
                        });
                        $scope.selectedRow = null;
                        GetDataFn();
                    });
                });
        }
    };


    //Cancel Form Function
    $scope.CancelFormFn = function (x, y) {
        $scope.cancelData.Id = x.Id;
        $scope.selectedRow = y;
    }

    //Submit Cancel Form Function
    $scope.SubmitCancel = function (x, y) {
        if (x.Id > 0) {
            $http.put('/api/ChangeApi/PutChangeCancel/' + x.Id, x).then(function (r) {
                messageLoad(r.data);
                GetDataFn();
                GetMasterInitialFn();
                $scope.selectedRow = null;
                $("#cancelModal .close").click();
            });
        }
    }

});

