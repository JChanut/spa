(function (ko, datacontext) {
    datacontext.Team = Team;
    datacontext.Cluster = Cluster;
    datacontext.Session = Session;
    datacontext.Tutor = Tutor;
    datacontext.Mission = Mission;
    datacontext.Result = Result;
    datacontext.Survey = Survey;

    function Team(data) {
        var self = this;
        data = data || {};

        // persisted properties
        self.Id = data.Id;
        self.Name = data.Name;
        self.Password = data.Password;
        self.SessionId = data.SessionId;
        self.ClusterId = data.ClusterId;
        self.Session = data.Session,
        self.Login = data.Login,
        self.ResultList = data.ResultList;
        self.Cluster = data.Cluster;

        // Non-persisted properties
        self.ErrorMessage = ko.observable();
    };

    function Cluster(data) {
        var self = this;
        data = data || {};

        // persisted properties
        self.Id = data.Id;
        self.Name = ko.observable(data.Name);
        self.PdfUrl = ko.observable(data.PdfUrl);

        // Non-persisted properties
        self.ErrorMessage = ko.observable();
        self.File = ko.observable();

        // Auto-Subscribe when those properties changes
        self.File.subscribe(function () {
            self.PdfUrl(self.File().Location);
        });
    }

    function Session(data) {
        var self = this;
        data = data || {};

        self.Id = data.Id;
        self.Name = ko.observable(data.Name);
        self.StartDate = ko.observable(data.StartDate);
        self.EndDate = ko.observable(data.EndDate);
        self.Mission1StartDate = ko.observable(data.Mission1StartDate);
        self.Mission1EndDate = ko.observable(data.Mission1EndDate);
        self.Mission2StartDate = ko.observable(data.Mission2StartDate);
        self.Mission2EndDate = ko.observable(data.Mission2EndDate);
        self.Mission3StartDate = ko.observable(data.Mission3StartDate);
        self.Mission3EndDate = ko.observable(data.Mission3EndDate);
        self.Mission4StartDate = ko.observable(data.Mission4StartDate);
        self.Mission4EndDate = ko.observable(data.Mission4EndDate);

        // Non-persisted properties
        self.ErrorMessage = ko.observable();
    }

    function Mission(data) {
        var self = this;
        data = data || {};

        // persisted properties
        self.Id = data.Id;
        self.Order = data.Order;
        self.Name = data.Name;
        self.MissionUrl = data.MissionUrl;
        self.FtaPdfUrl = ko.observable(data.FtaPdfUrl);

        // Non-persisted properties
        self.ErrorMessage = ko.observable();
        self.File = ko.observable();

        // Auto-Subscribe when those properties changes
        self.File.subscribe(function () {
            self.FtaPdfUrl(self.File().Location);
        });
    }

    function Tutor(data) {
        var self = this;
        data = data || {};

        self.Id = data.Id;
        self.Name = ko.observable(data.Name);
        self.FirstName = ko.observable(data.FirstName);
        self.SessionId = ko.observable(data.SessionId);
        self.ClusterId = ko.observable(data.ClusterId);
        self.Login = data.Login;
        self.Password = ko.observable(data.Password);

        // Non-persisted properties
        self.ErrorMessage = ko.observable();
    }

    function Result(data) {
        var self = this;
        data = data || {};

        self.dataTable = data;

        // Non-persisted properties
        self.ErrorMessage = ko.observable();
    }

    function Survey(data) {
        var self = this;
        data = data || {};

        self.dataTable = data;

        // Non-persisted properties
        self.ErrorMessage = ko.observable();
    }

})(ko, impactApp.datacontext);