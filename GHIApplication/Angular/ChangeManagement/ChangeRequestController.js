angular.module('GHIApp', []).controller('ChangeRequestController', function ($scope, $http) {

    //Get Session Value
    var sessionValue = $('#sessionValue').val();


    //Get Initial Function
    function GetInitialFn() {
        $scope.newData = {
            Id: 0,
            RequestBy: sessionValue,
            RequestDate: new Date(),
            Description: '',
            ChangeType: 0,
            ChangeOther: '',
            ChangePriority: '0',
            ChangeImpact: '',
            RollBackPlan: '',
            OtherFieldShow: 0
        };
    }

    GetInitialFn();



    //Get Change Type Data
    $http.get('/api/DropDownApi/GetChangeTypeData').then(function (r) {
        $scope.ChangeTypeData = [];
        $scope.ChangeTypeData.push({ Id: 0, ChangeTypeName: 'Select Change Type' });
        if (r.data.length > 0)
            for (var i = 0; i < r.data.length; i++) {
                $scope.ChangeTypeData.push(r.data[i]);
            }
    });

    //Other Field Show Function
    $scope.OtherFieldShow = function (x) {
        if (x == 5) {
            $scope.newData.OtherFieldShow = 1;
        }
        else {
            $scope.newData.OtherFieldShow = 0;
        }
    }

    //Save Function
    $scope.SaveFn = function (x) {
        if (x.RequestDate != "" && x.Description != "" && x.ChangeType != "") {
            $http.post('/api/ChangeApi/PostSaveRequest', x).then(function (r) {
                messageLoad(r.data);
                GetInitialFn();
            });
        }
    }

    //Clear Function
    $scope.ClearFn = function () {
        GetInitialFn();
    }
});

