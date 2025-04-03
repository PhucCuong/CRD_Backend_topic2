using API_Sample.Application.Services;
using API_Sample.Models.Common;
using API_Sample.Models.Request;
using API_Sample.Models.Response;
using API_Sample.WebApi.Lib;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace API_Sample.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IS_Post _s_post;

        public PostController(IS_Post post_service)
        {
            _s_post = post_service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MReq_Post request)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Post>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_post.Create(request);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(MReq_Post request)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Post>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_post.Update(request);
            return Ok(res);
        }

        [HttpPut("update-status/{post_id}")]
        public async Task<IActionResult> UpdateStatus(int post_id, bool status, string updatedBy)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Post>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_post.UpdateStatus(post_id, status, updatedBy);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Post>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_post.Delete(id);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Post>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_post.GetById(id);
            return Ok(res);
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> GetListByStatus(bool status)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Post>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_post.GetListByStatus(status);
            return Ok(res);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddAvatarPost(IFormFile file, int post_id)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Post>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _s_post.AddAvatarPost(file, post_id);
            return Ok(res);
        }
    }
}
