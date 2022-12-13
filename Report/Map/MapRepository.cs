using Report.Auth.Databases;
using Report.Common.Entities;
using Report.Map.Entities;
using System.Data;

namespace Report.Map
{
    public class MapRepository : BaseRepository
    {

        public struct Column
        {
            public const string Hash = "HASH";
        }

        public async Task<OccurrenceHash> Create(MapCreate mapCreate)
        {
            try
            {
                CustomParams = new List<SQLParameterCustom>();
                CustomParams.Add(new SQLParameterCustom(MapDataParams.LATITUDE, mapCreate.Latitude));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.LONGITUDE, mapCreate.Longitude));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.DESCRIPTION, mapCreate.OccurrenceDescription));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.ID_OCCURRENCE_TYPE, mapCreate.IdOccurrenceType));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.CREATED, mapCreate.Created));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.UPDATED, mapCreate.Updated));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.CREATED_BY, mapCreate.CreatedBy, true));
                CustomParams.Add(new SQLParameterCustom(MapDataParams.UPDATED_BY, mapCreate.UpdatedBy, true));
                
                Database dt = new Database();
                Params = dt.CreateParameter(CustomParams);

                DataSet ds = await dt.ExecuteProcedureParamsDataSet(MapDataProcedures.SPRPT_CR_OCCURRENCE, Params);

                OccurrenceHash hash = new OccurrenceHash();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        hash = new OccurrenceHash(row);
                    }
                }

                return hash;
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

        public async Task<IList<Marker>> GetAll()
        {
            try
            {
                Database dt = new Database();

                DataSet ds = await dt.ExecuteProcedureDataSet(MapDataProcedures.SPRPT_RT_MARKERS);

                IList<Marker> markers = new List<Marker>();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Marker marker = new Marker();
                        marker.Hash = DbParse<Guid>(row, MapDataColumns.HASH);
                        marker.Description = DbParse<string>(row, MapDataColumns.DESCRIPTION);
                        marker.Latitude = DbParse<decimal>(row, MapDataColumns.LATITUDE);
                        marker.Longitude = DbParse<decimal>(row, MapDataColumns.LONGITUDE);
                        marker.IdOccurrenceType = DbParse<int>(row, MapDataColumns.ID_OCCURRENCE_TYPE);
                        markers.Add(marker);
                    }
                }
                return markers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Guid> GetUserHash(int? idUser)
        {
            Guid hash = Guid.NewGuid();

            try
            {
                CustomParams = new List<SQLParameterCustom>();
                CustomParams.Add(new SQLParameterCustom(MapDataParams.ID_USER, idUser));

                Database dt = new Database();
                Params = dt.CreateParameter(CustomParams);

                DataSet ds = await dt.ExecuteProcedureParamsDataSet(MapDataProcedures.SPRPT_RT_USER_HASH, Params);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        hash = DbParse<Guid>(row, MapDataColumns.HASH);
                    }
                }

                return hash;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

