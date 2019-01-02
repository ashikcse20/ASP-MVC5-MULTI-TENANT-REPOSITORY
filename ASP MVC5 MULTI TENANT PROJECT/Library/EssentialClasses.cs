using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AspMvc5MultiTenantProject.Library
{
    public class ResultModel
    {
        public int Id { get; set; }
        public int returnedValueFromQuery { get; set; }
        public string Message { get; set; }
        public bool Result { get; set; }
        public DataTable DtTable { get; set; }
        public ArrayList MyDataList { get; set; }
    }
    public class QUERY2
    {
        public string SQL { get; set; }
        public Dictionary<string, object> PARAMETER { get; set; }
        public Dictionary<string, byte[]> BYTEPARAMETER { get; set; }
    }
}