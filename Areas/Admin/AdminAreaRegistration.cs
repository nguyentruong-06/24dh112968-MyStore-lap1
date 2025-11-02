using System.Web.Mvc;

namespace _24dh112968_MyStore_lap1.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "_24dh112968_MyStore_lap1.Areas.Admin.Controllers" } //  
            );
        }
    }
}
