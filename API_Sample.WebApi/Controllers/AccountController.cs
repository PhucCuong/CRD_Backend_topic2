using API_Sample.Application.Services;
using API_Sample.Models.Common;
using API_Sample.Models.Request;
using API_Sample.Models.Response;
using API_Sample.WebApi.Lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Sample.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IS_Account _s_account;

        public AccountController (IS_Account account)
        {
            _s_account = account;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MReq_Account request) 
        {
            Console.WriteLine($"Received: Username={request?.Username}, PassWord={request?.PassWord}");
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Account>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_account.Create(request);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Login(MReq_Account request)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Account>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_account.Login(request);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(MReq_Account request)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Account>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_account.Update(request);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string user_name)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Account>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_account.Delete(user_name);
            return Ok(res);
        }

        [HttpPut("change-avatar")]
        public async Task<IActionResult> ChangeAvatar(IFormFile file, string user_name)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Account>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_account.ChangeAvatar(file, user_name);
            return Ok(res);
        }
    }
}
