
//        var app = angular.module('MyApp', []);
//app.controller('MyController', function ($scope, $http) {
//    //  $scope.ButtonClick = function () {
//    $http({
//        url: "/Dictionary/ListWordExampleFals",
//        method: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json"

//        //data: '{"name": "' + 'Farhad'  + '"}',
//        // headers: { "Content-Type": "application/json" }
//    }).then(function mySuccess(data) {
//        $scope.names = data.data;
//    }, function myError (error) {
//        alert(error.data.Message);
//    });
//    // }
//});

//var app = angular.module('MyApp2', []);


//var app = angular.module('MyApp', []);
app.controller('MyController2', function ($scope) {
    $scope.SaveWord = function () {
        var xxx = $scope.DicEng
    };
});
//--------Dictionary
app.controller('MyController', function ($scope, $http) {
    $scope.SaveWord = function () {

        var xxx = $scope.DicEng
    };
 
    $scope.showMe = false;
    $scope.myFunc = function () {
        $scope.showMe = !$scope.showMe;
    };
    $scope.Showper = function (x) {
        $scope['p'+x] = true;

    }
    $scope.Showeng = function (x) {
        $scope['e'+x] = true;

    }
    $scope.ShowModal = function () {
        var urll = "/Dictionary/CreateWordAngular";
        $http({
            method: "Get",
            contentType: "application/json;charset=utf-8",
            dataType: "html",
            url: urll
        }).then(function mySuccess(data) {
            $(".BodyModal").html(data.data);
            //$(".FooterModal").html(
            //    "<button type='button' class='btn btn-danger' data-dismiss='modal'>بستن</button><span> </span><button ng-click='SaveWord()' type='button' class='btn btn-primary btnSave' data-dismiss='modal'>ثبت</button>"
            //    );
                $("#ModalAngular").modal();
            }
        )

    }
    //  $scope.ButtonClick = function () {
    $http({
        url: "/Dictionary/ListWordExampleAngular",
        method: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json"

        //data: '{"name": "' + 'Farhad'  + '"}',
        // headers: { "Content-Type": "application/json" }
    }).then(function mySuccess(data) {
        $scope.names = data.data;
        $scope.orderByMe = function (x) {
            $scope.myOrderBy = x;
           
        }
       // $("#ListWordExampleFalse").html(data)
    }, function myError(error) {
        console.log(error.data.Message);
    });
    // }
});
//var appAng = angular.module('angularApp', []);
appAng.controller('TaskControllerr', function ($scope, $http) {
    $scope.TaskToday = function () {
        $http({
            url: "/Task/TaskToday",
            method: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json"
        }).then(function mySuccess(data) {
            var r = data.data;
            ListTask("anjamnashode");
            ListTiming();
            //$scope.orderByMe = function (x) {
            //    $scope.myOrderBy = x;

            //}
            // $("#ListWordExampleFalse").html(data)
        }, function myError(error) {
            console.log(error.data.Message);
        });

    }
});
//----------
appAng.controller('customersCtrl2', function ($scope, $http) {
    $http({
        url: "/PersonnelGsystem/ListTaradod_angular",
        method: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json"
    }).then(function mySuccess(data) {
        $scope.names = data.data;
        $scope.orderByMe = function (x) {
            $scope.myOrderBy = x;
        }
    }, function myError(error) {
        console.log(error.data.Message);
    });
});


