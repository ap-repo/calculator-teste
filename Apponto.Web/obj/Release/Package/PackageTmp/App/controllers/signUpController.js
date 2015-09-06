appontoWeb.controller('SignUpController', function ($scope, $http, $state, UserService, SystemService) {
    //INSCREVER-SE

    $scope.user = {};
    $scope.saveCompany = false;

    $scope.signUp = function () {
        if ($scope.signup_form.$valid)
            $http({
                url: SystemService.apiUrl + 'api/signup',
                method: 'POST',
                data: $scope.user
            }).success(function (data) {
                UserService.setUser(data);
                $state.go('principal');
            });
    }
});