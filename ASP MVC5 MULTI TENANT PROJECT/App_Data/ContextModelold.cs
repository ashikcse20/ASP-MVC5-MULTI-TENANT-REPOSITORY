namespace AspMvc5MultiTenantProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=ModelDesktop")
        {
        }
		  public ContextModel(string conn) 
: base(conn)
        {
		  }
		  public virtual DbSet<TBL_TEST_1> TBL_TEST_1 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
