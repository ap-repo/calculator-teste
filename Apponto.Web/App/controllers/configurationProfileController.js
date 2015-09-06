appontoWeb.controller('ConfigurationProfileController', function ($scope, $state, $http, UserService, SystemService, toaster) {
    $scope.user = UserService.getUser();

    $scope.savePersonalInformation = function () {
        $http({
            url: SystemService.apiUrl + 'api/configurationProfile/personal',
            method: 'PUT',
            data: $scope.user
        }).success(function (data) {
            toaster.pop('success', 'Perfil', data);
            UserService.updateUserFromDatabase();
        });
    }

    $scope.saveAccessInformation = function () {
        if ($scope.profile_form.$valid)
            $http({
                url: SystemService.apiUrl + 'api/configurationProfile/access',
                method: 'PUT',
                data: $scope.user
            }).success(function (data) {
                toaster.pop('success', 'Perfil', data);
                UserService.updateUserFromDatabase();
            });
    }
})