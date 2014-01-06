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
        }
        ).when('/wishlists',
        {
          templateUrl: '/Content/app/wishlistlist.html',
          controller: WishlistListCtrl,
          resolve: function () {
            console.log('/wishlists has been hit');
          }
        }
        ).when('/wishlist/:id',
        {
          templateUrl: '/Content/app/wishlistlist.html',
          controller: WishlistListCtrl,
          resolve: function () {
            console.log('/wishlist/:id has been hit');
          }
        });

      // configure html5 to get links working on jsfiddle
      $locationProvider.html5Mode(true);
    })
    .run(function ($rootScope) {
    });