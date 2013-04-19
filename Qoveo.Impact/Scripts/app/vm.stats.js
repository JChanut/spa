window.impactApp = window.impactApp || {};

// Missions ViewModel
window.impactApp.statsViewModel = (function (ko, datacontext, config, _) {
    
    // ViewModel
    var results = ko.observable(), // The DataTable
        teams = ko.observableArray(),
        missions = ko.observableArray(),
        clusters = ko.observableArray(),
        sessions = ko.observableArray(),
        isTutor = ko.observable(),
        tutorId = ko.observable(),
        selectedTeamFilter = ko.observable(),
        selectedMissionFilter = ko.observable(),
        selectedClusterFilter = ko.observable(),
        selectedSessionFilter = ko.observable();

    var dataView = ko.computed(function () {
        if (results()) {
            // Create the google DataView
            var view = new config.google.visualization.DataView(new google.visualization.DataTable(results().dataTable));
            // Filter
            var missionFilter = selectedMissionFilter(),
                teamFilter = selectedTeamFilter(),
                clusterFilter = selectedClusterFilter(),
                sessionFilter = selectedSessionFilter();

            // Filtering the row
            view.setRows(view.getFilteredRows([missionTest(missionFilter), teamTest(teamFilter), clusterTest(clusterFilter), sessionTest(sessionFilter)]));

            // Hiding some columns -> Id's columns
            view.hideColumns([0, 1, 2, 3]);

            return view;
        }
    });

    var statUrl = ko.computed(function () {
        var url = '/api/stats/csv';
        if (isTutor()) {
            url = url + '?tutorId=' + tutorId();
        }
        return url;
    });

    // Filter Management
    // Filter by mission
    var missionTest = function (filter){
        if (!filter) return { column: 3, minValue: 0};
        return { column: 3, value: filter.Id};
    };
    // Filter by team
    var teamTest = function (filter) {
        if (!filter) return { column: 1, minValue: 0 };
        return { column: 1, value: filter.Id };
    };
    // Filter by cluster
    var clusterTest = function (filter) {
        if (!filter) return { column: 2, minValue: 0 };
        return { column: 2, value: filter.Id };
    };
    // Filter by session
    var sessionTest = function (filter) {
        if (!filter) return { column: 0, minValue: 0 };
        return { column: 0, value: filter.Id };
    };

    var logger = config.logger;

    // Gestion des erreurs
    var error = ko.observable();

    return {
        results: results,
        teams: teams,
        missions: missions,
        clusters: clusters,
        sessions: sessions,
        dataView: dataView,
        isTutor: isTutor,
        tutorId: tutorId,
        statUrl: statUrl,
        selectedTeamFilter: selectedTeamFilter,
        selectedMissionFilter: selectedMissionFilter,
        selectedClusterFilter: selectedClusterFilter,
        selectedSessionFilter: selectedSessionFilter,
        error: error,
    };

})(ko, impactApp.datacontext, impactApp.config, _);