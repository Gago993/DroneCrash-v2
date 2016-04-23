(function () {
    angular.module("app.data")
        .factory("DroneData", DroneData);

    DroneData.$inject = ["$resource"];

    function DroneData($resouce) {
        return $resouce("/api/Drones/:id", {}, {
            'getPlaceStatus': {
                method: 'GET',
                url: "/api/GetStatusInfo/GetDroneInfo",
            },
            'getNearbyDrones': {
                method: 'GET',
                url: "/api/Drones/Nearby",
                isArray: true
            },
            'getMyDrone': {
                method: 'GET',
                url: "/api/Drones/Me",
            }

        });
    }
})()