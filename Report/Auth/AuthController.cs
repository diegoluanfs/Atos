using Report.Common.Entities;
using Report.Auth.Entities;
using Microsoft.AspNetCore.Mvc;
using Report._Common;
using Microsoft.AspNetCore.Http.Features;

namespace Report.Auth
{
    public class AuthController : Controller
    {

        private string _version;
        private string _remoteIP;
        private IKeyManager _keyManager;

        public AuthController(IHttpContextAccessor httpContextAccessor, IKeyManager keyManager)
        {
            _remoteIP = httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
            _keyManager = keyManager;
        }

        [Route("auth/signup")]
        [HttpPost]
        //[EnableCors("MyPolicy")]
        public async Task<IActionResult> SignUp(SignUpReq signUpReq)
        {
            try
            {
                AuthBusiness userBusiness = new AuthBusiness();
                var resp = await userBusiness.SignUp(signUpReq);

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

        [Route("auth/signin")]
        [HttpPost]
        ////[EnableCors("MyPolicy")]
        public async Task<IActionResult> SignIn([FromBody] SignInReq signInReq)
        {
            try
            {
                var data = new SignInResp();
                AuthBusiness userBusiness = new AuthBusiness(_keyManager);
                var resp = await userBusiness.SignIn(signInReq, _remoteIP);

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

        [Route("auth/forgotpassword")]
        [HttpPost]
        //[EnableCors("MyPolicy")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordReq forgotPasswordReq)
        {
            try
            {
                AuthBusiness usersBusiness = new AuthBusiness();
                var resp = await usersBusiness.ForgotPassword(forgotPasswordReq);

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

        [Route("auth/activate")]
        [HttpPost]
        //[EnableCors("MyPolicy")]
        public async Task<IActionResult> Activate(ActivateReq activateReq)
        {
            try
            {
                AuthBusiness usersBusiness = new AuthBusiness();
                var resp = await usersBusiness.Activate(activateReq);

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

        [Route("auth/logout")]
        [HttpPost]
        //[EnableCors("MyPolicy")]
        public async Task<IActionResult> Logout()
        {
            try
            {

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

    }
}
