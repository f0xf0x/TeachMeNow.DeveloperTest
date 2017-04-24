(function () {
    'use strict';

    angular
        .module("app")
        .controller('Event.IndexController', Controller);


    function Controller($stateParams, classService, userService, $location, $localStorage) {
        /* jshint validthis:true */
        var vm = this;

        vm.activate = activate;
        vm.edit = edit;

        /* CRUD */
        vm.createEvent = createEvent;
        vm.viewEvent = viewEvent;
        vm.updateEvent = updateEvent;
        vm.deleteEvent = deleteEvent;

        /* Properies */
        vm.subject = "";
        vm.selectedPartner = 0;
        vm.startTime = "";
        vm.endTime = "";

        vm.partners = [];
        vm.isTutor = $localStorage.currentUser.userIsTutor;
        vm.userId = $localStorage.currentUser.userId;
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
        function edit() {
            $location.path("/edit/" + vm.classId);
        }

        function createEvent() {
            vm.loading = true;
            vm.isTutor = $localStorage.currentUser.userIsTutor;
            vm.userId = $localStorage.currentUser.userId;
            var partner = vm.selectedPartner.Id;
            var event;
            if (vm.isTutor) {
                event = { subject: vm.subject, StudentId: partner, TutorId:vm.userId, startTime: vm.startTime, endTime: vm.endTime };
            } else {
                event = { subject: vm.subject, StudentId:vm.userId, TutorId: partner, startTime: vm.startTime, endTime: vm.endTime };
            }

            classService.createClass(event).then(function (response) {
                vm.loading = false;
                $location.path("/class/"+response.data.Id);
            }, function errorCallback(response) {
                vm.loading = false;
                vm.error = "Operation was cancelled\n";
                vm.error += "HTTP " + response.status + "\n" + JSON.stringify(response.data);
            });
        }
        function viewEvent() {
            if (!vm.classId) {
                return;
            }

            vm.loading = true;

            classService.getClass(vm.classId).then(function (response) {
                vm.class = response.data;
                vm.subject = vm.class.Subject;
                var selectedPartner;
                if (vm.isTutor) {
                    selectedPartner = vm.class.StudentId;
                } else {
                    selectedPartner = vm.class.TutorId;
                }
                var len = vm.partners.length;
                for (var i = 0; i < len; i++) {
                    if (vm.partners[i].Id === selectedPartner) {
                        vm.selectedPartner = vm.partners[i];
                    }
                }
                vm.startTime = vm.class.StartTime;
                vm.endTime = vm.class.EndTime;

                vm.loading = false;

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
