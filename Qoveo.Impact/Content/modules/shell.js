window.shell = (function ($, tracking) {
    var globalScore = 0,
        scoreEpreuve1 = 0,
        scoreEpreuve2 = 0,
        scoreEpreuve3 = 0,
        jauge1 = 0,
        jauge2 = 0,
        jauge3 = 0,
        jauge4 = 0,
        jauge5 = 0,
        surveys = new Array();

    // Save the tracking locally
    var doTracking = function () {
        tracking.trackResult(
        this.globalScore,
        this.scoreEpreuve1,
        this.scoreEpreuve2,
        this.scoreEpreuve3,
        this.jauge1,
        this.jauge2,
        this.jauge3,
        this.jauge4,
        this.jauge5);
    };

    // Start the tracking : Initialize all the score to 0 and empty the survey's answers
    var startTracking = function () {
        tracking.razTracking();
    };

    // Save the tracking to the server
    var commitTracking = function () {
        return $.when(tracking.addResult(), tracking.addSurvey());
    };

    // Terminate the mission
    var terminate = function () {
        tracking.terminateMission();
    }

    // Return the cluster
    var getCluster = function () {
        var team = tracking.team();
        return team.Cluster;
    };

    // Return the FTA building PDF
    var getFtaPdfUrl = function () {
        var mission = tracking.pendingMission;
        return  mission().FtaPdfUrl();
    };
    
    var addSurvey = function (question, answer) {
        return tracking.trackSurvey(question, answer);
    }

    if (!tracking) {
        console.log('Impossible to activate tracking, please try to refresh the page');
        window.alert('Impossible to activate tracking, please try to refresh the page');
    }

    // Get the data previously save
    if (tracking.trackingResult) {
        globalScore = tracking.trackingResult.GlobalScore;
        scoreEpreuve1 = tracking.trackingResult.ScoreEpreuve1;
        scoreEpreuve2 = tracking.trackingResult.ScoreEpreuve2;
        scoreEpreuve3 = tracking.trackingResult.ScoreEpreuve3;
        jauge1 = tracking.trackingResult.Jauge1;
        jauge2 = tracking.trackingResult.Jauge2;
        jauge3 = tracking.trackingResult.Jauge3;
        jauge4 = tracking.trackingResult.Jauge4;
        jauge5 = tracking.trackingResult.Jauge5;
    }
    

    return {
        startTracking: startTracking,
        doTracking: doTracking,
        commitTracking: commitTracking,
        getCluster: getCluster,
        getFtaPdfUrl: getFtaPdfUrl,
        addSurvey: addSurvey,
        terminate: terminate,
        globalScore: globalScore,
        scoreEpreuve1: scoreEpreuve1,
        scoreEpreuve2: scoreEpreuve2,
        scoreEpreuve3: scoreEpreuve3,
        jauge1: jauge1,
        jauge2: jauge2,
        jauge3: jauge3,
        jauge4: jauge4,
        jauge5: jauge5,
        surveys: surveys
    }
})($, window.parent.impactApp.vm.launch);
