window.impactApp = window.impactApp || {};

window.impactApp.datacontext = (function (ko) {

    var datacontext = {
        getTeams: getTeams,
        getTeam: getTeam,
        getClusters: getClusters,
        getSessions: getSessions,
        getTutors: getTutors,
        getMissions: getMissions,
        getResults: getResults,
        getSurveys: getSurveys,
        saveChangedCluster: saveChangedCluster,
        saveChangedSession: saveChangedSession,
        saveChangedMission: saveChangedMission,
        saveNewSession: saveNewSession,
        createSession: createSession,
        createTutor: createTutor,
        saveChangedTutor: saveChangedTutor,
        saveNewTutor: saveNewTutor,
        saveChangedTeam: saveChangedTeam,
        saveNewResult: saveNewResult,
        saveNewSurvey: saveNewSurvey
    };

    return datacontext;

    ///----------------- TEAMS -----------------------///
    function getTeams(teamsObservable, errorObservable, tutorId) {
        return ajaxRequest("get", tutorId ? teamByTutorUrl(tutorId) : teamUrl())
            .done(getSucceeded)
            .fail(getFailed);

        function getSucceeded(data) {
            var mappedTeams = $.map(data, function (list) { return new createTeam(list); });
            teamsObservable(mappedTeams);
        }

        function getFailed() {
            errorObservable("Error retrieving teams lists.");
        }
    }

    function getTeam(teamObservable, errorObservable, teamId) {
        return ajaxRequest("get", teamUrl(teamId))
            .done(getSucceeded)
            .fail(getFailed);

        function getSucceeded(data) {
            teamObservable(createTeam(data));
        }

        function getFailed() {
            errorObservable("Error retrieving team.")
        }

    }

    function createTeam(data) {
        return new datacontext.Team(data); // Team is injected by model.js
    }

    function saveChangedTeam(team) {
        clearErrorMessage(team);
        return ajaxRequest("put", teamUrl(team.Id), team)
            .fail(function () {
                team.ErrorMessage("Error updating team");
            });
    }

    ///----------------- CLUSTERS -----------------------///
    function getClusters(clustersObservable, errorObservable, tutorId) {
        return ajaxRequest("get", tutorId ? clusterByTutorUrl(tutorId) : clusterUrl())
            .done(getSucceeded)
            .fail(getFailed);

        function getSucceeded(data) {
            var mappedClusters = $.map(data, function (list) { return new createCluster(list); });
            clustersObservable(mappedClusters);
        }

        function getFailed() {
            errorObservable("Error retrieving teams lists.");
        }
    }

    function createCluster(data) {
        return new datacontext.Cluster(data); // Cluster is injected by model.js
    }

    function saveChangedCluster(cluster) {
        clearErrorMessage(cluster);
        return ajaxRequest("put", clusterUrl(cluster.Id), cluster)
            .fail(function () {
                cluster.ErrorMessage("Error updating cluster.");
            });
    }

    ///----------------- SESSIONS -----------------------///
    function getSessions(sessionsObservable, errorObservable, tutorId) {
        return ajaxRequest("get", tutorId ? sessionByTutorUrl(tutorId) : sessionUrl())
            .done(getSucceeded)
            .fail(getFailed);

        function getSucceeded(data) {
            var mappedSessions = $.map(data, function (list) { return new createSession(list); });
            sessionsObservable(mappedSessions);
        }

        function getFailed() {
            errorObservable("Error retrieving teams lists.");
        }
    }

    function createSession(data) {
        return new datacontext.Session(data); // Session is injected by model.js
    }

    function saveChangedSession(session) {
        clearErrorMessage(session);
        return ajaxRequest("put", sessionUrl(session.Id), session)
            .fail(function () {
                session.ErrorMessage("Error updating session.");
            });
    }

    function saveNewSession(session) {
        clearErrorMessage(session);
        return ajaxRequest("post", sessionUrl(), session)
            .fail(function () {
                session.ErrorMessage("Error updating session.");
            });
    }

    ///----------------- TUTORS -----------------------///
    function getTutors(tutorsObservable, errorObservable) {
        return ajaxRequest("get", tutorUrl())
            .done(getSucceeded)
            .fail(getFailed);

        function getSucceeded(data) {
            var mappedTutors = $.map(data, function (list) { return new createTutor(list); });
            tutorsObservable(mappedTutors);
        }

        function getFailed() {
            errorObservable("Error retrieving tutors lists.");
        }
    }

    function createTutor(data) {
        return new datacontext.Tutor(data); // Tutor is injected by model.js
    }

    function saveChangedTutor(tutor) {
        clearErrorMessage(tutor);
        return ajaxRequest("put", tutorUrl(tutor.Id), tutor)
            .fail(function () {
                tutor.ErrorMessage("Error updating tutor");
            });
    }

    function saveNewTutor(tutor) {
        clearErrorMessage(tutor);
        return ajaxRequest("post", tutorUrl(), tutor)
            .fail(function () {
                tutor.ErrorMessage("Error updating tutor");
            });
    }

    ///----------------- MISSIONS -----------------------///
    function getMissions(missionsObservable, errorObservable) {
        return ajaxRequest("get", missionUrl())
            .done(getSucceeded)
            .fail(getFailed);

        function getSucceeded(data) {
            var mappedMissions = $.map(data, function (list) { return new createMission(list); });
            missionsObservable(mappedMissions);
        }

        function getFailed() {
            errorObservable("Error retrieving missions lists.");
        }
    }

    function createMission(data) {
        return new datacontext.Mission(data); // Tutor is injected by model.js
    }

    function saveChangedMission(mission) {
        clearErrorMessage(mission);
        return ajaxRequest("put", missionUrl(mission.Id), mission)
            .fail(function () {
                mission.ErrorMessage("Error updating mission");
            });
    }

    ///----------------- RESULTS -----------------------///
    function getResults(resultsObservable, errorObservable, tutorId) {
        return ajaxRequest("get", tutorId ? resultForTutorUrl(tutorId) : resultUrl())
            .done(getSucceeded)
            .fail(getFailed);

        function getSucceeded(data) {
            var mappedResults =  new createResult(data);
            resultsObservable(mappedResults);
        }

        function getFailed() {
            errorObservable("Error retrieving results lists.");
        }
    }

    function createResult(data) {
        return new datacontext.Result(data); // Result is injected by model.js
    }

    function saveNewResult(result) {
        clearErrorMessage(result);
        return ajaxRequest("post", trackingResultUrl(), result)
            .fail(function () {
                result.ErrorMessage("Error updating result");
            });
    }

    ///----------------- SURVEY -----------------------///
    function getSurveys(surveysObservable, errorObservable, tutorId) {
        return ajaxRequest("get", tutorId ? surveyForTutorUrl(tutorId) : surveyUrl())
            .done(getSucceeded)
            .fail(getFailed);

        function getSucceeded(data) {
            var mappedSurveys = new createSurvey(data);
            surveysObservable(mappedSurveys);
        }

        function getFailed() {
            errorObservable("Error retrieving surveys lists.");
        }
    }

    function createSurvey(data) {
        return new datacontext.Survey(data); // Tutor is injected by model.js
    }

    function saveNewSurvey(survey) {
        clearErrorMessage(survey);
        return ajaxRequest("post", trackingSurveyUrl(), survey)
            .fail(function () {
                survey.ErrorMessage("Error updating survey");
            });
    }


    // Private
    function clearErrorMessage(entity) { entity.ErrorMessage(null); }
    function ajaxRequest(type, url, data) { // Ajax helper
        var options = {
            dataType: "json",
            contentType: "application/json",
            cache: false,
            type: type,
            data: ko.toJSON(data)
        };
        return $.ajax(url, options);
    }
    // routes
    function teamUrl(id) { return "/api/team/" + (id || ""); }
    function teamByTutorUrl(tutorId) { return "/api/team/tutor?tutorId=" + tutorId; }
    function clusterUrl(id) { return "/api/cluster/" + (id || ""); }
    function clusterByTutorUrl(tutorId) { return "/api/cluster/tutor?tutorId=" + tutorId; }
    function sessionUrl(id) { return "/api/session/" + (id || ""); }
    function sessionByTutorUrl(tutorId) { return "/api/session/tutor?tutorId=" + tutorId; }
    function tutorUrl(id) { return "/api/tutor/" + (id || ""); }
    function missionUrl(id) { return "api/mission/" + (id || ""); }
    function resultUrl(id) { return "api/stats/all" + (id || ""); }
    function resultForTutorUrl(tutorId) { return "api/stats/all?tutorId=" + tutorId; }
    function surveyUrl(id) { return "api/stats/survey" + (id || ""); }
    function surveyForTutorUrl(tutorId) { return "api/stats/survey?tutorId=" + tutorId; }
    function trackingResultUrl(id) { return "api/result/" + (id || ""); }
    function trackingSurveyUrl(id) { return "api/survey/" + (id || ""); }
})(ko);