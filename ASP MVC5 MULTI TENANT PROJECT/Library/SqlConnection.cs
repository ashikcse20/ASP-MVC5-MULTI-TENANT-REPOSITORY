using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspMvc5MultiTenantProject.Library
{
    public class SqlConnections 
    {
        public ResultModel ExecuteSqlStoredProcedureSaveEdit(QUERY2 query, string getOrSaveEdit, out string ERROR)
        {
            ResultModel result = new ResultModel();
            SqlConnection con = getConnection253_Mymun();
            con.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = query.SQL;
                SqlParameter param = new SqlParameter();
                //param = command.Parameters.Add("@country", SqlDbType.VarChar, 50);
                param.Direction = ParameterDirection.InputOutput;
                //param.Value = "UK";


                if (query.PARAMETER != null)
                {
                    foreach (KeyValuePair<string, object> data in query.PARAMETER)
                    {
                        if (data.Key == "@Message")
                        {
                            command.Parameters.Add(data.Key, SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;
                        }
                        else if (data.Key == "@Result")
                        {
                            command.Parameters.Add(data.Key, SqlDbType.Bit, 1).Direction = ParameterDirection.Output;
                        }
                        else if (data.Key == "@ID")
                        {
                            command.Parameters.Add(data.Key, SqlDbType.BigInt, 1).Direction = ParameterDirection.Output;
                        }
                        else
                        {
                            if (data.Value != null)
                                command.Parameters.AddWithValue(data.Key, data.Value);
                            else
                                command.Parameters.AddWithValue(data.Key, DBNull.Value);
                        }
                    }
                }

                if (getOrSaveEdit == "saveedit")
                {
                    result.returnedValueFromQuery = command.ExecuteNonQuery();
                    if (command.Parameters.Contains("@ID"))
                    {
                        string s = command.Parameters["@ID"].Value.ToString();
                        int ID = 0;
                        int.TryParse(command.Parameters["@ID"].Value.ToString(), out ID);
                        result.Id = ID;
                    }
                    result.Message = command.Parameters["@Message"].Value.ToString();
                    result.Result = Convert.ToBoolean(command.Parameters["@Result"].Value.ToString());
                }
                else if (getOrSaveEdit == "get")
                {
                    SqlDataReader dr = command.ExecuteReader();
                    ERROR = String.Empty;

                    ArrayList myArryList = new ArrayList();
                    //dr.Read(); 


                    var dt = new DataTable();
                    bool FlagDataRead = false;
                    while (dr.Read())
                    {
                        FlagDataRead = true;
                        dt.Load(dr);
                        myArryList.Add(dt);
                        result.DtTable = dt;
                        result.Result = Convert.ToBoolean(command.Parameters["@Result"].Value.ToString());
                        result.Message = command.Parameters["@Message"].Value.ToString();
                        if (!dr.IsClosed)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (!FlagDataRead)
                    {

                        dr.NextResult();
                        result.Result = Convert.ToBoolean(command.Parameters["@Result"].Value.ToString());
                        result.Message = command.Parameters["@Message"].Value.ToString();
                    }

                }


                ERROR = String.Empty;

            }
            catch (SqlException ex)
            {
                ERROR = ex.Message.ToString();
                result.Message = ex.Message.ToString();
                result.Result = false;
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public SqlConnection getConnection253_Mymun()
        {
            //string connStr = @"data source=(local);database=DBL_Group;uid=sa;password=dblgroup";
            //string connStr = @"data source=(local);database=ProjectInfoDB;Connection Timeout=3000;Integrated Security=True";
            //string connStr = @"data source=192.168.153.208;database=ProjectInfoDB;uid=sa;password=dbl1234";
            //string connStr = @"data source=MISDEVELOPER\SQL14;database=ProjectInfoDB;uid=sa;password=logon";
            string connStr = @"data source=192.168.13.253;database=ProjectInfoDB;uid=sa;password=DbL123"; // live server
            SqlConnection conn = new SqlConnection(connStr);

            return conn;
        }
    }
    
}