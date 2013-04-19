using System.Web;
using System.Web.Optimization;

namespace Qoveo.Impact
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.fileupload.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // 3rd party Javascript files
            bundles.Add(new ScriptBundle("~/bundles/jsextlibs").Include(
                // knockout and its plugins
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.activity.js",
                "~/Scripts/knockout.command.js",

                // Other 3rd party libraries
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/moment.js",
                "~/Scripts/toastr.js",
                "~/Scripts/moment-datepicker.js",
                "~/Scripts/moment-datepicker-ko.js",
                "~/Scripts/bootstrap-datetimepicker.js",
                "~/Scripts/underscore.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxlogin").Include(
                "~/Scripts/app/ajaxlogin.js"));

            // All Application JS files
            // CAUTION !!!! the order of the scripts is very important for the reference beetween them
            // so be careful
            bundles.Add(new ScriptBundle("~/bundles/impact").Include(
                "~/Scripts/app/config.js",
                "~/Scripts/app/datacontext.js",
                "~/Scripts/app/model.js",
                "~/Scripts/app/presenter.js",
                "~/Scripts/app/vm.shell.js",
                "~/Scripts/app/vm.sessions.js",
                "~/Scripts/app/vm.clusters.js",
                "~/Scripts/app/vm.teams.js",
                "~/Scripts/app/vm.missions.js",
                "~/Scripts/app/vm.tutors.js",
                "~/Scripts/app/vm.stats.js",
                "~/Scripts/app/vm.surveys.js",
                "~/Scripts/app/vm.launch.js",
                "~/Scripts/app/vm.js",
                "~/Scripts/app/binder.js",
                "~/Scripts/app/bindings.js",
                "~/Scripts/app/bootstrapper.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/Impact.css",
                "~/Content/toastr.css",
                "~/Content/moment-datepicker/datepicker.css",
                "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            // Bootstrap and its plugin
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));
        }
    }
}