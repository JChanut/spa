window.impactApp = window.impactApp || {};

window.impactApp.vm = (function (shell, teams, clusters, sessions, tutors, launch, missions, stats, surveys) {
    return {
        shell: shell,
        teams: teams,
        clusters: clusters,
        sessions: sessions,
        missions: missions,
        tutors: tutors,
        stats: stats,
        surveys: surveys,
        launch: launch,
    }
})(impactApp.shellViewModel, impactApp.teamsViewModel, impactApp.clustersViewModel, impactApp.sessionsViewModel,
impactApp.tutorsViewModel, impactApp.launchViewModel, impactApp.missionsViewModel, impactApp.statsViewModel, impactApp.surveysViewModel);