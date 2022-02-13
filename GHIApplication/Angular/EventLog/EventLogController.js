angular.module('GHIApp', []).controller('EventLogController', function ($scope, $http) {

  
    //Get Initial Function
    function GetInitialFn() {
        $scope.newData = {
            Id: 0,
            EventDate: new Date(),
            EventTime: '',
            EventType: 0,
            EventDetails: '',
            EventCause: '',
            DiscoverBy: ''
        };
    }

    GetInitialFn();


    function SetTimeFn(x) {
        return x.getHours() + ":" + x.getMinutes() + ":" + 00;
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

  
    //Save Function
    $scope.SaveFn = function (x) {
        if (x.EventDate != "" && x.EventTime != "" && x.EventType != "" && x.DiscoverBy != "") {
            x.EventTime = SetTimeFn(x.EventTime);
            $http.post('/api/EventLogApi/PostSaveEventLog', x).then(function (r) {
                messageLoad(r.data);
                GetInitialFn();
                RemoveBlankOption();
            });
        }
    }

    //Clear Function
    $scope.ClearFn = function () {
        GetInitialFn();
    }

    //remove Blank option
    function RemoveBlankOption() {

        setTimeout(function () {
            $('select option').filter(function () {
                return $(this).val().trim() === "?";
            }).remove();
        }, 500);

    }


    RemoveBlankOption();
});

