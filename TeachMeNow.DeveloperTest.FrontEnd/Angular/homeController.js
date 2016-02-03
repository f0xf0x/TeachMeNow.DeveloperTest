(function () {
    'use strict';

    angular
        .module('tmnApp')
        .controller('homeController', controller);

    controller.$inject = ['userService']; 

    function controller(userService) {
        /* jshint validthis:true */
        var vm = this;
        vm.users = {};

        activate();

        function activate() {
           userService.getUsers().then(function (response) {
               vm.users = response.data;
            }, function (reason) {
                alert('Failed: ' + reason);
            });;
        }
    }
})();
