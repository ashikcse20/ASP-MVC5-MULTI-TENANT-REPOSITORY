namespace AspMvc5MultiTenantProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Core;
    using System.Configuration;

    public partial class BaseModel
    {

        public ContextModel Entity;
        //public string serverName = @"ASHIKPC\MSSQLSERVERASHIK"; 
        public string serverName = ConfigurationManager.AppSettings["serverName"];
        public string databaseName = Constant.DatabaseName;
        public BaseModel()
        {
            Entity = new ContextModel("Data Source=" + serverName + ";Initial Catalog=" + databaseName + ";Integrated Security=False;User Id=sa; Password=admin@321;MultipleActiveResultSets=True;Application Name=EntityFramework");
             
        }
    }
}
