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
                                    
        vm.drones = [];


        getWeather();
        getNearDrones();
        getMyDrone();
        
        $interval(getWeather, 10000 * 60);
        $interval(getMyDrone, 1000);
        
        function getWeather() {
            vm.weatherStatus = DroneData.getPlaceStatus({ lat: '41.9973', lng: '21.4280' });
        }

        function getNearDrones() {

            vm.nearbyDrones = DroneData.getNearbyDrones({ lat: '41.9969602', lng: '21.4211897' });

            vm.nearbyDrones.$promise.then(function (data) {
                var i = 2; console.log(data);
                angular.forEach(data, function (val, key) {

                    console.log(val);
                    var drone = { id: i, latitude: val.Coordinate.Latitude, longitude: val.Coordinate.Longitude };
                    vm.drones.push(drone);
                    i++;
                });

            });
        }

        function getMyDrone() {
            var myDrone = DroneData.getMyDrone({ DeviceToken: "Drone1" });

            myDrone.$promise.then(function () {
                console.log("my drone",vm.marker.coords);
                
                vm.marker.coords.latitude = myDrone.Coordinate.Latitude;
                vm.marker.coords.longitude = myDrone.Coordinate.Longitude;
            });

        }



    }
})()