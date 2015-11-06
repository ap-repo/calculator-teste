var appontoWeb = angular.module('appontoWeb', ['ui.router', 'ui.bootstrap', 'ngAnimate', 'ngCookies', 'ngTouch', 'uiGmapgoogle-maps', 'angular-loading-bar', 'checklist-model', 'toaster']);

appontoWeb.service('APIInterceptor', ['$q', '$rootScope', 'toaster', function ($q, $rootScope, toaster) {
    var service = this;

    service.request = function (config) {
        return config;
    };

    service.responseError = function (response) {
        if (response.status === 401) {
            toaster.pop('error', 'Não autorizado', response.data.Message);
        } else if (response.status === 404) {
            //TODO: logar no console response.data.Message
            toaster.pop('error', 'Não encontrado', 'Não encontramos o recurso solicitado.');
        }
        else if (response.status === 400) {
            //TODO: logar no console response.data.Message
            toaster.pop('error', 'Ops', response.data.Message);
        } else if (response.status === 500) {
            //TODO: logar no console response.data.Message
            toaster.pop('error', 'Ops', 'Tivemos um probleminha.');
        }

        return $q.reject(response);
    };
}])

appontoWeb.factory('UserService', function ($cookieStore, $http, SystemService) {
    var UserService = function () { };
    UserService.user = null;

    UserService.isAuth = function () {
        refreshUser();

        return (UserService.user != null);
    };
    UserService.setUser = function (newUser) {
        UserService.user = newUser;
        if (UserService.user == null) $cookieStore.remove('user');
        else $cookieStore.put('user', UserService.user);
    };
    UserService.getUser = function () {
        refreshUser();

        return UserService.user;
    };
    UserService.clearUser = function () {
        UserService.user = null;
        $cookieStore.remove('user');
    };
    UserService.getRoles = function (group) {
        if (UserService.isAuth()) {
            var permissions = [];
            $.each(UserService.getUser().AccessLevel, function (index, item) {
                $.each(item.Permissions, function (index, item) {
                    if (!$.contains(permissions, item) && item.PermissionGroup.Id == group) permissions.push(item);
                });
            });

            return permissions;
        }
        else return new Array();
    };
    UserService.updateUserFromDatabase = function () {
        $http({
            url: SystemService.apiUrl + 'api/user?userId=' + UserService.user.Id,
            method: 'GET'
        }).success(function (data) {
            UserService.user = data;
        });
    };
    function refreshUser() {
        if (UserService.user == null) {
            UserService.user = $cookieStore.get('user');
        }
    };

    return UserService;
});

appontoWeb.factory('SystemService', function () {
    var SystemService = function () { };

    SystemService.apiUrl = 'http://localhost/Apponto.API/';

    return SystemService;
});

appontoWeb.run(function ($rootScope, $state, UserService) {
    $rootScope.$on('$stateChangeStart', function (event, toState, toParams) {
        var requireLogin = toState.data.requireLogin;

        if (toState.name == 'autenticacao.entrar' && UserService.isAuth() && UserService.getUser().savePassword) {
            event.preventDefault();
            $state.go('principal');
        }
        else if (requireLogin && !UserService.isAuth()) {
            event.preventDefault();
            $state.go('autenticacao.entrar');
        }
    });
});

appontoWeb.directive('ngFocus', [function () {
    var FOCUS_CLASS = "ng-focused";
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ctrl) {
            ctrl.$focused = false;
            element.bind('focus', function (evt) {
                element.addClass(FOCUS_CLASS);
                scope.$apply(function () { ctrl.$focused = true; });
            }).bind('blur', function (evt) {
                element.removeClass(FOCUS_CLASS);
                scope.$apply(function () { ctrl.$focused = false; });
            });
        }
    }
}]);

appontoWeb.directive('passwordMatch', [function () {
    return {
        restrict: 'A',
        scope: true,
        require: 'ngModel',
        link: function (scope, elem, attrs, control) {
            var checker = function () {

                //get the value of the first password
                var e1 = scope.$eval(attrs.ngModel);

                //get the value of the other password  
                var e2 = scope.$eval(attrs.passwordMatch);
                return e1 == e2;
            };
            scope.$watch(checker, function (n) {

                //set the form control to valid if both 
                //passwords are the same, else invalid
                control.$setValidity("unique", n);
            });
        }
    };
}]);

