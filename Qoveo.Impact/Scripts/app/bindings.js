// KnockOut Binding Handler for a file upload
ko.bindingHandlers.fileUpload = {
    init: function (elem, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        $(elem).fileupload({
            dataType: 'json',
            add: function (e, data) {
                impactApp.presenter.toggleActivity(true);
                data.submit();
            },

            done: function (e, data) {
                $.each(data.result, function (index, file) {
                    var value = valueAccessor(),
                        valueUnwrapped = ko.utils.unwrapObservable(value);
                    if (ko.isWriteableObservable(value)) {
                        value(file);
                    }
                    impactApp.presenter.toggleActivity(false);
                })
            }
        });
    },

    update: function (elem, valueAccessor) {

    }
};

// KnockOut Binding Handler for a bootstrap popOver 
ko.bindingHandlers.popOver = {
    init: function (elem, valueAccessor, allBindingsAccessor) {
        
    },

    update: function (elem, valueAccessor, allBindingsAccessor) {
        // First get the latest data that we're bound to
        var value = valueAccessor(),
            allBindings = allBindingsAccessor();

        // Next, whether or not the supplied model property is observable, get its current value
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        // Grab more data from the title and content properties
        var title = ko.utils.unwrapObservable(allBindings.title),
            content = ko.utils.unwrapObservable(allBindings.content);

        // Initialize the popover with the correct informations
        $(elem).popover({
            placement: valueUnwrapped,
            title: title,
            content: content,
            animation: true
        });
    }
};

// KnockOut Binding Hanbler for a google table chart
// Load the Visualization API and the table package.
google.load('visualization', '1.0', { 'packages': ['table'] });
ko.bindingHandlers.table = {
    init: function (elem, valueAccessor, allBindingsAccessor) {

    },

    update: function (elem, valueAccessor, allBindingsAccessor) {

        var value = valueAccessor(),
            valueUnwrapped = ko.utils.unwrapObservable(value);
        
        // Instantiate and draw our chart, passing in some options.
        var visualization = new google.visualization.Table(elem);
        visualization.draw(valueUnwrapped, {
            allowHtml: true,
            page: 'enable',
            pageSize: 30,
            sort: 'enable',
            showRowNumber: true
        });
    }
};

// Knockout Binding Handler for a google gauge chart
// Load the Visualization API and the gauge package.
google.load('visualization', '1.0', { 'packages': ['gauge'] });
ko.bindingHandlers.gauge = {
    init: function (elem, valueAccessor, allBindingsAccessor) {

    },

    update: function (elem, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor(),
            valueUnwrapped = ko.utils.unwrapObservable(value),
            allBindings = allBindingsAccessor(),
            options = allBindings.gaugeOptions;


        var visualization = new google.visualization.Gauge(elem);
        visualization.draw(valueUnwrapped, options);
    }
};

// KnockOut Binding Handler for bootstrap-datetimepicker
ko.bindingHandlers.datetimepicker = {
    init: function (elem, valueAccesor, allBindingsAccessor, viewModel, bindingContext) {
        // Initialize the datetimepicker
        $(elem).datetimepicker({
            language: 'en'
        });

        // Register changeDateEvent
        ko.utils.registerEventHandler(elem, 'changeDate', function (event) {
            var accessor = valueAccesor();
            var all = allBindingsAccessor();
            if (ko.isObservable(accessor)) {
                var value = moment(event.localDate);
                accessor(value);
            }
        });
    },

    update: function (elem, valueAccesor, allBindingsAccessor) {
        var value = valueAccesor(),
            valueUnwrapped = ko.utils.unwrapObservable(value),
            // The date from the server are UTC
            mnt = moment.utc(valueUnwrapped);

        if (mnt) {
            // Display the time in local time
            mnt.local();
            $(elem).data('datetimepicker').setLocalDate(new Date(mnt.year(), mnt.month(), mnt.date(), mnt.hour(), mnt.minute()));
        }
    }
};