(function () {
    'use strict';

    angular
        .module("app")
        .controller('Home.IndexController', Controller);


    function Controller(classService) {
        /* jshint validthis:true */
        var vm = this;
        vm.classes = {};

        vm.activate = activate;

        function activate() {
            classService.getClasses().then(function (response) {
                vm.classes = response.data;
            }, function (reason) {
                alert('Failed: ' + reason);
            });;
        }

        activate();
    }
})();
