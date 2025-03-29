//using API_Sample.Application.Services;
//using API_Sample.Models.Common;
//using API_Sample.Models.Request;
//using API_Sample.Models.Response;
//using API_Sample.WebApi.Lib;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace API_Sample.WebApi.Controllers
//{
//    [ApiController]
//    [Route("[controller]/[action]")]
//    [Authorize]
//    public class ProductController : ControllerBase
//    {
//        private readonly IS_Product _s_Product;

//        public ProductController(IS_Product Product)
//        {
//            _s_Product = Product;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(MReq_Product request)
//        {
//            if (!ModelState.IsValid)
//                return Ok(new ResponseData<MRes_Product>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
//            var res = await _s_Product.Create(request);
//            return Ok(res);
//        }

//        [HttpPut]
//        public async Task<IActionResult> Update(MReq_Product request)
//        {
//            if (!ModelState.IsValid)
//                return Ok(new ResponseData<MRes_Product>(0, 400, DataAnnotationExtensionMethod.GetErrorMessage(ModelState)));
//            var res = await _s_Product.Update(request);
//            return Ok(res);
//        }

//        [HttpPut]
//        public async Task<IActionResult> UpdateStatus(int id, short status, int updatedBy)
//        {
//            var res = await _s_Product.UpdateStatus(id, status, updatedBy);
//            return Ok(res);
//        }

//        [HttpPut]
//        public async Task<IActionResult> UpdateStatusList(string sequenceIds, short status, int updatedBy)
//        {
//            var res = await _s_Product.UpdateStatusList(sequenceIds, status, updatedBy);
//            return Ok(res);
//        }

//        [HttpDelete]
//        public async Task<IActionResult> Delete(int id, int updatedBy)
//        {
//            var res = await _s_Product.UpdateStatus(id, -1, updatedBy);
//            return Ok(res);
//        }

//        [HttpDelete]
//        public async Task<IActionResult> DeleteHard(int id)
//        {
//            var res = await _s_Product.Delete(id);
//            return Ok(res);
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var res = await _s_Product.GetById(id);
//            return Ok(res);
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetListByStatus(int status)
//        {
//            var res = await _s_Product.GetListByStatus(status);
//            return Ok(res);
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetListByPaging(MReq_ProductPaging request)
//        {
//            var res = await _s_Product.GetListByPaging(request);
//            return Ok(res);
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetListBySequenceStatusSearchText(string sequenceStatus, string searchText)
//        {
//            var res = await _s_Product.GetListBySequenceStatusSearchText(sequenceStatus, searchText);
//            return Ok(res);
//        }
//    }
//}
