appontoWeb.controller('ConfigurationController', function ($scope, $state, UserService, SystemService) {
    var permissions = UserService.getRoles(2); //2 : Menu Configurações
    if (permissions.length > 0) {
        $state.go(permissions[0].Route);
    }
    $scope.configurationMenuList = permissions;

    $scope.go = function (route) {
        $state.go(route);
    };

    $scope.active = function (route) {
        return $state.is(route);
    };

    $scope.$on("$stateChangeSuccess", function () {
        $scope.configurationMenuList.forEach(function (configurationMenu) {
            configurationMenu.Active = $scope.active(configurationMenu.Route);
        });
    });
})