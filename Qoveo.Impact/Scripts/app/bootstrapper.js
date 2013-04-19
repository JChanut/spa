window.impactApp = window.impactApp || {};

window.impactApp.bootstrapper = (function($, config, presenter, binder, datacontext, vm){
    var
        run = function () {
            var isTutor = $(config.hiddenIds.isTutor).attr('value'),
                tutorId = $(config.hiddenIds.tutorId).attr('value');

            presenter.toggleActivity(true);
            config.dataserviceInit();
            config.googleChartInit();

            $.when(
                // Load the data from the server
                datacontext.getSessions(vm.sessions.sessions, vm.sessions.error, tutorId),
                datacontext.getClusters(vm.clusters.clusters, vm.clusters.error, tutorId),
                datacontext.getTeams(vm.teams.teams, vm.teams.error, tutorId),
                datacontext.getTutors(vm.tutors.tutors, vm.tutors.error),
                datacontext.getMissions(vm.missions.missions, vm.missions.error),
                datacontext.getResults(vm.stats.results, vm.stats.error, tutorId),
                datacontext.getTeam(vm.launch.team, vm.launch.error, $(config.hiddenIds.teamId).attr('value')),
                datacontext.getSurveys(vm.surveys.surveys, vm.surveys.error, tutorId))
                .done(function () {
                    // Get the already loaded data for the filter in the ViewModels
                    vm.teams.sessions = vm.sessions.sessions;
                    vm.teams.clusters = vm.clusters.clusters;
                    vm.tutors.sessions = vm.sessions.sessions;
                    vm.tutors.clusters = vm.clusters.clusters;
                    vm.stats.missions = vm.missions.missions;
                    vm.stats.teams = vm.teams.teams;
                    vm.stats.clusters = vm.clusters.clusters;
                    vm.stats.sessions = vm.sessions.sessions;
                    vm.surveys.missions = vm.missions.missions;
                    vm.surveys.teams = vm.teams.teams;
                    vm.surveys.sessions = vm.sessions.sessions;
                    vm.surveys.clusters = vm.clusters.clusters;
                    vm.launch.clusters = vm.clusters.clusters;
                    vm.launch.missions(vm.missions.missions());

                    // Get some datas from the DOM (hidden fields)
                    vm.stats.isTutor(isTutor);
                    vm.stats.tutorId(tutorId);
                    vm.surveys.isTutor(isTutor);
                    vm.surveys.tutorId(tutorId);

                    // Initiate the Knockout bindings
                    binder.bind();
                })
                .always(function () {
                    presenter.toggleActivity(false);
                });
        };

    return {
        run: run
    }
})($, impactApp.config, impactApp.presenter, impactApp.binder, impactApp.datacontext, impactApp.vm)
