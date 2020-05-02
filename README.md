**In the name of Allah, Most Gracious, Most Merciful بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيم **
Welcome to Hamdoon Soft Fun:
Here I just use this ASP .Net MVC5 application as Multitenant application with single database per tenant
I am doing it from Windows 10 .
All code can be found here https://github.com/ashikcse20
A. Technical Steps:
1. Go to C:\Windows\System32\drivers\etc open hosts file with Sublime Text or Notepad with administarator mode. find # 127.0.0.1 localhost and replace it by the following instruction.
# In hosts file a line starting with # is a comment line #The below line is an active line which make many alias of local host (remove the #) 127.0.0.1 localhost hamdoonsoft.com tenant1.hamdoonsoft.com tenant2.hamdoonsoft.com tenant3.hamdoonsoft.com
2. Then install IIS from Turn Windows Features On or Off
3. From IIS Create a site and map your site to the physical publish folder of the project.
4. Add Bindings from IIS for hamdoonsoft.com tenant1.hamdoonsoft.com tenant2.hamdoonsoft.com tenant3.hamdoonsoft.com .

B. Coding Steps:
1. We need to make change in RouteConfig.Cs of App_Start folder to support multiple tenant. Comment the default routeMap and Change RouteConfig.Cs as shown in the video. I just follow steps from http://blog.gaxion.com/2017/05/how-to-implement-multi-tenancy-with.html here for configuring route
2. There are some simple tricks for selecting different database for different tenant. To see that download the code or see this tutorial patiently.
© 2020 - Hamdoon Soft Application
