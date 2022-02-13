angular.module('GHIApp', []).controller('EventLogListController', function ($scope, $http) {

  
    GetDataFn();
  
   
    //Get All Data With Data Table
    function GetDataFn() {
        $('#dataTable').DataTable().destroy();
        $http.get('/api/EventLogApi/Get').then(function (r) {
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


    //Delete function
    $scope.IncidentFn = function (x, y) {
        $scope.selectedRow = y;
        incidentData(x, y);
    };

    function incidentData(x, y) {
        swal({
            title: "Are you sure to send this record for incident ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes",
            closeOnConfirm: false
        },
            function () {
                $http.put('/api/EventLogApi/Put/' + x, y).then(function (r) {
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
                $http.delete('/api/EventLogApi/Delete/' + x).then(function (r) {
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

    //Edit Form Function
    $scope.EditFn = function (x, y) {
       

        $scope.selectedRow = y;
        $http.post('/api/EventLogApi/PostEventFilterData/', x).then(function (d) {
            $scope.newData = d.data;
            $scope.newData.EventDate = new Date(d.data.EventDate);

            var timeControl = document.querySelector('input[type="time"]');
            timeControl.value = d.data.EventTime;
            $scope.EventTime = timeControl;

            localStorage.setItem("myTime", d.data.EventTime);
        });
    }

    //Update Form Data Function
    $scope.UpdateEventLog = function (x) {

        var oldTime = localStorage.getItem("myTime");
        if (oldTime !== x.EventTime) {
            x.EventTime = SetTimeFn(x.EventTime);
        }

        $http.post('/api/EventLogApi/PostEventUpdateData/', x).then(function (d) {
            $('.closeModal').click();
            GetDataFn();
            messageLoad(d.data);
            localStorage.removeItem("myTime");
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


    //Get Event Type Data
    $http.get('/api/DropDownApi/GetEventTypeData').then(function (r) {
        $scope.EventTypeData = [];
        $scope.EventTypeData.push({ Id: 0, EventTypeName: 'Select Event Type' });
        if (r.data.length > 0)
            for (var i = 0; i < r.data.length; i++) {
                $scope.EventTypeData.push(r.data[i]);
            }
    });

    //remove Blank option
    function RemoveBlankOption() {

        setTimeout(function () {
            $('select option').filter(function () {
                return $(this).val().trim() === "?";
            }).remove();
        }, 500);

    }


    RemoveBlankOption();

    function SetTimeFn(x) {
        return x.getHours() + ":" + x.getMinutes() + ":" + 00;
    }
  
});

