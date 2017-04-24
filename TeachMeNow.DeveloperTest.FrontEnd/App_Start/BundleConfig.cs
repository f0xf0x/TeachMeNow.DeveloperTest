using System.Web.Optimization;

namespace TeachMeNow.DeveloperTest.FrontEnd {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/bundles/npm_css").Include(
                "~/bower_components/fullcalendar/dist/fullcalendar.css",
                "~/node_modules/angular-bootstrap-datetimepicker/src/css/datetimepicker.css"));
            bundles.Add(new ScriptBundle("~/bundles/npm").Include(
                "~/node_modules/angular-ui-router/release/angular-ui-router.js",
                "~/node_modules/angular-messages/angular-messages.js",
                "~/node_modules/ngstorage/ngStorage.js",
                "~/bower_components/moment/moment.js",
                "~/bower_components/angular-ui-calendar/src/calendar.js",
                "~/bower_components/fullcalendar/dist/fullcalendar.js",
                "~/node_modules/angular-bootstrap-datetimepicker/src/js/datetimepicker.js",
                "~/node_modules/angular-bootstrap-datetimepicker/src/js/datetimepicker.templates.js",
                "~/bower_components/fullcalendar/dist/gcal.jss"
            ));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/angular-route.js")
                .IncludeDirectory("~/Angular", "*.js")
                .IncludeDirectory("~/Angular/app-services", "*.js")
                .IncludeDirectory("~/Angular/home", "*.js")
                .IncludeDirectory("~/Angular/event", "*.js")
                .IncludeDirectory("~/Angular/login", "*.js")
            );

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/site.css"));
        }
    }
}