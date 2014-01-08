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
          controller: ItemCtrl
        }
      ).when('/wishlists',
        {
          templateUrl: '/Content/app/wishlistlist.html',
          controller: WishlistListCtrl
        }
      ).when('/wishlist/:id',
        {
          templateUrl: '/Content/app/wishlist.html',
          controller: WishlistCtrl
        }
      ).when('/cart',
        {
          templateUrl: '/Content/app/cart.html',
          controller: CartCtrl
        }
      ).when('/newwishlist',
        {
          templateUrl: '/Content/app/newwishlist.html',
          controller: WishlistListCtrl
        }
      ).when('/items/:cat',
        {
          templateUrl: '/Content/app/items.html',
          controller: ItemCategoryCtrl
        }
      );

      // configure html5 to get links working on jsfiddle
      $locationProvider.html5Mode(true);
    })
    .run(function ($rootScope) {
    });