appontoWeb.controller('SignInController', function ($scope, $http, $state, $rootScope, UserService, SystemService) {
    //AUTENTICAR

    $scope.user = {};

    $scope.signIn = function () {
        if ($scope.signin_form.$valid)
            $http({
                url: SystemService.apiUrl + 'api/signin',
                method: 'POST',
                data: $scope.user
            }).success(function (data) {
                if ($scope.user.savePassword)
                    data.savePassword = true;

                UserService.setUser(data);
                $state.go('principal');
            });
    }
})