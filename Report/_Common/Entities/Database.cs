using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Report.Common.Entities
{

    /// <summary>
    /// 
    /// </summary>
    public class Database
    {
        public IList<SqlParameter> CreateParameter(IList<SQLParameterCustom> mappedParam)
        {

            List<SqlParameter> paramList = new List<SqlParameter>();

            foreach (var item in mappedParam)
            {
                SqlParameter parameter = new SqlParameter();
                parameter.Direction = ParameterDirection.Input;
                parameter.ParameterName = item.ParameterName;

                if (item.ParameterValue == null)
                {
                    parameter.Value = DBNull.Value;
                }
                else if (item.ParameterValue.GetType() == typeof(int?))
                {
                    parameter.DbType = DbType.Int32;

                    parameter.Value = item.ParameterValue;
                }
                else if (item.ParameterValue.GetType() == typeof(int))
                {
                    parameter.DbType = DbType.Int32;
                    if (!item.AcceptZero)
                    {
                        parameter.Value = (int)item.ParameterValue > 0 ? item.ParameterValue : null;
                    }
                    else
                    {
                        parameter.Value = (int)item.ParameterValue;
                    }
                }
                else if (item.ParameterValue.GetType() == typeof(int[]))
                {
                    if (item.ParameterValue.ToString().Length > 0)
                    {
                        parameter.DbType = DbType.String;
                        parameter.Value = string.Join(',', (int[])item.ParameterValue);
                    }
                    else
                    {
                        parameter.DbType = DbType.String;
                        parameter.Value = DBNull.Value;
                    }
                }

                else if (item.ParameterValue.GetType() == typeof(bool?))
                {
                    parameter.DbType = DbType.Boolean;
                    parameter.Value = ((bool?)item.ParameterValue).HasValue ? item.ParameterValue : null;

                }
                else if (item.ParameterValue.GetType() == typeof(bool))
                {
                    parameter.DbType = DbType.Boolean;
                    parameter.Value = item.ParameterValue;

                }
                else if (item.ParameterValue.GetType() == typeof(DateTime?))
                {
                    parameter.DbType = DbType.DateTime;
                    parameter.Value = ((DateTime?)item.ParameterValue).HasValue ? item.ParameterValue : null;

                }
                else if (item.ParameterValue.GetType() == typeof(DateTime))
                {
                    parameter.DbType = DbType.DateTime;
                    parameter.Value = (DateTime)item.ParameterValue != DateTime.MinValue ? item.ParameterValue : null;

                }

                else if (item.ParameterValue.GetType() == typeof(string))
                {
                    if (item.ParameterValue.ToString().Length > 0)
                    {
                        parameter.DbType = DbType.String;
                        parameter.Value = item.ParameterValue;
                    }
                    else
                    {
                        parameter.DbType = DbType.String;
                        parameter.Value = DBNull.Value;

                    }

                }

                else if (item.ParameterValue.GetType() == typeof(decimal))
                {
                    parameter.DbType = DbType.Decimal;

                    if (!item.AcceptZero)
                    {

                        parameter.Value = (decimal)item.ParameterValue;// > 0 ? item.ParameterValue : null;
                    }
                    else
                    {

                        parameter.Value = (decimal)item.ParameterValue;

                    }
                }
                else if (item.ParameterValue.GetType() == typeof(long))
                {
                    parameter.DbType = DbType.Int64;
                    parameter.Value = (long)item.ParameterValue > 0 ? item.ParameterValue : null;

                }
                else if (item.ParameterValue.GetType() == typeof(byte[]))
                {
                    parameter.DbType = DbType.Binary;
                    parameter.Value = item.ParameterValue;

                }
                else if (item.ParameterValue.GetType() == typeof(Guid))
                {
                    parameter.DbType = DbType.Guid;
                    parameter.Value = item.ParameterValue;

                }
                else
                {
                    parameter.Value = DBNull.Value;
                }

                paramList.Add(parameter);
            }

            return paramList;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IConfiguration Configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {

            string conexaoString = "Data Source=(localdb)\\MSSQLLocalDB;" +
                "Initial Catalog=report;" +
                "User ID=adm_report;" +
                "password=adm_report;" +
                "language=Portuguese";
            return conexaoString;
        }

        public async Task<bool> ExecuteProcedure(string procedureName)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                using (SqlConnection cnn = new SqlConnection(GetConnectionString()))
                {

                    cmd.Connection = cnn;
                    cmd.CommandText = procedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    await cmd.Connection.OpenAsync();
                    int ret = cmd.ExecuteNonQueryAsync().Result;
                    return ret > 0;
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Ocorreu um erro no banco de dados: " + ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }


        }
        public async Task<bool> ExecuteProcedureParams(string procedureName, IList<SqlParameter> parameters)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                using (SqlConnection cnn = new SqlConnection(GetConnectionString()))
                {

                    cmd.Connection = cnn;
                    cmd.CommandText = procedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters.ToArray());

                    await cmd.Connection.OpenAsync();
                    int ret = cmd.ExecuteNonQueryAsync().Result;
                    return ret > 0;
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Ocorreu um erro no banco de dados: " + ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }


        }
        public async Task<DataSet> ExecuteProcedureDataSet(string procedureName)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet dataSet = new DataSet();

            try
            {
                using (SqlConnection cnn = new SqlConnection(GetConnectionString()))
                {

                    cmd.Connection = cnn;
                    cmd.CommandText = procedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    await cmd.Connection.OpenAsync();
                    var adapter = new SqlDataAdapter(cmd);

                    adapter.Fill(dataSet);

                }

                return dataSet;
            }
            catch (SqlException ex)
            {
                cmd.Connection.Close();
                throw new Exception("Ocorreu um erro no banco de dados: " + ex.Message);
            }
            finally
            {

                cmd.Connection.Close();
            }
        }
        public async Task<DataSet> ExecuteProcedureParamsDataSet(string procedureName, IList<SqlParameter> parameterList)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet dataSet = new DataSet();

            try
            {
                using (SqlConnection cnn = new SqlConnection(GetConnectionString()))
                {

                    cmd.Connection = cnn;
                    cmd.CommandText = procedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameterList.ToArray());
                    await cmd.Connection.OpenAsync();
                    var adapter = new SqlDataAdapter(cmd);

                    adapter.Fill(dataSet);
                }

                return dataSet;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw new Exception("Ocorreu um erro no banco de dados: " + ex.Message);
            }
            finally
            {

                cmd.Connection.Close();

            }
        }

    }
}

