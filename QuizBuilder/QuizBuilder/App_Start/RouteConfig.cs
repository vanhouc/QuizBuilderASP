using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuizBuilder
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "QuizBuilder", action = "Default", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "UserQuizzes",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "QuizBuilder", action = "UserQuizzes", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "UserResults",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "QuizBuilder", action = "UserResults", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AdminHome",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "QuizBuilder", action = "AdminHome", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AdminUsers",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "QuizBuilder", action = "AdminUsers", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "UserHome",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "QuizBuilder", action = "UserHome", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Register",
                url: "{controller}/{action}",
                defaults: new { controller = "QuizBuilder", action = "Register" }
            );
        }
    }
}
