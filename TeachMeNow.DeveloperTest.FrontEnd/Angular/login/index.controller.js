(function () {
    'use strict';

    angular
        .module("app")
        .controller('Login.IndexController', Controller);

    function Controller($location, AuthenticationService, accountService) {
        var vm = this;

        vm.login = login;
        vm.register = register;

        initController();

        function initController() {
            // reset login status
            AuthenticationService.Logout();
        };

        function login() {
            vm.loading = true;
            AuthenticationService.Login(vm.email, vm.password, function (result,response) {
                if (result === true) {
                    $location.path('/home');
                } else {
                    vm.error = 'Username or password is incorrect\n'+JSON.stringify(response.data);
                    vm.loading = false;
                }
            });
        };

        function register() {
            vm.loading = true;
            var model = {
                UserName: vm.username,
                Email: vm.email,
                Password: vm.password,
                ConfirmPassword: vm.passwordConfirm,
                IsTutor: vm.isTutor === "true" ? true : false
            }
            accountService.createAccount(model).then(function success(response) {

                AuthenticationService.Login(vm.email, vm.password, function (result) {
                    if (result === true) {
                        $location.path('/home');
                    } else {
                        $location.path('/login');
                        vm.loading = false;
                    }
                });

            }, function errorCallback(response) {
                vm.loading = false;
                    vm.error = "Registration wasn't completed\n"+JSON.stringify(response.data);
            });
        }
    }
})();