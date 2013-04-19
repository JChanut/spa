$(function () {
    var getValidationSummaryErrors = function ($form) {
        var errorSummary = $form.find('.validation-summary-errors, .validation-summary-valid');
        return errorSummary;
    };

    var displayErrors = function (form, errors) {
        var errorSummary = getValidationSummaryErrors(form)
            .removeClass('validation-summary-valid')
            .addClass('validation-summary-errors');

        var items = $.map(errors, function (error) {
            return '<li>' + error + '</li>';
        }).join('');

        var ul = errorSummary
            .find('ul')
            .empty()
            .append(items);
    };

    var formSubmitHandler = function (e) {
        var $form = $(this);

        // We check if jQuery.validator exists on the form
        if (!$form.valid || $form.valid()) {
            $('.btn').button('loading');
            $.post($form.attr('action'), $form.serializeArray())
                .done(function (json) {
                    json = json || {};

                    // In case of success, we redirect to the provided URL or the same page.
                    if (json.success) {
                        location = json.redirect || location.href;
                    } else if (json.errors) {
                        displayErrors($form, json.errors);
                        $('.btn').button('reset');
                    }
                })
                .error(function () {
                    displayErrors($form, ['An unknown error happened.']);
                    $('.btn').button('reset');
                })
        }

        // Prevent the normal behavior since we opened the dialog
        e.preventDefault();
    };
    
    $("#loginForm").submit(formSubmitHandler);
    $("#registerForm").submit(formSubmitHandler);
    $('#teamLoginForm').submit(formSubmitHandler)

    // Team Login by defaults
    var $teamLogin = $('#team-login');
    $teamLogin.addClass('login-active');
    $teamLogin.show();
    
});

// Function to switch login between admin and team
var switchLogin = function (login) {
    var $activeLogin = $('.login-active');
    $activeLogin.hide();
    $activeLogin.removeClass('login-active')

    var $newLogin = $('#' + login + '-login');
    $newLogin.addClass('login-active');
    $newLogin.show();
};