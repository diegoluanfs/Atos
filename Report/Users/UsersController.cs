using Report._Common;
using Report.Common.Entities;
using Report.Users.Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Report.Users
{
    [ApiController]
    public class UsersController : Controller
    {
        private string _key;
        private string _version;
        private string _remoteIP;
        private int _currentUser;
        private IKeyManager _keyManager;

        public UsersController(IHttpContextAccessor httpContextAccessor, IKeyManager keyManager)
        {
            _remoteIP = httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
            _key = httpContextAccessor.HttpContext.Request.Headers["ApiKey"];
            _keyManager = keyManager;
        }

        [Route("users/changepassword")]
        [HttpPut]
        //[EnableCors("MyPolicy")]
        public async Task<IActionResult> ChangePassword(ChangePasswordReq changePasswordReq)
        {
            try
            {
                _currentUser = await _keyManager.ValidateKey(_key);

                UsersBusiness userBusiness = new UsersBusiness();
                var resp = await userBusiness.ChangePassword(_currentUser, changePasswordReq);

                return StatusCode(204);
            }
            catch (BusinessException eb)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = eb.ToString() });
                return StatusCode(400, new Return() { Data = null, Message = eb.Message, ErrorCode = eb.HResult.ToString() });
            }
            catch (SecurityException es)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = es.ToString() });
                return StatusCode(401, new Return() { Data = null, Message = es.Message, ErrorCode = es.HResult.ToString() });
            }
            catch (Exception ex)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = ex.ToString() });
                return StatusCode(500, new Return() { Data = null, Message = "Internal Server Error!", ErrorCode = "log service number" });
            }
        }

        [Route("users/update")]
        [HttpPut]
        //[EnableCors("MyPolicy")]
        public async Task<IActionResult> Update(UpdateReq updateReq)
        {
            try
            {
                _currentUser = await _keyManager.ValidateKey(_key);

                UsersBusiness usersBusiness = new UsersBusiness();
                var resp = await usersBusiness.Update(_currentUser, updateReq);

                return StatusCode(200, new Return() { Data = resp });
            }
            catch (BusinessException eb)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = eb.ToString() });
                return StatusCode(400, new Return() { Data = null, Message = eb.Message, ErrorCode = eb.HResult.ToString() });
            }
            catch (SecurityException es)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = es.ToString() });
                return StatusCode(401, new Return() { Data = null, Message = es.Message, ErrorCode = es.HResult.ToString() });
            }
            catch (Exception ex)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = ex.ToString() });
                return StatusCode(500, new Return() { Data = null, Message = "Internal Server Error!", ErrorCode = "log service number" });
            }
        }

        [Route("users/create")]
        [HttpPost]
        //[EnableCors("MyPolicy")]
        public async Task<IActionResult> Create(CreateReq createReq)
        {
            try
            {
                UsersBusiness usersBusiness = new UsersBusiness();
                var resp = await usersBusiness.Create(createReq);

                return StatusCode(200, new Return() { Data = resp });
            }
            catch (BusinessException eb)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = eb.ToString() });
                return StatusCode(400, new Return() { Data = null, Message = eb.Message, ErrorCode = eb.HResult.ToString() });
            }
            catch (SecurityException es)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = es.ToString() });
                return StatusCode(401, new Return() { Data = null, Message = es.Message, ErrorCode = es.HResult.ToString() });
            }
            catch (Exception ex)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = ex.ToString() });
                return StatusCode(500, new Return() { Data = null, Message = ex.Message, ErrorCode = "log service number" });
            }
        }

        [Route("users")]
        [HttpDelete]
        //[EnableCors("MyPolicy")]
        public async Task<IActionResult> Cancel()
        {
            try
            {
                _currentUser = await _keyManager.ValidateKey(_key);

                UsersBusiness usersBusiness = new UsersBusiness();
                var resp = usersBusiness.Cancel();

                return StatusCode(201, new Return() { Data = resp });
            }
            catch (BusinessException eb)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = eb.ToString() });
                return StatusCode(400, new Return() { Data = null, Message = eb.Message, ErrorCode = eb.HResult.ToString() });
            }
            catch (SecurityException es)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = es.ToString() });
                return StatusCode(401, new Return() { Data = null, Message = es.Message, ErrorCode = es.HResult.ToString() });
            }
            catch (Exception ex)
            {
                //await new LogService().CreatePurchase(new LogObject() { Created = DateTime.Now, IP = _remoteIP, Object = JsonConvert.SerializeObject(helpbuy), User = _currentUser == null ? 0 : _currentUser.IdUser, Information = ex.ToString() });
                return StatusCode(500, new Return() { Data = null, Message = "Internal Server Error!", ErrorCode = "log service number" });
            }
        }

    }
}
