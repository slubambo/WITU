using System;
using System.Web;
using System.Web.Optimization;
using WITU.Utils;
using FluentNHibernate.Utils;

namespace WITU
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //now for the bundling....
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/base/jquery-{version}.js", "~/Scripts/base/jquery-ui-{version}.js", "~/Scripts/base/jquery.tmpl.js", "~/Scripts/base/jsrender.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery-unobstrusive-ajax").Include(
            //    "~/Scripts/jquery.unobstrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/base/jquery.unobtrusive*",
                        "~/Scripts/base/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/base/modernizr-2.7.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/base/bootstrap.js",
                      "~/Scripts/base/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-libs")
                .Include("~/Scripts/dataTables/js/jquery.dataTables.js", "~/Scripts/dataTables/js/dataTables.responsive.js", "~/Scripts/dataTables/js/dataTables.tableTools.js")
                .Include("~/Scripts/timeline-js/js/storyjs-embed.js")
                .Include("~/Scripts/timeline-js/js/timeline.js")
                .Include("~/Scripts/timeline-js/js/timeline-min.js")
                .Include("~/Scripts/bootstrap-dialog/js/bootstrap-dialog.js"/*, "~/Scripts/bootstrap-dialog/prettify/run_prettify.js"*/)
                .Include("~/Scripts/toastr/toastr.js")
                .Include("~/Scripts/dataTables/Buttons-1.1.2/js/dataTables.buttons.js")
                .Include("~/Scripts/dataTables/Buttons-1.1.2/js/buttons.colVis.js")
                .Include("~/Scripts/dataTables/Buttons-1.1.2/js/buttons.flash.js")
                .Include("~/Scripts/dataTables/Buttons-1.1.2/js/buttons.html5.js")
                .Include("~/Scripts/dataTables/Buttons-1.1.2/js/buttons.print.js")
                .Include("~/Scripts/dataTables/Buttons-1.1.2/js/pdfmake.js").Include("~/Scripts/dataTables/Buttons-1.1.2/js/vfs_fonts.js")
                .Include("~/Scripts/dataTables/FixedHeader-3.1.1/js/dataTables.fixedHeader.js")
                .Include("~/Scripts/base/jquery.jeditable.js")
                .Include("~/Scripts/slick/slick.js")
                .Include("~/Scripts/ElasticTiles/Elastic-tiles.jQuery.js"));

            bundles.Add(new ScriptBundle("~/bundles/html-editors")
                .Include("~/Scripts/tinymce/jquery.tinymce.min.js")
                .Include("~/Scripts/ckeditor/ckeditor.js", 
                "~/Scripts/ckeditor/adapters/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jssor").IncludeDirectory("~/Scripts/jssor", "*.js"));


            bundles.Add(new ScriptBundle("~/bundles/FileUpload")
                .Include("~/Scripts/FileUpload/js/vendor/jquery.ui.widget.js")
                .Include("~/Scripts/FileUpload/js/jquery.iframe-transport.js")
                .Include("~/Scripts/FileUpload/js/jquery.fileupload.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom")
                .Include("~/Scripts/custom/base-script.js")
                .Include("~/Scripts/custom/select2.full.js")
                .Include("~/Scripts/custom/spin.js")
                .IncludeDirectory("~/Scripts/custom", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/chosen")
                .Include("~/Scripts/chosen/chosen.jquery.js")
                .Include("~/Scripts/chosen/docsupport/prism.js"));

            bundles.Add(new StyleBundle("~/Content/chosen").IncludeWithCssRewriteTransform("~/Scripts/chosen/chosen.css")
                .IncludeWithCssRewriteTransform("~/Scripts/chosen/docsupport/prism.css"));

            bundles.Add(new StyleBundle("~/Content/css").IncludeWithCssRewriteTransform("~/Content/base/jquery-ui-{version}.css")

                .IncludeWithCssRewriteTransform("~/Content/base/bootstrap.css")
                .IncludeWithCssRewriteTransform("~/Content/custom/*.css")
                .IncludeWithCssRewriteTransform("~/Content/base/select2.css")
                .IncludeWithCssRewriteTransform("~/Scripts/dataTables/css/jquery.dataTables.css", "~/Scripts/dataTables/css/jquery.dataTable_themeroller.css",
                    "~/Scripts/dataTables/css/dataTables.responsive.css", "~/Scripts/dataTables/css/dataTables.tableTools.css")
                .IncludeWithCssRewriteTransform("~/Scripts/bootstrap-dialog/css/bootstrap-dialog.css")
                .IncludeWithCssRewriteTransform("~/Scripts/timeline-js/css/timeline.css")
                .IncludeWithCssRewriteTransform("~/Scripts/dataTables/Buttons-1.1.2/css/buttons.dataTables.css", "~/Scripts/dataTables/FixedHeader-3.1.1/css/fixedHeader.dataTables.css")
                .IncludeWithCssRewriteTransform("~/Scripts/toastr/toastr.css")
                .IncludeWithCssRewriteTransform("~/Scripts/slick/slick.css")
                .IncludeWithCssRewriteTransform("~/Scripts/ElasticTiles/Elastic-tiles.css")
                );



            bundles.Add(new StyleBundle("~/Content/font-awesome").IncludeWithCssRewriteTransform("~/Content/base/font-awesome.css"));

            //file upload stuff...
            bundles.Add(new StyleBundle("~/Content/file-upload").IncludeWithCssRewriteTransform("~/Content/FileUpload/jquery.fileupload.css", 
                "~/Content/FileUpload/jquery.fileupload-ui.css",
                "~/Content/FileUpload/style.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
