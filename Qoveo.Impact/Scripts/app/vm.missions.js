window.impactApp = window.impactApp || {};

// Missions ViewModel
window.impactApp.missionsViewModel = (function (ko, datacontext, config) {

    // ViewModel
    var missions = ko.observableArray();

    var logger = config.logger;

    // Gestion des erreurs
    var error = ko.observable();

    // Operations
    var updateMission = function (mission) {
        impactApp.presenter.toggleActivity(true);
        $.when(datacontext.saveChangedMission(mission))
            .done(function () {
                logger.success(config.toasts.savedData);
            })
            .always(function () {
                impactApp.presenter.toggleActivity(false);
            })
            .fail(function () {
                logger.error(config.toasts.errorSavingData);
            });
    };

    return {
        missions: missions,
        updateMission: updateMission,
        error: error
    };

})(ko, impactApp.datacontext, impactApp.config);