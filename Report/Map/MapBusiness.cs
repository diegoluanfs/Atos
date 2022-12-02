using report._Common;
using report.Common.Entities;
using System.Transactions;
using Report.Map.Entities;

namespace report.Map
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

        public async Task<bool> Create(Occurrence occurrence)
        {
            try
            {
                #region default user verification
                //Check who is requesting

                //Check user permission

                #endregion

                #region verify fields

                #endregion

                MapCreate mapCreate = new MapCreate();
                mapCreate.Created = DateTime.Now;
                mapCreate.Updated = DateTime.Now;

                bool resp = false;

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //Check if user exists
                    MapRepository mapsRepository = new MapRepository();
                    resp = await mapsRepository.Create(mapCreate);
                    //Register Map and Create Maporization Key

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

    }
}
