appontoWeb.controller('UserAccessLevelController', function ($scope, $state, $stateParams, $http, UserService, SystemService, toaster) {
    var userId = $stateParams.userId;

    $scope.userAccessLevels = [];
    $scope.accessLevels = [];

    function getAccessLevel() {
        $http({
            url: SystemService.apiUrl + 'api/accessLevel',
            method: 'GET'
        }).success(function (data) {
            $scope.accessLevels = data;

            $http({
                url: SystemService.apiUrl + 'api/user/' + userId + '/accesslevel',
                method: 'GET'
            }).success(function (data) {
                $.each(data, function () {
                    $scope.userAccessLevels.push(this.Id);
                });
            });            
        });
    }

    $scope.save = function () {
        var accessLevels = [];

        $.each($scope.userAccessLevels, function (index, item) {
            $.each($scope.accessLevels, function () {
                if (this.Id == item)
                    accessLevels.push(this);
            });
        });

        $http({
            url: SystemService.apiUrl + 'api/user/'+ userId +'/accessLevel',
            method: 'PUT',
            data: accessLevels
        }).success(function (data) {
            toaster.pop('success', 'Permissão', data);
        });
    }

    getAccessLevel();
})