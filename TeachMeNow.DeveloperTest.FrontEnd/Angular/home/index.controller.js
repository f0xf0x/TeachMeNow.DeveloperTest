(function () {
    'use strict';

    angular
        .module("app")
        .controller('Home.IndexController', Controller);


    function Controller($scope, uiCalendarConfig, classService) {
        /* jshint validthis:true */
        var vm = this;
        vm.classes = {};

        vm.events = [];
        $scope.uiConfig = {
            calendar: {
                height: 500,
                editable: false,
                aspectRatio: 2,
                header: {
                    left: 'title',
                    center: '',
                    right: 'today prev,next'
                },
                dayClick: $scope.setCalDate,
                background: '#f26522'
            }
        };

        vm.activate = activate;

        function activate() {
            classService.getClasses().then(function (response) {
                vm.classes = response.data;
                /* config object */
                vm.events.slice(0);
                var len = vm.classes.length;
                for (var i = 0; i < len; i++) {
                    var item = vm.classes[i];
                    vm.events.push({
                        id: item.Id,
                        title: item.Subject,
                        start: new Date(item.StartTime),
                        end: new Date(item.EndTime)
                    });
                }
                uiCalendarConfig.calendars['classesCalendar'].fullCalendar('removeEventSources');
                uiCalendarConfig.calendars['classesCalendar'].fullCalendar('addEventSource', vm.events);

                //uiCalendarConfig.calendars.classesCalendar.fullCalendar('renderEvents');


            }, function (reason) {
                alert('Failed: ' + reason);
            });;
        }
    }
})();
