using Report.Common.Entities;
using Report._Common;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Report.Map.Entities;

namespace Report.Map
{
    public class MapController : Controller
    {

        private string _key;
        private string _version;
        private string _remoteIP;
        private int _currentUser;
        private IKeyManager _keyManager;

        public MapController(IHttpContextAccessor httpContextAccessor, IKeyManager keyManager)
        {
            _remoteIP = httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
            _key = httpContextAccessor.HttpContext.Request.Headers["ApiKey"];
            _keyManager = keyManager;
        }


        [Route("map/create")]
        [HttpPost]
        //[EnableCors("MyPolicy")]
        public async Task<IActionResult> Create([FromBody] Occurrence occurrence)
        {
            try
            {
                //_currentUser = await _keyManager.ValidateKey(_key);

                MapBusiness userBusiness = new MapBusiness();
                OccurrenceHash resp = await userBusiness.Create(_currentUser, occurrence);

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

        [Route("map/search")]
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

        [Route("map/getall")]
        [HttpGet]
        ////[EnableCors("MyPolicy")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                MapBusiness userBusiness = new MapBusiness(_keyManager);
                var resp = await userBusiness.GetAll(_remoteIP);

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
