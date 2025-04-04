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
    public class NewsController : ControllerBase
    {
        private readonly IS_News _news_service;

        public NewsController(IS_News news_service)
        { 
            _news_service = news_service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MReq_News request)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_News>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _news_service.Create(request);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MReq_News request)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_News>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _news_service.Update(request);
            return Ok(res);
        }

        [HttpPut("update-status/{news_id}")]
        public async Task<IActionResult> UpdateStatus(int news_id, bool status, string updatedBy)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_News>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _news_service.UpdateStatus(news_id, status, updatedBy);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_News>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _news_service.Delete(id);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_News>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _news_service.GetById(id);
            return Ok(res);
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> GetListByStatus(bool status)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_News>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _news_service.GetListByStatus(status);
            return Ok(res);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddAvatarPost(IFormFile file, int news_id)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_News>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _news_service.AddAvatarNews(file, news_id);
            return Ok(res);
        }
    }
}
