(function () {
    'use strict';

    angular
        .module("app")
        .factory('AuthenticationService', Service);

    function Service($http, $localStorage, baseUrl) {
        var service = {};

        service.Login = Login;
        service.Logout = Logout;

        return service;

        function Login(username, password, callback) {
            var body = $.param({ username: username, password: password, grant_type: "password" });
            var httpParams = {
                method: "post",
                url: baseUrl + '/token',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: body
            };
            $http(httpParams).then(function successCallback(response) {
                // login successful if there's a token in the response
                var accessToken = response.data.access_token;
                if (accessToken) {
                    var userIsTutor = (response.data.IsTutor == "true");
                    var userId = response.data.UserId;
                    // store username and token in local storage to keep user logged in between page refreshes
                    $localStorage.currentUser = { username: username, token: accessToken, userIsTutor:userIsTutor, userId : userId};

                    // add jwt token to auth header for all requests made by the $http service
                    $http.defaults.headers.common.Authorization = 'Bearer ' + accessToken;

                    // execute callback with true to indicate successful login
                    callback(true);
                } else {
                    // execute callback with false to indicate failed login
                    callback(false,response);
                }
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                callback(false,response);
            });
        }

        function Logout() {
            // remove user from local storage and clear http auth header
            delete $localStorage.currentUser;
            $http.defaults.headers.common.Authorization = '';
        }
    }
})();