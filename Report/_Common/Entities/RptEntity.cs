using System.Data;

namespace Report.Common.Entities
{
    public class RptEntity
    {
        public T DbParse<T>(DataRow dr, string columnName)
        {
            if (dr.Table.Columns.Contains(columnName) && !string.IsNullOrEmpty(dr[columnName].ToString()))
            {
                return (T)Convert.ChangeType(dr[columnName], typeof(T));
            }
            else
            {
                return default(T);
            }
        }
        public int DbParse(DataRow dr, string[] columnName)
        {
            foreach (var item in columnName)
            {
                if (dr.Table.Columns.Contains(item) && !string.IsNullOrEmpty(dr[item].ToString()))
                {
                    return Convert.ToInt32(dr[item]);
                }
            }

            return 0;
        }
    }
}
