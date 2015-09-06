appontoWeb.controller('RegisterController', function ($scope, $state, $http, UserService, SystemService, toaster) {
    $scope.registerButtons = [
        { Name: 'Entrada', Action: 1, Order: 1, Clicked: false, Visible: true, Order: 1, Disabled: false },
        { Name: 'Pausa', Action: 2, Order: 2, Clicked: false, Visible: true, Order: 2, Disabled: true },
        { Name: 'Volta', Action: 3, Order: 3, Clicked: true, Visible: false, Order: 3, Disabled: false },
        { Name: 'Saída', Action: 4, Order: 4, Clicked: false, Visible: true, Order: 4, Disabled: true }
    ]

    $scope.register = {};
    getDayRegister();

    $scope.saveRegister = function (action) {
        var user = UserService.getUser();
        $scope.register.action = {};
        $scope.register.user = user;

        $scope.register.action.id = action;

        if (user.Configuration.ConfigurationLimitation.LimitationType.Id == 2) {
            navigator.geolocation.getCurrentPosition(function (pos) {
                $scope.$apply(function () {
                    $scope.register.latitude = pos.coords.latitude;
                    $scope.register.longitude = pos.coords.longitude;

                    register();
                });
            }, function (error) {
                switch (error.code) {
                    case error.PERMISSION_DENIED:
                        toaster.pop('warning', 'Localização', 'Você não autorizou a localização. Por favor habilite essa funcionalidade.');
                        break;
                    case error.POSITION_UNAVAILABLE:
                        toaster.pop('warning', 'Localização', 'Não conseguimos encontrar você. Sua localização está indisponível.');
                        break;
                    case error.TIMEOUT:
                        toaster.pop('warning', 'Localização', 'Esgotou o tempo para localizá-lo. Tente novamente.');
                        break;
                    case error.UNKNOWN_ERROR:
                        toaster.pop('error', 'Localização', 'Aconteceu algo inesperado. Tente novamente.');
                        break;
                }
            },
        {
            maximumAge: 600000,
            timeout: 10000,
            enableHighAccuracy: true
        });
        } else {
            register();
        }
    }

    function register() {
        $http({
            url: SystemService.apiUrl + 'api/register',
            method: 'POST',
            data: $scope.register
        }).success(function (data) {
            toaster.pop('success', 'Registro de ponto', data);
            getDayRegister();
        });
    }

    function getDayRegister() {
        $http({
            url: SystemService.apiUrl + 'api/register/' + UserService.getUser().Id + '/day',
            method: 'GET'
        }).success(function (data) {
            $scope.records = [];

            $.each(data, function () {
                this.DateFormatedHourMinute = moment(this.DateMilliseconds).format('HH:mm');

                if (this.Action.Id == 1) {
                    $scope.registerButtons[0].Clicked = true;
                    $scope.registerButtons[0].Disabled = true;

                    $scope.registerButtons[1].Disabled = false;
                    $scope.registerButtons[1].Clicked = false;

                    $scope.registerButtons[3].Disabled = false;
                    $scope.registerButtons[3].Clicked = false;
                } else if (this.Action.Id == 2) {
                    $scope.registerButtons[1].Clicked = true;
                    $scope.registerButtons[1].Disabled = true;
                    $scope.registerButtons[1].Visible = false;

                    $scope.registerButtons[2].Visible = true;

                    $scope.registerButtons[3].Disabled = true;
                    $scope.registerButtons[3].Clicked = false;
                } else if (this.Action.Id == 3) {
                    $scope.registerButtons[1].Clicked = false;
                    $scope.registerButtons[1].Disabled = false;
                    $scope.registerButtons[1].Visible = true;

                    $scope.registerButtons[2].Visible = false;

                    $scope.registerButtons[3].Disabled = false;
                    $scope.registerButtons[3].Clicked = false;
                } else if (this.Action.Id == 4) {
                    $scope.registerButtons[1].Clicked = true;
                    $scope.registerButtons[1].Disabled = true;
                    $scope.registerButtons[1].Visible = true;

                    $scope.registerButtons[3].Clicked = true;
                    $scope.registerButtons[3].Disabled = true;
                }
            });

            $scope.records = data;
        }).error(function (data) {
            alert('Não foi possível salvar');
        });
    }
})