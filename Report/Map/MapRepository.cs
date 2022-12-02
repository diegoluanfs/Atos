using Report.Auth.Databases;
using Report.Common.Entities;
using Report.Map.Entities;
using System.Data;

namespace Report.Map
{
    public class MapRepository : BaseRepository
    {
        public async Task<bool> Create(MapCreate mapCreate)
        {
            try
            {
                CustomParams = new List<SQLParameterCustom>();

                CustomParams.Add(new SQLParameterCustom(MapDataParams.LATITUDE, mapCreate.Latitude));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.LONGITUDE, mapCreate.Longitude));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.CREATED, mapCreate.Created));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.UPDATED, mapCreate.Updated));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.CREATED_BY, mapCreate.CreatedBy, true));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.UPDATED_BY, mapCreate.UpdatedBy, true));
                //CustomParams.Add(new SQLParameterCustom(MapDataParams.GEOLOCATION, mapCreate.Geolocation, true));

                Database dt = new Database();
                Params = dt.CreateParameter(CustomParams);

                DataSet ds = await dt.ExecuteProcedureParamsDataSet(MapDataProcedures.SPRPT_CR_OCCURRENCE, Params);

                bool resp = false;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    resp = true;
                }

                return resp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IList<Occurrence>> Search()
        {
            try
            {
                Database dt = new Database();

                DataSet ds = await dt.ExecuteProcedureDataSet(MapDataProcedures.SPRPT_MAP_SEARCH);

                IList<Occurrence> occurrences = new List<Occurrence>();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Occurrence occurrence = new Occurrence();
                        occurrence.Latitude = DbParse<decimal>(row, MapDataColumns.LATITUDE).ToString();
                        occurrence.Longitude = DbParse<decimal>(row, MapDataColumns.LONGITUDE).ToString();
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

