window.impactApp = window.impactApp || {};

window.impactApp.presenter = (function ($) {
    var
        transitionOptions = {
            ease: 'swing',
            fadeOut: 100,
            floatIn: 500,
            offsetLeft: '20px',
            offsetRight: '-20px'
        },

        entranceThemeTransition = function ($view) {
            $view.css({
                display: 'block',
                visibility: 'visible'
            }).addClass('view-active').animate({
                marginRight: 0,
                marginLeft: 0,
                opacity: 1
            }, transitionOptions.floatIn, transitionOptions.ease);
        },

    resetViews = function () {
        $('.view').css({
            marginLeft: transitionOptions.offsetLeft,
            marginRight: transitionOptions.offsetRight,
            opacity: 0
        })
    },

        goToView = function (viewId) {
            $('.view').hide();
            var $activeViews = $('.view-active'),
                $view = $('#' + viewId.toLowerCase() + '-view');

            toggleActivity(true)

            if ($activeViews.length) {
                $activeViews.fadeOut(transitionOptions.fadeOut, function () {
                    resetViews();
                    entranceThemeTransition($view);
                });
                $('.view').removeClass('view-active');
            } else {
                resetViews();
                entranceThemeTransition($view);
            }
            toggleActivity(false)
        },

        toggleActivity = function (show) {
            $('#busyindicator').activity(show);
        };

    return {
        goToView: goToView,
        toggleActivity: toggleActivity
    }

})($);