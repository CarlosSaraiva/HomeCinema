(function (app) {
    'user strict';
    app.controller('rootCtrl', rootCtrl);

    function rootCtrl($scope) {
        $scope.UserData = {};
        $scope.UserData.displayUserInfo = displayUserInfo;
        $scope.logout = logout;
    }

    function displayUserInfo() {
    }

    function logout() {
    }

    $scope.UserData.displayUserInfo();
})(angular.module('homeCinema'));