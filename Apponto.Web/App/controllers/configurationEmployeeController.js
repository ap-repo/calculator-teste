appontoWeb.controller('ConfigurationEmployeeController', function ($scope, $state, $http, SystemService, UserService) {
    $scope.user = UserService.getUser();

    function getEmployees() {
        $http({
            url: SystemService.apiUrl + 'api/configurationEmployee?companyId=' + $scope.user.Company.Id,
            method: 'GET'
        }).success(function (data) {
            $scope.employees = data;
        });
    }

    getEmployees();

    $scope.selectEmployee = function (userId) {
        $state.go('usuario', { 'userId': userId });
    }
})