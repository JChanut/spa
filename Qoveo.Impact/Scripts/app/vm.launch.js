window.impactApp = window.impactApp || {};

// Session ViewModel
window.impactApp.launchViewModel = (function (ko, datacontext, _, config) {
    //ViewModel
    var team = ko.observable(),
        missions = ko.observableArray(),
        clusters = ko.observableArray(),
        chosenMissionId = ko.observable(),
        selectedMissionId = null,
        trackingResult = {
            GlobalScore: 0,
            ScoreEpreuve1: 0,
            ScoreEpreuve2: 0,
            ScoreEpreuve3: 0,
            Jauge1: 0,
            Jauge2: 0,
            Jauge3: 0,
            Jauge4: 0,
            Jauge5: 0
        },
        trackingSurvey = new Array(),
        logger = config.logger;

    // Computed Observable that return the pending mission
    var pendingMission = ko.computed(function () {
        if (missions()) {
            var missionList = missions();
            var missionId = chosenMissionId();
            return _.find(missionList, function (o) {
                return o.Order == missionId
            })
        }
    });

    // Computed Observable that return the pending result
    var pendingMissionResult = ko.computed(function () {
        if (team() && team().ResultList && pendingMission())
        {
            var results = team().ResultList;
            return findResult(results);
        }
    });

    // Computed Observable for the gauge
    var dataView = ko.computed(function () {
        if (pendingMissionResult()) {
            var result = pendingMissionResult();
            var data = config.google.visualization.arrayToDataTable([
              ['Label', 'Value'],
              ['Economic', result.Jauge1],
              ['Social', result.Jauge2],
              ['Image', result.Jauge3],
              ['Environment', result.Jauge4],
              ['Legal', result.Jauge5]
            ]);
            return data;
        }
    });

    // Called when the user want to change the mission
    var selectMission = function (mission) {
        if (!pendingMissionResult()) {
            // Can Select a new mission only if another mission is selected than the pending mission
            if (mission.Id != chosenMissionId()) {
                // Saving the mission selected by the user
                selectedMissionId = mission.Id
                // Showing the confirmation modal window
                $('#confirmModal').modal('show');
            }
        }
        else {
            chosenMissionId(mission.Order);
        }
    };

    // Called By The confirmation Modal window
    var confirmChangeMission = function () {
        // Hide the modal
        $('#confirmModal').modal('hide');
        // Go the mission
        chosenMissionId(selectedMissionId);
    };

    // Track the result locally
    var trackResult = function (score, score1, score2, score3, jauge1, jauge2, jauge3, jauge4, jauge5) {
        var t = team();
        var mission = pendingMission();
        trackingResult.TeamId = t.Id;
        trackingResult.MissionId = mission.Id;
        trackingResult.GlobalScore = score;
        trackingResult.ScoreEpreuve1= score1;
        trackingResult.ScoreEpreuve2 = score2;
        trackingResult.ScoreEpreuve3 = score3;
        trackingResult.Jauge1 = jauge1;
        trackingResult.Jauge2 = jauge2;
        trackingResult.Jauge3 = jauge3;
        trackingResult.Jauge4 = jauge4;
        trackingResult.Jauge5 = jauge5;
        trackingResult.ErrorMessage = error;
    };

    var trackSurvey = function (question, answer) {
        var t = team();
        var mission = pendingMission();
        trackingSurvey.push({ MissionId: mission.Id, TeamId: t.Id, Question: question, Answer: answer });
    };

    // Saved the result to the server
    var addResult = function () {
        impactApp.presenter.toggleActivity(true);
         return $.when(datacontext.saveNewResult(trackingResult))
            .done(function (result) {
                var t = team();
                t.ResultList.push(result);
                logger.success(config.toasts.savedData);
            })
            .always(function () {
                impactApp.presenter.toggleActivity(false);
            })
            .fail(function () {
                logger.error(config.toasts.errorSavingData);
            })
    };

    // Saved the survey to the server
    var addSurvey = function () {
        impactApp.presenter.toggleActivity(true);
        var data = trackingSurvey;
        data.ErrorMessage = error;
        return $.when(datacontext.saveNewSurvey(data))
            .done(function () {
                logger.success(config.toasts.savedData);
            })
            .always(function () {
                impactApp.presenter.toggleActivity(false);
            })
            .fail(function () {
                logger.error(config.toasts.errorSavingData);
            })
    };

    // Function that terminate the mission
    // Only refresh the ViewModel via observable
    var terminateMission = function () {
        // Indicate the observable that the viewmodel must be updated
        team.valueHasMutated();
        razTracking();
    }

    // Function that RAZ the Result and survey
    var razTracking = function () {
        // We must empty the survey aray
        trackingSurvey.length = 0;
        // Web must empty the result too
        trackResult(0, 0, 0, 0, 0, 0, 0, 0, 0);
    }

    var findResult = function (results) {
        return _.find(results, function (o) {
            return o.MissionId == pendingMission().Id
        });
    };

    var error = ko.observable();

    // Show Mission 1 by default
    chosenMissionId(1);

    return {
        team: team,
        missions: missions,
        clusters: clusters,
        chosenMissionId: chosenMissionId,
        pendingMissionResult: pendingMissionResult,
        pendingMission: pendingMission,
        selectMission: selectMission,
        confirmChangeMission: confirmChangeMission,
        dataView: dataView,
        trackResult: trackResult,
        trackingResult: trackingResult,
        addResult: addResult,
        addSurvey: addSurvey,
        trackSurvey: trackSurvey,
        trackingSurvey: trackingSurvey,
        terminateMission: terminateMission,
        razTracking: razTracking,
        error: error
    }

})(ko, impactApp.datacontext, _, impactApp.config);