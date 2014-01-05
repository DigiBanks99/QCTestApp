angular.module('app')
.directive('itemtile', ['$sce', function ($controller, $sce) {
  return {
    restrict: 'AE',
    replace: true,
    template: '<div class="item-tile">' +
                  '<img src="/Content/images/tent{{item.ItemID}}.jpg" />' +
                  '<h3>{{item.ItemName}}</h3>' +
                  '<p class="short-description">{{item.ShortDescription}}</p>' +
                  '<div ng-bind-html="item.Description"></div>' +
                  '<p class="price">R {{item.Price}}</p>' +
                '</div>',
    scope: {
      item: '='
    },
    link: function (scope, element, attrs) {
      scope.getSafeHtml = function (html) {
        return $sce.trustAsHTML(html);
      };
    }
  };
}]);

angular.module('app').directive('wishlistlist', function ($controller) {
  return {
    restrict: 'E',
    replace: false,
    scope: { wishls: '=' },
    //template: 'Content/app/wishlistslist.html'
    template: '<div class="wishlist-text">' +
                '<ul>' +
                  '<li ng-repeat="wishlist in wishls">' +
                    '<button class="wishlist-text" ng-click="addToWishlist(wishlist)">{{wishlist.WishlistName}}</button>' +
                  '</li>' +
                '</ul>' +
              '</div>'
  };
});