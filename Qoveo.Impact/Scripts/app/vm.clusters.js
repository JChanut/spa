window.impactApp = window.impactApp || {};

// Cluster ViewModel
window.impactApp.clustersViewModel = (function (ko, datacontext, config) {

    // ViewModel
    var clusters = ko.observableArray();

    var logger = config.logger;

    // Gestion des erreurs
    var error = ko.observable();

    // Operations
    var updateCluster = function (cluster) {
        impactApp.presenter.toggleActivity(true);
        $.when(datacontext.saveChangedCluster(cluster))
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
        clusters: clusters,
        updateCluster: updateCluster,
        error: error
    };

})(ko, impactApp.datacontext, impactApp.config);