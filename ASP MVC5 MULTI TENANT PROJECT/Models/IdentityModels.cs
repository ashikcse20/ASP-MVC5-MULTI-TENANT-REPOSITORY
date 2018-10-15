using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AspMvc5MultiTenantProject.Core;

namespace AspMvc5MultiTenantProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string TestColumn { get; internal set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnectoin", throwIfV1Schema: false)
        {
        }
        public ApplicationDbContext(string ConStr)
                : base(ConStr)
        {
        }
        public static ApplicationDbContext Create()
        {
            string serverName = @"ASHIKPC\MSSQLSERVERASHIK";
            string databaseName = Constant.DatabaseName;
            string connectionString = "Data Source=" + serverName + ";Initial Catalog=" + databaseName + ";Integrated Security=False;User Id=sa; Password=admin@321;MultipleActiveResultSets=True;Application Name=EntityFramework";
            if (string.IsNullOrEmpty(databaseName))
                return new ApplicationDbContext();
            else
            {
                return new ApplicationDbContext(connectionString);
            }
        }
    }

}