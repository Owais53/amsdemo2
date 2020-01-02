using System.Web;
using System.Web.Optimization;

namespace amsdemo
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquerylogin").Include(

                         "~/Scripts/jquery-3.2.1.min.js",
                        "~/Scripts/animsition.min.js",
                        "~/Scripts/popper.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/select2.min.js",
                        "~/Scripts/moment.min.js",
                        "~/Scripts/daterangepicker.js",
                        "~/Scripts/countdowntime.js",
                        "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/popper.min.js",
                        "~/Scripts/metisMenu.min.js",
                        "~/Scripts/jquery.slimscroll.min.js",
                        "~/Scripts/app.min.js",
                        "~/Scripts/dashboard_1_demo.js",
                        "~/Scripts/datatables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/themify-icons.css",
                      "~/Content/main.min.css",
                      "~/Content/datatables.min.css"));

            bundles.Add(new StyleBundle("~/Content/cssforlogin").Include(
                     "~/Content/bootstrap.min1.css",
                     "~/Content/font-awesome.min1.css",
                     "~/Content/material-design-iconic-font.min.css",
                     "~/Content/animate.css",
                     "~/Content/hamburgers.min.css",
                     "~/Content/animsition.min.css",
                     "~/Content/select2.min.css",
                     "~/Content/daterangepicker.css",
                     "~/Content/util.css",
                     "~/Content/main1.css"));
        }
    }
}
