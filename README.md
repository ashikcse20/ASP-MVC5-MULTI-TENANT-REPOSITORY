### In the name of Allah, Most Gracious, Most Merciful بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيم 
Welcome to Hamdoon Soft Fun:
## Contact: ashikcse20@gmail.com or skype ashikcse20 or facebook.com/ashikcse20 . Feel free to contact

**At the starting of this written tutorial I am taking in mind that your familiar with C#, ASP .NET MVC,Folder Structure Of ASP .NET MVC, Routing, NuGet Package Manager, Publishing project in visual studio, IIS(Internet Information Services) managing like Application Pools, Bindings setting, SQL Server Management Studio (SSMS), SQL Data Base Server Or LocalDB, Creating ASP MVC Project with ADO Entity > Data Model Wizard > Code First From Databse . If you are not familiar these please acquire knowledge about these, that will help you a lot. ** 

This tutorial with video can be found in My Blog [ASP .NET MVC 5 Multi Tenant Example With New Project (Single Different Database For Per Tenant Using Entity Framework)](https://submitmysites.blogspot.com/2018/09/in-name-of-allah-most-gracious-most.html) OR directly in youtube [ASP .NET MVC 5 Multi Tenant Example With Basic Code (Single Database Per Tenant)](https://www.youtube.com/watch?time_continue=784&v=e5Ic8qPfQV4). These are same video.
Here I just use this ASP .Net MVC5 application as Multitenant application with single database per tenant. Next time I will do the same thing with ASP .Net core.
## Required Environement
1. I am doing it from Windows 10.
2. ASP .Net MVC5
3. Entity Framework (You can use Dapper or Normal SqlClient if you understand the main mechanism for multitenant system with single database per tenant). I have written another blog for fetching data from MSSQL database by SqlClient. You can find the link here [Using SqlClient connect to sql server and CRUD in Databse thorough Stored Procedure](https://submitmysites.blogspot.com/2018/11/using-sqlclient-connect-to-sql-server.html) Or this one [Using SqlClient connect to sql server and CRUD in Databse thorough code base Inner Query Query](https://submitmysites.blogspot.com/2018/08/using-sqlclient-connect-to-sql-server.html)
4.IIS (Internet INformation Services)
5. SQL Server Management Studio (SSMS)
6. Microsoft SQL Server Or LocalDB

###### All code can be found here https://github.com/ashikcse20/ASP-MVC5-MULTI-TENANT-REPOSITORY
## Provide me a popcorn packet with a glass of fruit juice: https://www.paypal.me/ashikcse20 

Ok, fine let start I have devide the tutorual in two section Section A(Technical Steps) and Section B(Coding Steps)

## Section A. Technical Steps: 
1. Go to C:\Windows\System32\drivers\etc open hosts file with Sublime Text or Notepad with administarator mode. find # 127.0.0.1 localhost in the file and replace it by the following instruction.
 **In hosts file a line starting with # is a comment line #The below line is an active line which make many alias of local host (remove the #) 127.0.0.1 localhost hamdoonsoft.com tenant1.hamdoonsoft.com tenant2.hamdoonsoft.com tenant3.hamdoonsoft.com**
2. Then install "IIS(Internet Information Services) > Web Management Tools> IIS Management Console" and other some features Turn Windows features on or of> If you get error afer running the project  then install others some features from IIS(Internet Information Services).
3. From IIS Create a site. Right click on site name and click *Add Application* Map your site to the **Physical path** of the published folder of the project added in this repository.
4. Add Bindings from IIS for hamdoonsoft.com tenant1.hamdoonsoft.com tenant2.hamdoonsoft.com tenant3.hamdoonsoft.com. 

###### For better understanding of Section A please see this video from first to last [ASP .NET MVC 5 Multi Tenant Example With Basic Code (Single Database Per Tenant)](https://www.youtube.com/watch?time_continue=784&v=e5Ic8qPfQV4) tutorial patiently

## Section B. Coding Steps: 
###### Fortunately after downloading this repository you don't have any coding task primarily. Just read the below information carefully.  
**1** Firstly try to understand the code in RouteConfig.Cs in App_Start folder. We need to make change in RouteConfig.cs of App_Start folder to support multiple tenant (Code is already added in this repository's RouteConfig.cs file ). Comment the default routeMap wich was created during the creation of the project by visual studio and Change RouteConfig.cs as shown in the video (already commented in this repository RouteConfig.Cs file ). I just follow steps from http://blog.gaxion.com/2017/05/how-to-implement-multi-tenancy-with.html here for configuring route

**2** Then see the code in controller: test1  Action: TenantInfo . Also visit http://localhost:9780/test1/TenantInfo after running the project by ctrl + f5. You may got this error after running the project [Could not find a part of the path … bin\roslyn\csc.exe](https://stackoverflow.com/questions/32780315/could-not-find-a-part-of-the-path-bin-roslyn-csc-exe) then just uninstall and reinstall or update the version of **Microsoft.CodeDom.Providers.DotNetCompilerPlatform** by NuGet Package Manager from References under Properties in solution explorer.
##### Keep in mind that from visual studio you can only run single tenant. To test multiple tenant described in section A you must have to host the published code in IIS server and set up the bindings.

###### Added  code in App_Start/RouteConfig.cs
N.B: You don't need to add this code it is already added in RouteConfig.cs in this repository. I am just showing the code below.
<details>
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
     </details>
  3. There are some simple tricks for selecting different database for different tenant. To see that download the code and open App_Data/ BaseModel.cs. Here I have just make a dynamic connection string based on tenant name by variable 'Constant.DatabaseName' which was declared in Core/Constant.cs file and initialize  in App_Start/RouteConfig.cs at class: RoutingConstraint function: Match. For better understanding please see this video  [ASP .NET MVC 5 Multi Tenant Example With Basic Code (Single Database Per Tenant)](https://www.youtube.com/watch?time_continue=784&v=e5Ic8qPfQV4) tutorial patiently. 
  
And for making proper connection string add the bellow code serverName in Web.config (Not the Web.config file in View folder) file at section <appSettings>.
  
       <appSettings> 
              <add key="serverName" value="ASHIKPC\MSSQLSERVER" /> 
        </appSettings>
Where ASHIKPC\MSSQLSERVER is your Data Source . See in BaseModel.cs connection string which was passed in App_Data/ContextModel.cs class constructor.
You can use here LocalDb wich is installed during the installation of Visual Studio. For this google how to make cooncestring for LocalDb in asp .Net MVC. 
Now run the project and visit hamdoonsoft.com tenant1.hamdoonsoft.com tenant2.hamdoonsoft.com tenant3.hamdoonsoft.com
© 2020 - Hamdoon Soft Application