appontoWeb.directive("ngUnique", ['$http', 'SystemService', function (async, SystemService) {
    return {
        restrict: 'A',
        scope: true,
        require: 'ngModel',
        link: function (scope, elem, attrs, control) {
            elem.on('blur', function (evt) {
                if (elem.val() != '')
                    scope.$apply(function () {
                        var ajaxConfiguration = { method: 'GET', url: SystemService.apiUrl + 'api/user/' + elem.val() + '/exist' };
                        async(ajaxConfiguration)
                            .success(function (data, status, headers, config) {
                                control.$setValidity('userExists', !data);
                            });
                    });
            });
        }
    };
}]);

appontoWeb.config(['$stateProvider', '$urlRouterProvider', '$httpProvider', '$locationProvider', function ($stateProvider, $urlRouterProvider, $httpProvider, $locationProvider) {

    $urlRouterProvider.otherwise("autenticacao/entrar");

    $stateProvider
        .state('principal', {
            url: '/principal',
            controller: 'RegisterController',
            templateUrl: 'views/register.html',
            data: {
                requireLogin: true
            }
        })
    .state('location', {
        url: '/localizacao',
        controller: 'LocationController',
        templateUrl: 'views/location.html',
        data: {
            requireLogin: true
        }
    })

        .state('autenticacao', {
            url: '/autenticacao',
            controller: 'LoginController',
            templateUrl: 'views/login.html',
            data: {
                requireLogin: false
            }
        })
        .state('autenticacao.inscrever', {
            url: '/inscrever',
            controller: 'SignUpController',
            templateUrl: 'views/signUp.html'
        })
        .state('autenticacao.entrar', {
            url: '/entrar',
            controller: 'SignInController',
            templateUrl: 'views/signIn.html'
        })
        .state('relatorio', {
            url: '/relatorio',
            controller: 'ReportController',
            templateUrl: 'views/report.html',
            data: {
                requireLogin: true
            }
        })
        .state('configuracao', {
            url: '/configuracao',
            controller: 'ConfigurationController',
            templateUrl: 'views/configuration.html',
            data: {
                requireLogin: true
            }
        })
        .state('configuracao.perfil', {
            url: '/perfil',
            controller: 'ConfigurationProfileController',
            templateUrl: 'views/configurationProfile.html'
        })
        .state('configuracao.funcionario', {
            url: '/funcionario',
            controller: 'ConfigurationEmployeeController',
            templateUrl: 'views/configurationEmployee.html'
        })
        .state('configuracao.permissao', {
            url: '/permissao',
            controller: 'ConfigurationPermissionController',
            templateUrl: 'views/configurationPermission.html'
        })
        .state('usuario', {
            url: '/usuario/:userId',
            controller: 'UserController',
            templateUrl: 'views/user.html',
            data: {
                requireLogin: true
            }
        })
        .state('usuario.restricao', {
            url: '/restricao',
            controller: 'UserRestrictionController',
            templateUrl: 'views/userRestriction.html',
            data: {
                requireLogin: true
            }
        })
        .state('usuario.acesso', {
            url: '/acesso',
            controller: 'UserAccessLevelController',
            templateUrl: 'views/userAccessLevel.html',
            data: {
                requireLogin: true
            }
        })
        .state('usuario.relatorio', {
            url: '/relatorio',
            controller: 'ReportController',
            templateUrl: 'views/report.html',
            data: {
                requireLogin: true
            }
        })
        .state('sair', {
            url: 'autenticacao/entrar',
            controller: 'LogoutController',
            data: {
                requireLogin: true
            }
        });

    $httpProvider.interceptors.push('APIInterceptor');
}])

appontoWeb.config(function (uiGmapGoogleMapApiProvider) {
    uiGmapGoogleMapApiProvider.configure({
        key: 'AIzaSyCAJ0tzeOBMMj3MkjvNn_BUV4eyXlz_3SE',
        v: '3.17',
        libraries: 'places'
    });
})

appontoWeb.controller('BaseController', function ($scope, $state, $location, UserService) {

    //de acordo com perfil, montar o menu
    $scope.baseMenuList = [
        { Name: 'Principal', Route: 'principal', Order: 1, Active: true, Icon: 'fa fa-home' },
        { Name: 'Minha Localização', Route: 'location', Order: 2, Active: false, Icon: 'fa fa-map-marker' },
        { Name: 'Relatório', Route: 'relatorio', Order: 3, Active: false, Icon: 'fa fa-list' },
        { Name: 'Configuração', Route: 'configuracao', Order: 4, Active: false, Icon: 'fa fa-cogs' },
        { Name: 'Sair', Route: 'sair', Order: 5, Active: false, Icon: 'fa fa-sign-out' }
    ]

    $scope.go = function (baseMenu) {
        $.each($scope.baseMenuList, function () {
            this.Active = false;
        });

        baseMenu.Active = true;
        $state.go(baseMenu.Route);
    };
})