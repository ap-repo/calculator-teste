﻿appontoWeb.controller('ReportController', function ($scope, $state, $http, $stateParams, UserService, SystemService) {
    var userId = $stateParams.userId;

    $scope.today = function () {
        $scope.startDate = new Date();
        $scope.endDate = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        $scope.startDate = null;
        $scope.endDate = null;
    };

    // Disable weekend selection
    $scope.disabled = function (date, mode) {
        return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
    };

    $scope.open = function ($event, item) {
        $event.preventDefault();
        $event.stopPropagation();

        if (item == 1) {
            $scope.openedStartDate = true;
            $scope.openedEndDate = false;
        }
        else {
            $scope.openedEndDate = true;
            $scope.openedStartDate = false;
        }
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 2);
    $scope.events =
      [
        {
            date: tomorrow,
            status: 'full'
        },
        {
            date: afterTomorrow,
            status: 'partially'
        }
      ];

    $scope.getDayClass = function (date, mode) {
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
    };

    $scope.getReport = function () {
        var userSearch = (userId !== undefined) ? userId : UserService.getUser().Id;
        $http({
            url: SystemService.apiUrl + 'api/report/' + userSearch + '/between/?startDate=' + moment($scope.startDate).format('YYYY/MM/DD') + '&endDate=' + moment($scope.endDate).format('YYYY/MM/DD'),
            method: 'GET'
        }).success(function (data) {
            $scope.records = [];
            $scope.totalWorked = 0;

            $.each(data, function (i, day) {
                $.each(day.List, function (i, register) {
                    register.DateFormatedHourMinute = moment(register.DateMilliseconds).format('HH:mm:ss');
                });

                $scope.totalWorked += day.Worked;

                day.Worked = moment.utc(day.Worked).format('HH:mm:ss');
            });

            $scope.totalWorked = moment.utc($scope.totalWorked).format('HH:mm:ss');

            $scope.records = data;
        });
    }

})