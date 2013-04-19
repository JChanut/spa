window.impactApp = window.impactApp || {};

// Tutors ViewModel
window.impactApp.tutorsViewModel = (function (ko, _, datacontext, config) {

    // ViewModel
    var tutors = ko.observableArray(),
        sessions = ko.observableArray(),
        clusters = ko.observableArray(),
        selectedSessionFilter = ko.observable(),
        selectedClusterFilter = ko.observable(),
        logger = config.logger;
    // Gestion des erreurs
    var error = ko.observable();

    // Gestion des filtres
    var filteredTutors = ko.computed(function () {
        var filter1 = selectedSessionFilter(),
            filter2 = selectedClusterFilter();

        var filterArray = _.filter(tutors(), function (o) {
            return sessionTest(filter1, o) && clusterTest(filter2, o);
        });
        return filterArray;
    });

    var sessionTest = function (session, tutor) {
        if (!session) return true;
        if (session.Id == tutor.SessionId()) return true;
        else return false;
    };

    var clusterTest = function (cluster, tutor) {
        if (!cluster) return true;
        if (cluster.Id == tutor.ClusterId()) return true;
        else return false;
    }

    // Operations
    var updateTutor = function (tutor) {
        impactApp.presenter.toggleActivity(true);
        // On checke si on doit ajouter ou modifier un tuteur
        $.when(tutor.Id ? datacontext.saveChangedTutor(tutor) : datacontext.saveNewTutor(tutor))
            .done(function (tutor) {
                logger.success(config.toasts.savedData);
            })
            .always(function () {
                impactApp.presenter.toggleActivity(false);
            })
            .fail(function () {
                logger.error(config.toasts.errorSavingData);
            })
    };

    var addTutor = function (tutor) {
        tutors.push(datacontext.createTutor({
            Name: 'Tutor Name',
            FirstName: 'Tutor FirstName',
            Login : 'Save data to generate user name',
            Password: 'Save data to generate password'
        }))
    };

    return {
        tutors: tutors,
        sessions: sessions,
        clusters: clusters,
        selectedSessionFilter: selectedSessionFilter,
        selectedClusterFilter: selectedClusterFilter,
        filteredTutors: filteredTutors,
        addTutor: addTutor,
        updateTutor: updateTutor,
        error: error
    };

})(ko, _, impactApp.datacontext, impactApp.config);