namespace AspMvc5MultiTenantProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_TEST_1
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
