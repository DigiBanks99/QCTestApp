angular.module('app', ['ngRoute', 'ngSanitize'])
    .config(function ($routeProvider, $httpProvider, $locationProvider, $sceProvider) {

      $routeProvider.when('/',
        {
          templateUrl: '/Content/app/home.html',
          controller: HomeCtrl
        }
      ).when('/items',
        {
          templateUrl: '/Content/app/items.html',
          controller: ItemCtrl
        }
      ).when('/item/:id',
        {
          templateUrl: '/Content/app/item.html',
          controller: ItemCtrl,
          resolve: function () {
            console.log('/item/:id has been hit');
          }
        });

      // configure html5 to get links working on jsfiddle
      $locationProvider.html5Mode(true);
    })
    .run(function ($rootScope) {
    });

function loadXMLDoc() {
  //var xmlhttp;
  //if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
  //  xmlhttp = new XMLHttpRequest();
  //}
  //else {// code for IE6, IE5
  //  xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
  //}
  //xmlhttp.onreadystatechange = function () {
  //  if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
  //    document.getElementById("myDiv").innerHTML = xmlhttp.responseText;
  //  }
  //}
  //xmlhttp.open("GET", "ajax_info.txt", true);
  //xmlhttp.send();
  document.getElementById("myDiv").innerHTML = xmlhttp.responseText;
}