window.impactApp = window.impactApp || {};

window.impactApp.binder = (function ($, ko, config, vm) {
    var ids = config.viewIds,

        bind = function () {
            ko.applyBindings(vm.shell, getView(ids.shell));
            ko.applyBindings(vm.sessions, getView(ids.session));
            ko.applyBindings(vm.clusters, getView(ids.cluster));
            ko.applyBindings(vm.teams, getView(ids.team));
            ko.applyBindings(vm.tutors, getView(ids.tutor));
            ko.applyBindings(vm.missions, getView(ids.mission));
            ko.applyBindings(vm.stats, getView(ids.stat));
            ko.applyBindings(vm.surveys, getView(ids.survey));
            ko.applyBindings(vm.launch, getView(ids.launch));
        },

        getView = function (viewName) {
            return $(viewName).get(0);
        };

    return {
        bind: bind
    }

})($, ko, impactApp.config, impactApp.vm);