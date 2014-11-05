using System.Web;
using System.Web.Optimization;
using System.Web.Optimization.React;

namespace TheDevelopersStuff.Web.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libs/jquery-{version}.js",
                        "~/Scripts/libs/react-0.12.0.min.js"));

            bundles.Add(new StyleBundle("~/Content/css"));

            bundles.Add(new JsxBundle("~/bundles/main").Include(
                "~/Scripts/components/filters.jsx"
                ));
        }
    }
}