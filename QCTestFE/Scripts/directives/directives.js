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
    replace: true,
    scope: {
      wishls: '='
    },
    template: '<div class="wishlist-text">' +
                '<ul>' +
                  '<li ng-repeat="wishlist in wishls">' +
                    '<button type="button" class="wishlist-text" ng-click="addToWishlist(wishlist)">{{wishlist.WishlistName}}</button>' +
                  '</li>' +
                '</ul>' +
              '</div>',
    link: function (scope, element, attrs) {
      scope = $scope;
    }
  };
});

angular.module('app').directive('ngItemLine', function () {
  return {
    restrict: 'A',
    replace: false,
    scope: {
      iswl: '=',
      orders: '=',
      items: '='
    },
    template: '<tr ng-repeat="order in orders">' +
                '<td class="grid-number">{{order.OrderID}}</td>' + //testline 
                '<td class="grid-checkbox"><input type="checkbox" ng-model="order.selected" /></td>' +
                '<td class="grid-image"><a href="/item/{{order.ItemID}}"><img src="/Content/images/tent{{order.ItemID}}.jpg" /></a></td>' +
                '<td class="grid-name"><a href="/item/{{order.ItemID}}">{{getItem(order.ItemID, items).ItemName}}</a></td>' +
                '<td class="grid-price">R {{getItem(order.ItemID, items).Price}}</td>' +
                '<td ng-hide="{{iswl}}" class="grid-quantity"><input type="text" ng-model="order.Quantity" ng-change="quantityChanged(order.OrderID, order.Quantity)" /></td>' +
                '<td ng-hide="{{iswl}}" class="grid-price">R {{order.Total}}</td>' +
              '</tr>',
    controller: ['$scope', '$http', function ($scope, $http) {
      $scope.quantityChanged = function (orderID, quantity) {
        var dataArray = new Array();
        if (orderID == null || quantity == null) {
          console.log("DEV Error: JS - orderID or quantity is null in CartCtrl");
          return;
        }
        dataArray[0] = orderID;
        dataArray[1] = quantity;
        $http.post('/api/order', dataArray).then(function (response) {
          $scope.Message = response.data.Message;
          for (var i = 0; i < $scope.orders.length; i++) {
            if ($scope.orders[i].OrderID == response.data.Order.OrderID)
              $scope.orders[i] = response.data.Order;
          }
        });
      }
    }],
    link: function (scope, iElement, iAttrs, ctrl) {
      scope.getItem = function (itemID, items) {
        for (var i = 0; i < items.length; i++) {
          if (items[i].ItemID == itemID)
            return items[i];
        }
      };
    }
  }
});

angular.module('app').directive('ngCartitemHeaders', function () {
  return {
    restrict: 'A',
    replace: false,
    template: '<tr>' +
                '<th class="grid-number">#</th>' +
                '<th class="grid-checkbox"></th>' +
                '<th class="grid-image"></th>' +
                '<th class="grid-name">Name</th>' +
                '<th class="grid-price">Price</th>' +
                '<th class="grid-quantity">Quantity</th>' +
                '<th class="grid-price">Total</th>' +
              '</tr>'
  }
});

angular.module('app').directive('ngCartitemTotal', function () {
  return {
    restrict: 'A',
    replace: false,
    scope: {
      ngTotal: '='
    },
    template: '<tr>' +
                '<td class="grid-total-space"></td>' +
                '<td class="grid-total-desc">Cart Total</td>' +
                '<td class="grid-total">R {{ngTotal}}</td>' +
              '</tr>'
  }
});