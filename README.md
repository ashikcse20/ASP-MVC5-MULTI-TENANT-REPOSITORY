### In the name of Allah, Most Gracious, Most Merciful بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيم 
Welcome to Hamdoon Soft Fun:
This tutorial with video can be found in [ASP .NET MVC 5 Multi Tenant Example With New Project (Single Different Database For Per Tenant Using Entity Framework)](https://submitmysites.blogspot.com/2018/09/in-name-of-allah-most-gracious-most.html)
## Provide me a popcorn packet with a cup of coffee: https://www.paypal.me/ashikcse20 
Here I just use this ASP .Net MVC5 application as Multitenant application with single database per tenant
I am doing it from Windows 10 .
All code can be found here https://github.com/ashikcse20
## A. Technical Steps:
1. Go to C:\Windows\System32\drivers\etc open hosts file with Sublime Text or Notepad with administarator mode. find # 127.0.0.1 localhost and replace it by the following instruction.
 ** In hosts file a line starting with # is a comment line #The below line is an active line which make many alias of local host (remove the #) 127.0.0.1 localhost hamdoonsoft.com tenant1.hamdoonsoft.com tenant2.hamdoonsoft.com tenant3.hamdoonsoft.com **
2. Then install IIS from Turn Windows Features On or Off
3. From IIS Create a site and map your site to the physical publish folder of the project.
4. Add Bindings from IIS for hamdoonsoft.com tenant1.hamdoonsoft.com tenant2.hamdoonsoft.com tenant3.hamdoonsoft.com .

## B. Coding Steps:
1. We need to make change in RouteConfig.Cs of App_Start folder to support multiple tenant. Comment the default routeMap and Change RouteConfig.Cs as shown in the video. I just follow steps from http://blog.gaxion.com/2017/05/how-to-implement-multi-tenancy-with.html here for configuring route
Add some code in App_Start/RouteConfig.cs







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

	   
       
       
       public class RoutingConstraint : IRouteConstraint // It is main Class for Multi teanant
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
     
2. There are some simple tricks for selecting different database for different tenant. To see that download the code or see the video [ASP .NET MVC 5 Multi Tenant Example With Basic Code (Single Database Per Tenant)] (https://www.youtube.com/watch?time_continue=784&v=e5Ic8qPfQV4) tutorial patiently.
One thing you have to add App_Data/BaseModel.cs for this tricks.
And add the bellow code serverName in Web.config file at section <appSettings>
  
       <appSettings> 
              <add key="serverName" value="ASHIKPC\MSSQLSERVERASHIK" /> 
        </appSettings>
  
© 2020 - Hamdoon Soft Application
