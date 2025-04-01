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
    public class FieldOfActivityController : ControllerBase
    {
        private readonly IS_FieldOfActivity _service;

        public FieldOfActivityController(IS_FieldOfActivity service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MReq_FieldOfActivity request)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_FieldOfActivity>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _service.Create(request);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(MReq_FieldOfActivity request)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_FieldOfActivity>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _service.Update(request);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_FieldOfActivity>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _service.Delete(id);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_FieldOfActivity>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _service.GetById(id);
            return Ok(res);
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> GetListByStatus(int status)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_Post>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _service.GetListByStatus(status);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, int status, string updatedBy)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseData<MRes_FieldOfActivity>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
            var res = await _service.UpdateStatus(id, status, updatedBy);
            return Ok(res);
        }
    }
}
