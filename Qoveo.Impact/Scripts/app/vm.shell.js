window.impactApp = window.impactApp || {};

window.impactApp.shellViewModel = (function (ko, datacontext, presenter, config) {

    // Gestion de la navigation
    var
        menuHashes = config.hashes,
	    chosenMenuId = ko.observable(),
        isTeam = ko.observable($(config.hiddenIds.isTeam).attr('value')),
        userName = ko.observable($(config.hiddenIds.userId).attr('value')),
        teamId = ko.observable($(config.hiddenIds.teamId).attr('value')),
        isTutor = ko.observable($(config.hiddenIds.isTutor).attr('value')),
        tutorId = ko.observable($(config.hiddenIds.tutorId).attr('value')),
    
        // Gestion des erreurs
        error = ko.observable();

    // Routing
    Sammy(function () {
        this.get('#/:menu', function () {
            chosenMenuId(this.params.menu);
            presenter.goToView(this.params.menu)
        });

        // Default route
        this.get('/', function () {
            if (isTeam())
                this.app.runRoute('get', '#/launch');
            else if (isTutor())
                this.app.runRoute('get', '#/stats');
            else
                this.app.runRoute('get', '#/sessions')
        });

    }).run();

    return {
        menuHashes: menuHashes,
        chosenMenuId: chosenMenuId,
        isTeam: isTeam,
        isTutor: isTutor,
        userName: userName,
        teamId: teamId,
        tutorId: tutorId,
        error: error
    };

})(ko, impactApp.datacontext, impactApp.presenter, impactApp.config);