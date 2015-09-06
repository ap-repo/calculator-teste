appontoWeb.controller('UserRestrictionController', function ($scope, $state, $stateParams, $http, UserService, SystemService, uiGmapGoogleMapApi, toaster) {
    var userId = $stateParams.userId;
    
    $scope.latitude = -15.826691;
    $scope.longitude = -47.9218204;

    $http({
        url: SystemService.apiUrl + 'api/user?userId=' + userId,
        method: 'GET'
    }).success(function (data) {
        $scope.limitation = data.Configuration.ConfigurationLimitation;
        $scope.employee = data;

        if ($scope.limitation.Latitude !== undefined)
            $scope.latitude = $scope.limitation.Latitude;

        if ($scope.limitation.Longitude !== undefined)
            $scope.longitude = $scope.limitation.Longitude;

        $scope.initialize();
    });

    $scope.saveLimitationMap = function () {
        $http({
            url: SystemService.apiUrl + 'api/user/' + userId + '/limitation',
            method: 'PUT',
            data: $scope.limitation
        }).success(function (data) {
            toaster.pop('success', 'Restrição', data);
            UserService.updateUserFromDatabase();
        });
    }

    $scope.setLimitationType = function (limitationTypeId) {
        $scope.limitation.LimitationType.Id = limitationTypeId;
    }

    $scope.initialize = function () {
        $scope.map = {
            center: {
                latitude: $scope.latitude,
                longitude: $scope.longitude
            },
            zoom: 18,
            options: { draggable: false }
        }

        $scope.marker = {
            id: 0,
            coords: {
                latitude: $scope.latitude,
                longitude: $scope.longitude
            },
            options: { draggable: false }
        }
    }

    $scope.initialize();

    angular.extend($scope, {
        map: {
            center: {
                latitude: $scope.latitude,
                longitude: $scope.longitude
            },
            zoom: 18,
            options: { draggable: false }
        },
        marker: {
            id: 0,
            coords: {
                latitude: $scope.latitude,
                longitude: $scope.longitude
            },
            options: { draggable: false }
        },
        searchbox: {
            template: 'searchbox.tpl.html',
            position: 'top-left',
            options: {
                bounds: {}
            },
            parentdiv: 'searchBoxParent',
            events: {
                places_changed: function (searchBox) {

                    var place = searchBox.getPlaces();
                    if (!place || place == 'undefined' || place.length == 0) {
                        console.log('no place data :(');
                        return;
                    }

                    $scope.limitation.Latitude = place[0].geometry.location.lat();
                    $scope.limitation.Longitude = place[0].geometry.location.lng();

                    $scope.map = {
                        center: {
                            latitude: place[0].geometry.location.lat(),
                            longitude: place[0].geometry.location.lng()
                        },
                        zoom: 18,
                        options: { draggable: false }
                    };

                    $scope.marker = {
                        id: 0,
                        coords: {
                            latitude: place[0].geometry.location.lat(),
                            longitude: place[0].geometry.location.lng()
                        },
                        options: { draggable: false }
                    };
                }
            }
        }
    });
})