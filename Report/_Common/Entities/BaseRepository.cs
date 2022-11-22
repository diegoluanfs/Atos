using System.Data;
using System.Data.SqlClient;

namespace report.Common.Entities
{
    public class BaseRepository
    {
        internal IList<SqlParameter> Params { get; set; }
        internal IList<SQLParameterCustom> CustomParams { get; set; }

        internal T DbParse<T>(DataRow dr, string columnName)
        {
            if (dr.Table.Columns.Contains(columnName) && !string.IsNullOrEmpty(dr[columnName].ToString()))
            {
                return (T)Convert.ChangeType(dr[columnName], typeof(T));
            }
            else
            {
                return default;
            }
        }
    }
}
