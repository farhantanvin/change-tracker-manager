using System.Web;
using System.Web.Optimization;

namespace GHIApplication
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                           "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
           "~/Scripts/angular.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                  "~/Scripts/ruang-admin.min.js",
                  "~/Scripts/bootstrap.bundle.min.js",
                  "~/Scripts/jquery.dataTables.min.js",
                  "~/Scripts/dataTables.bootstrap4.min.js",
                  "~/Scripts/jquery.easing.min.js",
                  "~/Scripts/sweet-alert.js",
                  "~/Scripts/select-two.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/all.min.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/ruang-admin.min.css",
                      "~/Content/dataTables.bootstrap4.min.css",
                      "~/Content/sweet-alert.css",
                      "~/Content/select-two.css",
                      "~/Content/site.css"));
        }
    }
}
