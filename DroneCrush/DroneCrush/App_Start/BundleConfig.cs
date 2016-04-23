using System.Web;
using System.Web.Optimization;

namespace DroneCrush
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/bower_components/angular/angular.js",
                        "~/Scripts/bower_components/angular-route/angular-route.js",
                        "~/Scripts/bower_components/angular-ui-router/release/angular-ui-router.js",
                        "~/Scripts/angular-resource.js",
                        "~/Scripts/bower_components/lodash/lodash.js",
                        "~/Scripts/bower_components/angular-simple-logger/dist/angular-simple-logger.js",
                        "~/Scripts/bower_components/angular-google-maps/dist/angular-google-maps.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/App/App.js",
                        "~/App/Core/*.module.js",
                        "~/App/Core/config.js",
                        "~/App/Main/*.module.js",
                        "~/App/Main/*.controller.js",

                        "~/App/DataServices/*.module.js",
                        "~/App/DataServices/*.data.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));



        }
    }
}
