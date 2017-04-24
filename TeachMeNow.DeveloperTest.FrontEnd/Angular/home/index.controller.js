(function () {
    'use strict';

    angular
        .module("app")
        .controller('Home.IndexController', Controller);


    function Controller($scope, $location, uiCalendarConfig, classService) {
        /* jshint validthis:true */
        var vm = this;

        vm.events = [];
        vm.activate = activate;
        vm.resize = resize;
        vm.eventClick = eventClick;
        vm.loading = true;

        $scope.uiConfig = {
            calendar: {
                events: vm.events,
                height: 500,
                editable: true,
                aspectRatio: 2,
                header: {
                    left: 'title',
                    center: '',
                    right: 'today month,agendaWeek,listMonth prev,next'
                },
                dayClick: $scope.setCalDate,
                background: '#f26522',
                eventClick: vm.eventClick,
                eventDrop: vm.resize,
                eventResize: vm.resize
            }
        };


        function activate() {
            vm.loading = true;
            classService.getClasses().then(function (response) {
                vm.loading = false;
                /* config object */
                var len = response.data.length;
                for (var i = 0; i < len; i++) {
                    var item = response.data[i];
                    vm.events.push({
                        id: item.Id,
                        title: item.Subject,
                        start: new Date(item.StartTime),
                        end: new Date(item.EndTime)
                    });
                }

            }, function errorCallback(response) {
                vm.loading = false;
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
            vm.loading = true;
            classService.updateClass(classModel).then(function (response) {
                vm.loading = false;
                if (!response) {
                    revertFunc();
                    return;
                }
                //activate();
            }, function (reason) {
                vm.loading = false;
                alert('Failed: ' + reason);
            });
        }

        function eventClick(event, jsEvent, view) {
            vm.loading = true;
            $location.path("/class/" + event.id);
        }
    }
})();
