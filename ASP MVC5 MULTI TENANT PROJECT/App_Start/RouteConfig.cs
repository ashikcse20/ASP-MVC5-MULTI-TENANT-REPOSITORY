using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AspMvc5MultiTenantProject.Core;

namespace AspMvc5MultiTenantProject
{
	 public class RouteConfig
	 {
		  public static void RegisterRoutes(RouteCollection routes)
		  {
				routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
				// Comment this else the next MapRoute will not be initialize 
				//routes.MapRoute(
				//	 name: "Default2",
				//	 url: "{controller}/{action}/{id}",
				//	 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
				//);

				routes.MapRoute(
				name: "Default", url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				constraints: new { TenantRouting = new RoutingConstraint() }
					 );
		  }
	 }

	 public class RoutingConstraint : IRouteConstraint
	 { 

		  public bool Match(HttpContextBase httpContext, Route route, string getParameter, RouteValueDictionary values, RouteDirection routeDirection)
		  {
				// Got htis code from  http://blog.gaxion.com/2017/05/how-to-implement-multi-tenancy-with.html
				var GetAddress = httpContext.Request.Headers["Host"].Split('.'); 
				var tenant = GetAddress[0];
				//Here you can apply your tricks and logic. Note for when you put it in public server then www.hamdunsoft.com , www.tenant1.hamdunsoft.com then you need to change a little bit in the conditions . Because a www. was added.
				if (GetAddress.Length < 2) // See here for localhost:80 or localhost:9780 ohh also for hamdun soft  execution will enter here . But for less than 2? will hamdunsoft.com enter here?
				{
					 tenant = "This is the main domain";

					 Constant.DatabaseName = "TEST";
					 if (!values.ContainsKey("tenant"))
						  values.Add("tenant", tenant);
					 //return false;
					 // return true;
				}
				 else if (GetAddress.Length == 2) //   execution will enter here  for  hamdunsoft.com enter here but not for www.hamdunsoft.com
				{
					 tenant = "This is the main domain";

					 Constant.DatabaseName = GetAddress[0];  
					 if (!values.ContainsKey("tenant"))
						  values.Add("tenant", tenant);
					 //return false;
					 // return true;
				}
				else if (!values.ContainsKey("tenant")) // for tenant1.hamdunsoft.com execution will enter here
				{
					 values.Add("tenant", tenant);
					 Constant.DatabaseName = GetAddress[1]+"."+ tenant;
				}

				return true;
		  }
	 }
}
