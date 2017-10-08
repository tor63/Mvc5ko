var ObjectState = {
    Unchanged: 0,
    Added: 1,
    Modified: 2,
    Deleted: 3
};

SalesOrderViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

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
                debugger;
                //alert("flagged");
                self.ObjectState(ObjectState.Modified);
            }
            return true;
        };
};