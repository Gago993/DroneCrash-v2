(function () {
    'use strict'

    angular
        .module("app.main")
        .controller("MainController", MainController);

    MainController.$inject = ['$interval', 'DroneData'];

    function MainController($interval, DroneData) {
        var vm = this;
        vm.test = "test";

        vm.weatherStatus = {};

        vm.map = { center: { latitude: 41.9973, longitude: 21.4280 }, zoom: 8 };

        vm.marker = { id: 1, coords: { latitude: 50, longitude: -50 }, options: { icon: "/Content/Images/Default/drone.png" } };
                                    

        getWeather();

        
        $interval(getWeather, 10000 * 60);
        
        function getWeather() {
            vm.weatherStatus = DroneData.getPlaceStatus({ lat: '41.9973', lon: '21.4280' });

            vm.weatherStatus.$promise.then(function () {
                vm.marker.coords.latitude = vm.weatherStatus.Coordinate.Latitude;
                vm.marker.coords.longitude = vm.weatherStatus.Coordinate.Longitude;
            });
            
        }
    }
})()