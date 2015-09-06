appontoWeb.controller('LogoutController', function ($scope, $state, UserService) {
    UserService.clearUser();

    $state.go('autenticacao.entrar');
})