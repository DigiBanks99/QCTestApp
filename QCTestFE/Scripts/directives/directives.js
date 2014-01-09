angular.module('app')
.directive('ngItemtile', ['$sce', function ($controller, $sce) {
  return {
    restrict: 'AE',
    replace: true,
    scope: {
      ngItem: '='
    },
    template: '<div class="item-tile">' +
                  '<img src="/Content/images/{{ngItem.ItemID}}.jpg" />' +
                  '<h3>{{ngItem.ItemName}}</h3>' +
                  '<p class="short-description">{{ngItem.ShortDescription}}</p>' +
                  '<div ng-bind-html="ngItem.Description"></div>' +
                  '<p class="price">R {{ngItem.Price}}</p>' +
                '</div>',
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
      ngIswl: '=',
      ngOrders: '=',
      ngItems: '=',
      ngCheckedList: '=ngCheckedList'
    },
    template: '<tr ng-repeat="order in ngOrders">' +
                '<td class="grid-number">{{order.OrderID}}</td>' + //testline 
                '<td class="grid-checkbox"><input type="checkbox" ng-model="order.selected"  ng-click="tick(order.OrderID)" /></td>' +
                '<td class="grid-image"><a href="/item/{{order.ItemID}}"><img src="/Content/images/{{order.ItemID}}.jpg" /></a></td>' +
                '<td class="grid-name"><a href="/item/{{order.ItemID}}">{{getItem(order.ItemID, ngItems).ItemName}}</a></td>' +
                '<td class="grid-price">R {{getItem(order.ItemID, ngItems).Price}}</td>' +
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
          for (var i = 0; i < $scope.ngOrders.length; i++) {
            if ($scope.ngOrders[i].OrderID == response.data.Object.OrderID)
              $scope.ngOrders[i] = response.data.Object;
          }
        });
      }
    }],
    link: function (scope, iElement, iAttrs, ctrl) {
      scope.ngOrders = new Array();
      for (var i = 0; i < iAttrs.ngOrders.length; i++) {
        if (iAttrs.ngOrders[i].DispatchDate != null) {
          scope.ngOrders[scope.ngOrders.length] = iAttrs.ngOrders[i];
        }
      }

      scope.getItem = function (itemID, items) {
        for (var i = 0; i < items.length; i++) {
          if (items[i].ItemID == itemID)
            return items[i];
        }
      };

      scope.tick = function (itemID) {
        var list = new Array();
        var deleted = false;
        for (var i = 0; i < scope.ngCheckedList.length; i++) {
          if (scope.ngCheckedList[i] == itemID) {
            scope.ngCheckedList[i] = null;
            deleted = true;
            continue;
          }
          list[list.length] = scope.ngCheckedList[i];
        }
        if (deleted) {
          scope.ngCheckedList = list;
          return;
        }
        scope.ngCheckedList[scope.ngCheckedList.length] = itemID;
      };
    }
  }
});

angular.module('app').directive('ngWishlist', function () {
  return {
    restrict: 'A',
    replace: false,
    scope: {
      ngWishlistItems: '=',
      ngItems: '=',
      ngCheckedList: '=ngCheckedList'
    },
    template: '<tr ng-repeat="wlitem in ngWishlistItems">' +
                '<td class="grid-checkbox"><input type="checkbox" ng-model="wlitem.selected" ng-click="tick(wlitem.ItemID)" /></td>' +
                '<td class="grid-image"><a href="/item/{{wlitem.ItemID}}"><img src="/Content/images/{{wlitem.ItemID}}.jpg" /></a></td>' +
                '<td class="grid-name"><a href="/item/{wlitem.ItemID}}">{{getItem(wlitem.ItemID, ngItems).ItemName}}</a></td>' +
                '<td class="grid-price">R {{getItem(wlitem.ItemID, ngItems).Price}}</td>' +
              '</tr>',
    link: function (scope, iElement, iAttrs) {
      scope.getItem = function (itemID, items) {
        for (var i = 0; i < items.length; i++) {
          if (items[i].ItemID == itemID)
            return items[i];
        }
      };

      scope.tick = function (itemID) {
        var list = new Array();
        var deleted = false;
        for (var i = 0; i < scope.ngCheckedList.length; i++) {
          if (scope.ngCheckedList[i] == itemID) {
            scope.ngCheckedList[i] = null;
            deleted = true;
            continue;
          }
          list[list.length] = scope.ngCheckedList[i];
        }
        if (deleted) {
          scope.ngCheckedList = list;
          return;
        }
        scope.ngCheckedList[scope.ngCheckedList.length] = itemID;
      };
    }
  }
});

angular.module('app').directive('ngCartitemHeaders', function () {
  return {
    restrict: 'A',
    replace: false,
    scope: {
      ngIswl: '='
    },
    template: '<tr>' +
                '<th class="grid-number" ng-hide="ngIswl">#</th>' +
                '<th class="grid-checkbox"></th>' +
                '<th class="grid-image"></th>' +
                '<th class="grid-name">Name</th>' +
                '<th class="grid-price">Price</th>' +
                '<th class="grid-quantity" ng-hide="ngIswl">Quantity</th>' +
                '<th class="grid-price" ng-hide="ngIswl">Total</th>' +
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

angular.module('app').directive('ngCategoryListSelector', function () {
  return {
    restrict: 'EA',
    replace: false,
    scope: {
      ngCategories: '=',
      ngCategory: '&'
    },
    template: '<ul id="cat-list">' +
                '<li ng-repeat="cat in ngCategories" ng-click="setCategory(cat)">AS {{cat.CategoryName}}</li>' +
              '</ul>',
    link: function (scope, iElement, iAttrs) {
      scope.setCategory = function (cat) {
        ngCategory = cat;
      };
    }
  }
});