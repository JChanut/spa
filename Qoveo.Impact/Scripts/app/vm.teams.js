window.impactApp = window.impactApp || {};

// Teams ViewModel
window.impactApp.teamsViewModel = (function (ko, _, datacontext, config) {

    // ViewModel
    var teams = ko.observableArray(),
        sessions = ko.observableArray(),
        clusters = ko.observableArray(),
        selectedSession = ko.observable(),
        selectedCluster = ko.observable(),
        logger = config.logger;

    // Gestion des erreurs
    var error = ko.observable();

    // Gestion des filtres
    var filteredTeams = ko.computed(function () {
        var filter1 = selectedSession(),
            filter2 = selectedCluster();

        var filterArray = _.filter(teams(), function (o) {
            return sessionTest(filter1, o) && clusterTest(filter2, o);
        });
        return filterArray;
    });

    var sessionTest = function (session, team) {
        if (!session) return true;
        if (session.Id == team.SessionId) return true;
        else return false;
    };

    var clusterTest = function (cluster, team) {
        if (!cluster) return true;
        if (cluster.Id == team.ClusterId) return true;
        else return false;
    };

    // Operations
    var updateTeam = function (team) {
        impactApp.presenter.toggleActivity(true);
        $.when(datacontext.saveChangedTeam(team))
            .done(function () {
                logger.success(config.toasts.savedData);
            })
            .always(function () {
                impactApp.presenter.toggleActivity(false);
            })
            .fail(function () {
                logger.error(config.toasts.errorSavingData);
            })
    }

    return {
        teams: teams,
        sessions: sessions,
        clusters: clusters,
        selectedSession: selectedSession,
        selectedCluster: selectedCluster,
        filteredTeams: filteredTeams,
        updateTeam: updateTeam,
        error: error
    };

})(ko, _, impactApp.datacontext, impactApp.config);