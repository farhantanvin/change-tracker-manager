angular.module('GHIApp', []).controller('ChangeReportController', function ($scope, $http) {

    //Report Initial Load Function
    function GetInitialFn() {
        $scope.filterData = { Year: new Date().getFullYear(), ChangeType: 0};
    }
    GetInitialFn();
    
    //Get Year
    $scope.yearData = [{ value: 'Select Year' }];
    for (var i = 2000; i <= new Date().getFullYear(); i++) {
        $scope.yearData.push({ value: i });
    }
 
    //Get Month
    //let d = new Date();
    //$scope.monthData = Array.apply(null, Array(12)).map((v, k) => d.setMonth(k));

    //Get Change Type Data
    $http.get('/api/DropDownApi/GetChangeTypeData').then(function (r) {
        $scope.ChangeTypeData = [];
        $scope.ChangeTypeData.push({ Id: 0, ChangeTypeName: 'Select Change Type' });
        if (r.data.length > 0)
            for (var i = 0; i < r.data.length; i++) {
                $scope.ChangeTypeData.push(r.data[i]);
            }
    });

    //Get Card No Data
    $http.get('/api/DropDownApi/GetCardNoData').then(function (r) {
        if (r.data.length > 0)
            $scope.CardNoData = r.data;
    });

    //Search Function
    $scope.SearchFn = function (x) {
        if (x!=null) {
            $http.post('/api/ChangeApi/PostChangeReportData', x).then(function (r) {
                $scope.result = r.data;
            });
        }
    };
});

