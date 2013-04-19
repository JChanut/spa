window.impactApp = window.impactApp || {};

// Missions ViewModel
window.impactApp.surveysViewModel = (function (ko, datacontext, config) {
    
    // ViewModel
    var surveys = ko.observable(), // The DataTable
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
        if (surveys())
        {
            var view = new config.google.visualization.DataView(new google.visualization.DataTable(surveys().dataTable));

            // Filtert
            var missionFilter = selectedMissionFilter(),
                sessionFilter = selectedSessionFilter(),
                clusterFilter = selectedClusterFilter(),
                teamFilter = selectedTeamFilter();

            // Filtering the row
            view.setRows(view.getFilteredRows([missionTest(missionFilter), teamTest(teamFilter), clusterTest(clusterFilter), sessionTest(sessionFilter)]));

            // Hiding some columns -> Id's columns
            view.hideColumns([0, 1, 2, 3]);

            return view;
        }
    });

    var surveyUrl = ko.computed(function () {
        var url = '/api/stats/surveycsv';
        if (isTutor()) {
            url = url + '?tutorId=' + tutorId();
        }
        return url;
    });

    // Filter Management
    var missionTest = function (filter) {
        if (!filter) return { column: 3, minValue: 0 };
        return { column: 3, value: filter.Id };
    };

    var teamTest = function (filter) {
        if (!filter) return { column: 1, minValue: 0 };
        return { column: 1, value: filter.Id };
    };

    var clusterTest = function (filter) {
        if (!filter) return { column: 2, minValue: 0 };
        return { column: 2, value: filter.Id };
    };

    var sessionTest = function (filter) {
        if (!filter) return { column: 0, minValue: 0 };
        return { column: 0, value: filter.Id };
    };

    var logger = config.logger;

    // Gestion des erreurs
    var error = ko.observable();

    return {
        surveys: surveys,
        dataView: dataView,
        missions: missions,
        teams: teams,
        clusters: clusters,
        sessions: sessions,
        isTutor: isTutor,
        tutorId: tutorId,
        surveyUrl: surveyUrl,
        selectedTeamFilter: selectedTeamFilter,
        selectedClusterFilter: selectedClusterFilter,
        selectedMissionFilter: selectedMissionFilter,
        selectedSessionFilter: selectedSessionFilter,
        error: error
    };

})(ko, impactApp.datacontext, impactApp.config);