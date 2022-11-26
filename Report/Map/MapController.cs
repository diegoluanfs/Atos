using report.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using report._Common;
using Microsoft.AspNetCore.Http.Features;

namespace report.Map
{
    public class MapController : Controller
    {

        private string _version;
        private string _remoteIP;
        private IKeyManager _keyManager;

        public MapController(IHttpContextAccessor httpContextAccessor, IKeyManager keyManager)
        {
            _remoteIP = httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
            _keyManager = keyManager;
        }

        [Route("map")]
        [HttpPost]
        //[EnableCors("MyPolicy")]
        public async Task<IActionResult> Create(string latitude, string longitude)
        {
            try
            {
                MapBusiness userBusiness = new MapBusiness();
                var resp = await userBusiness.Create(latitude, longitude);

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

        [Route("map")]
        [HttpGet]
        ////[EnableCors("MyPolicy")]
        public async Task<IActionResult> Search()
        {
            try
            {
                MapBusiness userBusiness = new MapBusiness(_keyManager);
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
