﻿var ObjectState = {
    Unchanged: 0,
    Added: 1,
    Modified: 2,
    Deleted: 3
};

var salesOrderItemMapping = {
    'SalesOrderItems': {
        key: function (salesOrderItem) {
            return ko.utils.unwrapObservable(salesOrderItem.SalesOrderItemId);
        },
        create: function (options) {
            return new SalesOrderItemViewModel(options.data);
        }
    }
};

//Client side view models
SalesOrderItemViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, salesOrderItemMapping, self);

    self.flagSalesOrderItemAsEdited = function () {
        if (self.ObjectState() !== ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }

        return true;
    },

        self.ExtendedPrice = ko.computed(function () {
            return (self.Quantity() * self.UnitPrice()).toFixed(2);
        });
};


SalesOrderViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, salesOrderItemMapping, self);

    self.save = function () {
        //Client side ViewModel is sent to a Controller Action
        $.ajax({
            url: "/Sales/Save/",
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                debugger;
                //Client side: Server side model is mapped to Client side viewModel - and view is update by ko-binding
                if (data.salesOrderViewModel != null)
                    ko.mapping.fromJS(data.salesOrderViewModel, {}, self);

                //If deleted
                if (data.newLocation != null)
                    window.location = data.newLocation;
            }
        });
    },

        self.flagSalesOrderAsEdited = function () {
            if (self.ObjectState() != ObjectState.Added) {
                //debugger;
                //alert("flagged");
                self.ObjectState(ObjectState.Modified);
            }
            return true;
        },

        self.addSalesOrderItem = function () {
            //Needs to add the state 'ObjectState.Added' to the item
            var salesOrderItem = new SalesOrderItemViewModel({ SalesOrderItemId: 0, ProductCode: "", Quantity: 1, UnitPrice: 0, ObjectState: ObjectState.Added });
            self.SalesOrderItems.push(salesOrderItem);
        },

        self.Total = ko.computed(function () {
            var total = 0;
            ko.utils.arrayForEach(self.SalesOrderItems(), function (salesOrderItem) {
                total += parseFloat(salesOrderItem.ExtendedPrice());
            });
            return total.toFixed(2);
        }),

        self.deleteSalesOrderItem = function (salesOrderItem) {
            self.SalesOrderItems.remove(this);

            if (salesOrderItem.SalesOrderItemId() > 0 && self.SalesOrderItemsToDelete.indexOf(salesOrderItem.SalesOrderItemId()) === -1)
                self.SalesOrderItemsToDelete.push(salesOrderItem.SalesOrderItemId());
        };
};