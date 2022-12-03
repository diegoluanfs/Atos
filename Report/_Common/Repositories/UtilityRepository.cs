using Report.Common.Entities;
using System.Data;
using Report.Common.Data.Databases;

namespace Report._Common.Repositories
{
    public class UtilityRepository : BaseRepository
    {
        public async Task<IList<OccurrenceType>> Search()
        {
            try
            {
                Database dt = new Database();

                DataSet ds = await dt.ExecuteProcedureDataSet(UtilityDataProcedures.SPRPT_RT_OCCURRENCE_TYPE);

                IList<OccurrenceType> occurrences = new List<OccurrenceType>();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        OccurrenceType occurrence = new OccurrenceType();
                        occurrence.Id = DbParse<int>(row, UtilityDataColumns.ID);
                        occurrence.Name = DbParse<string>(row, UtilityDataColumns.NAME).Trim();
                        occurrences.Add(occurrence);
                    }
                }
                return occurrences;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

