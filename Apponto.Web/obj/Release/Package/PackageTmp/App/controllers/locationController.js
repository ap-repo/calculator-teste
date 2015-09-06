appontoWeb.controller('LocationController', function ($scope, $state, $stateParams, $http, SystemService, uiGmapGoogleMapApi, toaster) {
    navigator.geolocation.getCurrentPosition(function (pos) {
        $scope.latitude = pos.coords.latitude;
        $scope.longitude = pos.coords.longitude;

        $scope.$apply(function () {
            $scope.map = {
                center: {
                    latitude: $scope.latitude,
                    longitude: $scope.longitude
                },
                zoom: 18,
                options: { draggable: false },
                control: {}
            }

            $scope.marker = {
                id: 0,
                coords: {
                    latitude: $scope.latitude,
                    longitude: $scope.longitude
                },
                options: { draggable: false }
            }
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
        }
        );
})