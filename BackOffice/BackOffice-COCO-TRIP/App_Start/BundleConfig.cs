using System.Web;
using System.Web.Optimization;

namespace BackOffice_COCO_TRIP
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js")); 

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/custom.css",
                      "~/Content/font-awesome.css",
                      "~/Content/global.css",
                      "~/Content/sidebar.css"
                      ));

            bundles.Add(new StyleBundle("~/bundles/customJS").Include(
                     "~/Scripts/custom.js"
                     ));
            bundles.Add(new StyleBundle("~/bundles/categoriesJS").Include(
                     "~/Scripts/categories.js"
                     ));

            bundles.Add(new StyleBundle("~/bundles/categoriesCSS").Include(
                    "~/Content/Categorias/categorias.css"
                    ));

            bundles.Add(new StyleBundle("~/bundles/Lugares_delete-line-tableJS").Include(
                     "~/Scripts/Lugares/delete-line-table.js"
                     ));

            bundles.Add(new StyleBundle("~/bundles/Lugares_insert-line-tableJS").Include(
                     "~/Scripts/Lugares/insert-line-table.js"
                     ));

            bundles.Add(new StyleBundle("~/bundles/Lugares_insert-coordsJS").Include(
                     "~/Scripts/Lugares/google-map.js"
                     ));
            bundles.Add(new StyleBundle("~/bundles/Lugares_show-coordsJS").Include(
                     "~/Scripts/Lugares/google-map-detail.js"
                     ));

          // Localidad M8
            bundles.Add(new StyleBundle("~/bundles/Localidad_insert-coordsJS").Include(
                         "~/Scripts/M8_Localidad/google-map.js"
                         ));

      
            bundles.Add(new StyleBundle("~/bundles/lugares_homeCSS").Include(
                    "~/Content/Lugares/home.css"
                    ));

            bundles.Add(new StyleBundle("~/bundles/lugares_addCSS").Include(
                    "~/Content/Lugares/add.css"
                    ));

            bundles.Add(new StyleBundle("~/bundles/lugares_modifyCSS").Include(
                    "~/Content/Lugares/modify.css"
                    ));

            bundles.Add(new StyleBundle("~/bundles/lugares_activityCSS").Include(
                    "~/Content/Lugares/activity.css"
                    ));

            bundles.Add(new StyleBundle("~/bundles/lugares_detailCSS").Include(
                    "~/Content/Lugares/detail.css"
                    ));

            bundles.Add(new StyleBundle("~/bundles/lugares_viewallCSS").Include(
                    "~/Content/Lugares/view_all.css"
                    ));
            bundles.Add(new StyleBundle("~/bundles/lugares_alertsCSS").Include(
                    "~/Content/Lugares/alerts.css"
                    ));
            bundles.Add(new StyleBundle("~/bundles/sweetAlertCSS").Include(
                    "~/Content/sweetalert2.min.css"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/sweetAlertJS").Include(
                    "~/Scripts/sweetalert2.min.js"
                   ));
    }
    }
}
