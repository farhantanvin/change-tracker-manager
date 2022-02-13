angular.module('GHIApp', []).controller('EventTypeController', function ($scope, $http) {

    function GetInitialFn() {
        $scope.newData = { Id: 0, EventTypeName: "" };
    }

    GetInitialFn();
    GetDataFn();
  
   

    //Get All Data With Data Table
    function GetDataFn() {
        $('#dataTable').DataTable().destroy();
        $http.get('/api/EventTypeApi/Get').then(function (r) {
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

    //Save & Update Function
    $scope.SubmitFn = function (x) {
        if (x.Id === 0) {
            $http.post('/api/EventTypeApi/Post', x).then(function (r) {
                if (r.data == 1) {
                    messageLoad("Data Saved Successfully !");
                    GetInitialFn();
                    GetDataFn();
                }
                else {
                    messageLoad("Event Type Already Exists !");
                }
            });
        } else {
            $http.put('/api/EventTypeApi/Put/' + x.Id, x).then(function (r) {
                messageLoad(r.data);
                GetInitialFn();
                $("#btnSubmit").html('<i class="fa fa-save"></i> Save');
            });
        }
    };


    //Clear Field Function
    $scope.ClearFn = function () {
        GetInitialFn();
        $("#btnSubmit").html('<i class="fa fa-save"></i> Save');
    };


    //Edit Function
    $scope.EditFn = function (x, y) {
        $scope.newData = x;
        $scope.selectedRow = y;
        $("#btnSubmit").html('<i class="fa fa-edit"></i> Update');
    };



    //Status Change Function
    $scope.StatusFn = function (x, y) {
        $scope.selectedRow = y;
        $("#btnSubmit").html('<i class="fa fa-save"></i> Save');
        $http.get('/api/EventTypeApi/Get/' + x).then(function (r) {
            messageLoad(r.data);
            GetInitialFn();
            GetDataFn();
        });
    };

    //Delete function
    $scope.DeleteFn = function (x, y) {
        $scope.selectedRow = y;
        removeData(x, y);
    };

    function removeData(x, y) {
        swal({
            title: "Are you sure to delete this record ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes",
            closeOnConfirm: false
        },
            function () {
                $http.delete('/api/EventTypeApi/Delete/' + x).then(function (r) {
                    swal({
                        title: r.data,
                        type: "success",
                        showCancelButton: true,
                        showConfirmButton: false
                    });
                    $("button.cancel").html("<i class='fa fa-remove'></i> Close");
                    $scope.result.splice(y, 1);
                    GetDataFn();

                });
            });
        $("button.cancel").html("<i class='fa fa-times'></i> No");
        $("button.confirm").html("<i class='fa fa-check'></i> Yes");
    }

});

