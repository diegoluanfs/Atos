using Report.Common.Entities;
using System.Transactions;
using Report._Common.Entities;
using Report._Common.Repositories;
using Report._Common;

namespace Report.Utility
{
    public class UtilityBusiness
    {
        const string RegexName = @"^[a-z ]+$";
        const string RegexEmail = @"/[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/gm";
        const string RegexPassword = @"/(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$/gm";

        private IKeyManager _keyManager;

        public UtilityBusiness(IKeyManager keyManager)
        {
            _keyManager = keyManager;
        }

        public UtilityBusiness()
        {

        }

        public async Task<IList<OccurrenceType>> Search(string _remoteIP)
        {
            try
            {
                #region default user verification
                //Check who is requesting

                //Check user permission

                #endregion

                #region verify fields

                #endregion
                IList<OccurrenceType> occurrences = new List<OccurrenceType>();

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //Check if user exists
                    UtilityRepository mapsRepository = new UtilityRepository();
                    occurrences = await mapsRepository.Search();
                    //Register Utility and Create Utilityorization Key

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
