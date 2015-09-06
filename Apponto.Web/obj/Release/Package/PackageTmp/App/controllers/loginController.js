appontoWeb.controller('LoginController', function ($scope, $state) {
    $scope.go = function (route) {
        $state.go(route);
    };

    $scope.active = function (route) {
        return $state.is(route);
    };

    $scope.$on("$stateChangeSuccess", function () {
        $scope.loginMenuList.forEach(function (loginMenu) {
            loginMenu.active = $scope.active(loginMenu.route);
        });
    });

    //de acordo com perfil, montar o menu
    $scope.loginMenuList = [
        { name: 'Entrar', route: 'autenticacao.entrar', order: 1, active: true },
        { name: 'Inscrever-se', route: 'autenticacao.inscrever', order: 2, active: false }
    ]

    $scope.go('autenticacao.entrar');
})