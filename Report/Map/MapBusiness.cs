using Report._Common;
using Report.Common.Entities;
using System.Transactions;
using Report.Map.Entities;

namespace Report.Map
{
    public class MapBusiness
    {
        const string RegexName = @"^[a-z ]+$";
        const string RegexEmail = @"/[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/gm";
        const string RegexPassword = @"/(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$/gm";

        private IKeyManager _keyManager;

        public MapBusiness(IKeyManager keyManager)
        {
            _keyManager = keyManager;
        }


        public MapBusiness()
        {

        }

        public async Task<OccurrenceHash> Create(int? _currentUser, Occurrence occurrence)
        {
            try
            {
                #region default user verification
                //Check who is requesting

                //Check user permission

                #endregion

                #region verify fields

                occurrence.Latitude = occurrence.Latitude.Trim();
                occurrence.Longitude = occurrence.Longitude.Trim();

                #endregion

                MapCreate mapCreate = new MapCreate();
                mapCreate.Latitude = decimal.Parse(occurrence.Latitude);
                mapCreate.Longitude = decimal.Parse(occurrence.Longitude);
                mapCreate.Created = DateTime.Now;
                mapCreate.Updated = DateTime.Now;
                mapCreate.OccurrenceDescription = occurrence.OccurrenceDescription;
                mapCreate.IdOccurrenceType = occurrence.OccurrenceType;

                if (_currentUser != 0)
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        //Check if user exists
                        MapRepository mapsRepository = new MapRepository();
                        mapCreate.HashUser = await mapsRepository.GetUserHash(_currentUser);
                        mapCreate.CreatedBy = _currentUser;
                        mapCreate.UpdatedBy = _currentUser;
                        transactionScope.Complete();
                    }
                }
                else
                {
                    mapCreate.CreatedBy = null;
                    mapCreate.UpdatedBy = null;
                    mapCreate.HashUser = Guid.NewGuid();
                }

                OccurrenceHash resp = new OccurrenceHash();

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    MapRepository mapsRepository = new MapRepository();
                    resp = await mapsRepository.Create(mapCreate);

                    //with two hash is possible add in table user who reported

                    transactionScope.Complete();
                }

                return resp;
            }
            catch (BusinessException eb)
            {
                throw eb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IList<Occurrence>> Search(string _remoteIP)
        {
            try
            {
                #region default user verification
                //Check who is requesting

                //Check user permission

                #endregion

                #region verify fields

                #endregion
                IList<Occurrence> occurrences = new List<Occurrence>();

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //Check if user exists
                    MapRepository mapsRepository = new MapRepository();
                    occurrences = await mapsRepository.Search();
                    //Register Map and Create Maporization Key

                    transactionScope.Complete();
                }

                return occurrences;
            }
            catch (BusinessException eb)
            {
                throw eb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<Marker>> GetAll(string _remoteIP)
        {
            try
            {
                #region default user verification
                //Check who is requesting

                //Check user permission

                #endregion

                #region verify fields

                #endregion
                IList<Marker> occurrences = new List<Marker>();

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //Check if user exists
                    MapRepository mapsRepository = new MapRepository();
                    occurrences = await mapsRepository.GetAll();
                    //Register Map and Create Maporization Key

                    transactionScope.Complete();
                }

                return occurrences;
            }
            catch (BusinessException eb)
            {
                throw eb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
