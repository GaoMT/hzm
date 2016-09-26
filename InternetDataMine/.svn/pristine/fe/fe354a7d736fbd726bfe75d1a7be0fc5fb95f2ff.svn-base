using System.Web;
using System.Web.Optimization;

namespace InternetDataMine
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/icon").Include("~/Content/themes/icon.css", "~/Content/themes/color.css"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include("~/Scripts/common*", "~/Scripts/UpLoadFiles*", "~/Scripts/datamanager*"));

            bundles.Add(new ScriptBundle("~/bundles/supcan").Include("~/binary/dynaload.js"));
            

            //使用easyui脚本
            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                        "~/Scripts/jquerymin*",
                        "~/Scripts/jqueryeasyuimin*","~/Scripts/easyuilang*","~/Scripts/datagrid-detailview*"));

            bundles.Add(new ScriptBundle("~/bundles/easyuilang").Include("~/Scripts/easyuilang*"));
            
            //使用easyui皮肤
            bundles.Add(new StyleBundle("~/Content/themes/easyui/css").Include(
                "~/Content/themes/ui-dark-hive/easyui.css"));




                

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

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

            bundles.Add(new ScriptBundle("~/bundles/Echarts").Include("~/Scripts/echarts-*"));

        }

       
    }
}