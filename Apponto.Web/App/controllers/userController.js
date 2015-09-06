appontoWeb.controller('UserController', function ($scope, $state, UserService, SystemService) {
    var permissions = UserService.getRoles(3); //3 : Menu Usuário
    if (permissions.length > 0) {
        $state.go(permissions[0].Route);
    }
    $scope.userMenuList = permissions;

    $scope.go = function (route) {
        $state.go(route);
    };

    $scope.active = function (route) {
        return $state.is(route);
    };

    $scope.$on("$stateChangeSuccess", function () {
        $scope.userMenuList.forEach(function (userMenu) {
            userMenu.Active = $scope.active(userMenu.Route);
        });
    });
})