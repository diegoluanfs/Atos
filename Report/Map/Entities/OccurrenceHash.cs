using System.Data;
using Report.Common.Entities;
using static Report.Map.MapRepository;

namespace Report.Map.Entities
{
    public class OccurrenceHash
    {
        public OccurrenceHash(DataRow dr)
        {
            Hash = DbParse<Guid>(dr, Column.Hash);
        }
        public OccurrenceHash(){ }

        public Guid Hash { get; set; }

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
    }
}
