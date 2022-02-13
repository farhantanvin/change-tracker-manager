angular.module('GHIApp', []).controller('DashboardController', function ($scope, $http) {

    function GetInitialFn() {
        $scope.newData = { Id: 0 };
    }

    GetInitialFn();

});

