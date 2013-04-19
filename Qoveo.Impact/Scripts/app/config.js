window.impactApp = window.impactApp || {};

window.impactApp.config = (function (toastr, google) {

    var
        // properties
        //-----------------
        // Hashes
        hashes = {
            teams: '#/teams',
            clusters: '#/clusters',
            sessions: '#/sessions',
            missions: '#/missions',
            tutors: '#/tutors',
            stats: '#/stats',
            surveys: '#/surveys',
            launch: '#/launch',
        },

        logger = toastr, // use toastr for the logger
        google = google;

        // Views
        viewIds = {
            shell: '#shellTop-view',
            team: '#teams-view',
            cluster: '#clusters-view',
            session: '#sessions-view',
            mission: '#missions-view',
            tutor: '#tutors-view',
            stat: '#stats-view',
            survey: '#surveys-view',
            launch: '#launch-view',
        },
        
        // Hidden Fields
        hiddenIds = {
            userId: '#hidden-user-id',
            isTeam: '#hidden-isteam',
            teamId: '#hidden-team-id',
            isTutor: '#hidden-istutor',
            tutorId: '#hidden-tutor-id',
        },

        // Toast basic message
        toasts = {
            changesPending: 'Please save or cancel your changes before leaving the page.',
            errorSavingData: 'Data could not be saved. Please check the logs.',
            errorGettingData: 'Could not retrieve data.  Please check the logs.',
            invalidRoute: 'Cannot navigate. Invalid route',
            retreivedData: 'Data retrieved successfully',
            savedData: 'Data saved successfully'
        },

        // methods
        //-----------------
        dataserviceInit = function () { },

        googleChartInit = function () {
            // Load the Visualization API and the table package.
            google.load('visualization', '1.0', { 'packages': ['table'] });
        };

        
    return {
        viewIds: viewIds,
        hashes: hashes,
        toasts: toasts,
        logger: logger,
        hiddenIds: hiddenIds,
        dataserviceInit: dataserviceInit,
        googleChartInit: googleChartInit,
        google: google
    }
})(toastr, google);