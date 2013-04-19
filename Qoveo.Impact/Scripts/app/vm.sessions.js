window.impactApp = window.impactApp || {};

// Session ViewModel
window.impactApp.sessionsViewModel = (function (ko, datacontext, config) {
    //ViewModel
    var sessions = ko.observableArray();

    var error = ko.observable();

    var logger = config.logger;

    // Operations
    var updateSession = function (session) {
        impactApp.presenter.toggleActivity(true);
        // On checke si on doit ajouter ou modifier une session
        $.when(session.Id ? datacontext.saveChangedSession(session) : datacontext.saveNewSession(session))
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

    var addSession = function (session) {
        sessions.push(datacontext.createSession({
            Name: 'New Session'
        }))
    };
    

    return {
        sessions: sessions,
        updateSession: updateSession,
        addSession: addSession,
        error: error
    }

})(ko, impactApp.datacontext, impactApp.config);