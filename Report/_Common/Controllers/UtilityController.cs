using Report.Common.Entities;
using Report._Common;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Report.Utility
{
    public class UtilityController : Controller
    {

        private string _version;
        private string _remoteIP;
        private IKeyManager _keyManager;

        public UtilityController(IHttpContextAccessor httpContextAccessor, IKeyManager keyManager)
        {
            _remoteIP = httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
            _keyManager = keyManager;
        }

        [Route("[controller]/getoccurrences")]
        [HttpGet]
        ////[EnableCors("MyPolicy")]
        public async Task<IActionResult> Search()
        {
            try
            {
                UtilityBusiness userBusiness = new UtilityBusiness(_keyManager);
                var resp = await userBusiness.Search(_remoteIP);

                return StatusCode(201, new Return() { Data = resp });
            }
            catch (BusinessException eb)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = eb.ToString() });
                return StatusCode(400, new Return() { Data = null, Message = eb.Message, ErrorCode = eb.HResult.ToString() });
            }
            catch (Exception ex)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = ex.ToString() });
                return StatusCode(500, new Return() { Data = null, Message = "Internal Server Error!", ErrorCode = "log service number" });
            }
        }

    }
}
