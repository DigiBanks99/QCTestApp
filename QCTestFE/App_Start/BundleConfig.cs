namespace QCTestFE
{
  using System.Web;
  using System.Web.Optimization;

  public class BundleConfig
  {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new StyleBundle("~/content/css").Include(
        "~/content/app.css",
        "~/content/header.css",
        "~/content/accordian.css",
        "~/content/shopping.css"));

      bundles.Add(new ScriptBundle("~/js/jquery").Include("~/scripts/vendor/jquery-{version}.js"));

      bundles.Add(new ScriptBundle("~/js/app").Include(
        "~/scripts/vendor/angular.js",
        "~/scripts/vendor/angular-route.js",
        "~/scripts/vendor/angular-sanitize.js",
        "~/scripts/app.js",
        "~/scripts/controllers.js",
        "~/scripts/accordion.js",
        "~/scripts/directives/directives.js"));
    }
  }
}
