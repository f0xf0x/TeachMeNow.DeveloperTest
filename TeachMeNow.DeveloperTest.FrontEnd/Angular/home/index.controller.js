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
        vm.activate = activate;
        vm.resize = resize;

        $scope.uiConfig = {
            calendar: {
                height: 500,
                editable: true,
                aspectRatio: 2,
                header: {
                    left: 'title',
                    center: '',
                    right: 'today month,agendaWeek prev,next'
                },
                dayClick: $scope.setCalDate,
                background: '#f26522',
                eventDrop: vm.resize,
                eventResize: vm.resize
            }
        };


        function activate() {
            classService.getClasses().then(function (response) {
                vm.classes = response.data;
                /* config object */
                uiCalendarConfig.calendars['classesCalendar'].fullCalendar('removeEventSources');
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
                uiCalendarConfig.calendars['classesCalendar'].fullCalendar('addEventSource', vm.events);

                //uiCalendarConfig.calendars.classesCalendar.fullCalendar('renderEvents');


            }, function errorCallback(response) {
                vm.error = "Operation was cancelled\n";
                vm.error += "HTTP " + response.status + "\n" + response.data;
                alert(vm.error);
            });
        }

        function resize(event, delta, revertFunc, jsEvent, ui, view) {
            var classModel = {
                Id: event.id,
                Subject: event.title,
                StartTime: event.start,
                EndTime: event.end
            };
            classService.updateClass(classModel).then(function (response) {
                if (!response) {
                    revertFunc();
                    return;
                }
                activate();
            }, function (reason) {
                alert('Failed: ' + reason);
            });

        }
    }
})();
