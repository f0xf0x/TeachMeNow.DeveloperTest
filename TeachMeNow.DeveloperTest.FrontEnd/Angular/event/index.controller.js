(function () {
    'use strict';

    angular
        .module("app")
        .controller('Event.IndexController', Controller);


    function Controller($stateParams, classService, userService, $location, $localStorage) {
        /* jshint validthis:true */
        var vm = this;

        vm.activate = activate;
        vm.createEvent = createEvent;
        vm.viewEvent = viewEvent;
        vm.updateEvent = updateEvent;
        vm.deleteEvent = deleteEvent;
        vm.partners = [];
        vm.isTutor = $localStorage.currentUser.userIsTutor;
        vm.partnerLabel = vm.isTutor ? "Choose student" : "Choose tutor";
        vm.loading = false;
        if ($stateParams.classid) {
            vm.classId = $stateParams.classid;
        }

        function activate() {
            vm.partners.slice(0);
            userService.getPartners().then(function (response) {
                var partners = response.data;
                var len = partners.length;
                for (var i = 0; i < len; i++) {
                    vm.partners.push(partners[i]);
                }
            });
            viewEvent();
        }

        function createEvent() {
            vm.loading = true;
            var partner = vm.selectedPartner.Id;
            var event;
            if (vm.isTutor) {
                event = { subject: vm.subject, StudentId: partner, startTime: vm.startTime, endTime: vm.endTime };
            } else {
                event = { subject: vm.subject, TutorId: partner, startTime: vm.startTime, endTime: vm.endTime };
            }

            classService.createClass(event).then(function (response) {
                vm.loading = false;
                $location.path("/class/"+response.data.Id);
            }, function errorCallback(response) {
                vm.loading = false;
                vm.error = "Operation was cancelled\n";
                vm.error += "HTTP " + response.status + "\n" + response.data;
            });
        }
        function viewEvent() {
            if (!vm.classId) {
                return;
            }

            vm.loading = true;

            classService.getClass(vm.classId).then(function (response) {
                vm.loading = false;
                vm.class = response.data;
            }, function errorCallback(response) {
                vm.loading = false;
                vm.error = "Operation was cancelled\n";
                vm.error += "HTTP " + response.status + "\n" + response.data;
            });
        }
        function updateEvent() {
            if (!vm.classId) {
                return;
            }
            vm.loading = true;
            var partner = vm.selectedPartner.Id;
            var event;
            if (vm.isTutor) {
                event = {Id:vm.classId, subject: vm.subject, StudentId: partner, startTime: vm.startTime, endTime: vm.endTime };
            } else {
                event = {Id:vm.classId, subject: vm.subject, TutorId: partner, startTime: vm.startTime, endTime: vm.endTime };
            }

            classService.updateClass(event).then(function () {
                vm.loading = false;
                $location.path("/class/"+vm.classId);
            }, function errorCallback(response) {
                vm.loading = false;
                vm.error = "Operation was cancelled\n";
                vm.error += "HTTP " + response.status + "\n" + response.data;
            });
        }
        function deleteEvent() {
            if (!vm.classId) {
                return;
            }
            vm.loading = true;
            classService.deleteClass(vm.classId).then(function () {
                vm.loading = false;
                $location.path("/home");
            }, function errorCallback(response) {
                vm.loading = false;
                vm.error = "Operation was cancelled\n";
                vm.error += "HTTP " + response.status + "\n" + response.data;
            });
        }
    }
})();
